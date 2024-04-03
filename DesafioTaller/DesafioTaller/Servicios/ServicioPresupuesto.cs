using AutoMapper;
using ChallengeTaller.Models;
using Dapper;
using DesafioTaller.DTO;
using DesafioTaller.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace DesafioTaller.Servicios
{
    public class ServicioPresupuesto : IServicioPresupuesto
    {
        private string CadenaConexion;
        private readonly IMapper _mapper;


        public ServicioPresupuesto(IMapper mapper,ConexionBaseDatos conex)
        {
            _mapper = mapper;
            CadenaConexion = conex.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task<Presupuesto> EmitirPresupuesto(PresupuestoDTO presuDTO, int recargo, int descuento)
        {
            IEnumerable<Desperfecto> desperfectos;

            decimal costoTotalReparacion = 0;

            using (var connection = new SqlConnection(CadenaConexion))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@VehiculoId", presuDTO.IdVehiculo);

                using (var multi = await connection.QueryMultipleAsync("ObtenerDesperfectosPorVehiculo", parameters, commandType: CommandType.StoredProcedure))
                {
                    var desperfectosDictionary = new Dictionary<long, Desperfecto>();
                    var repuestosDictionary = new Dictionary<long, List<Repuesto>>();

                        desperfectos = multi.Read<Desperfecto, Repuesto, Desperfecto>(
                        (desperfecto, repuesto) =>
                        {
                            if (!desperfectosDictionary.TryGetValue(desperfecto.Id, out var foundDesperfecto))
                            {
                                foundDesperfecto = desperfecto;
                                desperfectosDictionary.Add(desperfecto.Id, foundDesperfecto);
                            }

                            if (repuesto != null)
                            {
                                if (!repuestosDictionary.TryGetValue(foundDesperfecto.Id, out var repuestosList))
                                {
                                    repuestosList = new List<Repuesto>();
                                    repuestosDictionary.Add(foundDesperfecto.Id, repuestosList);
                                }

                                repuestosList.Add(repuesto);
                            }

                            return foundDesperfecto;
                        },
                        splitOn: "Id");

                    foreach (var desperfecto in desperfectos)
                    {
                        if (repuestosDictionary.TryGetValue(desperfecto.Id, out var repuestosList))
                        {
                            desperfecto.Repuestos = repuestosList;
                        }
                    }

                    desperfectos = desperfectos.Distinct().ToList();
                }
            }

            if (desperfectos != null && desperfectos.Any())
            {
                foreach (var desperfecto in desperfectos)
                {
                    decimal costoDesperfecto = desperfecto.ManoDeObra + desperfecto.Repuestos.Sum(repuesto => repuesto.Precio);

                    costoDesperfecto += 130 * desperfecto.Tiempo;
                    costoTotalReparacion += costoDesperfecto;
                }

                costoTotalReparacion -= descuento;
                costoTotalReparacion += recargo;

                costoTotalReparacion += costoTotalReparacion * 0.1m;
            }

            long Id;

            using (var connection = new SqlConnection(CadenaConexion))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", presuDTO.Nombre);
                parameters.Add("@Apellido", presuDTO.Apellido);
                parameters.Add("@Email", presuDTO.Email);
                parameters.Add("@Total", costoTotalReparacion);
                parameters.Add("@IdVehiculo", presuDTO.IdVehiculo);
                parameters.Add("@Id", dbType: DbType.Int64, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                await connection.ExecuteAsync("GuardarPresupuesto", parameters, commandType: CommandType.StoredProcedure);

                 Id = parameters.Get<long>("@Id");

            }
            var presupuesto = _mapper.Map<Presupuesto>(presuDTO);
            presupuesto.Id= Id;
            return presupuesto;
        }
    }
}
