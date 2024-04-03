using ChallengeTaller.Models;
using Dapper;
using DesafioTaller.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace DesafioTaller.Servicios
{
    public class ServicioVehiculo : IServicioVehiculo
    {
        private string CadenaConexion;

        public ServicioVehiculo(ConexionBaseDatos conex)
        {
            CadenaConexion = conex.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task NuevoVehiculoMoto(Moto vehiculo)
        {
            using (var sqlConnection = new SqlConnection(CadenaConexion))
            {
                await sqlConnection.OpenAsync();

                var vehiculoParameters = new DynamicParameters();
                vehiculoParameters.Add("@Marca", vehiculo.Marca, DbType.String, ParameterDirection.Input);
                vehiculoParameters.Add("@Modelo", vehiculo.Modelo, DbType.String, ParameterDirection.Input);
                vehiculoParameters.Add("@Patente", vehiculo.Patente, DbType.String, ParameterDirection.Input);
                vehiculoParameters.Add("@Tipo", "Moto", DbType.String, ParameterDirection.Input);


                // Insertar vehículo genérico en la tabla Vehiculo
                var vehiculoId = await sqlConnection.ExecuteScalarAsync("VehiculoAlta", vehiculoParameters, commandType: CommandType.StoredProcedure);


                var motoParameters = new DynamicParameters();
                motoParameters.Add("@IdVehiculo", vehiculoId, DbType.Int64, ParameterDirection.Input);
                motoParameters.Add("@Cilindrada", vehiculo.Cilindrada, DbType.String, ParameterDirection.Input);

                await sqlConnection.ExecuteAsync("MotoAlta", motoParameters, commandType: CommandType.StoredProcedure);


                // Insertar los desperfectos asociados al vehículo
                foreach (var desperfecto in vehiculo.Desperfectos)
                {
                    var desperfectoParameters = new DynamicParameters();
                    desperfectoParameters.Add("@IdVehiculo", vehiculoId, DbType.Int64, ParameterDirection.Input);
                    desperfectoParameters.Add("@Descripcion", desperfecto.Descripcion, DbType.String, ParameterDirection.Input);
                    desperfectoParameters.Add("@ManoDeObra", desperfecto.ManoDeObra, DbType.Decimal, ParameterDirection.Input);
                    desperfectoParameters.Add("@Tiempo", desperfecto.Tiempo, DbType.Int32, ParameterDirection.Input);
                    desperfectoParameters.Add("@DesperfectoId", dbType: DbType.Int64, direction: ParameterDirection.Output); 


                    await sqlConnection.ExecuteScalarAsync("DesperfectoAlta", desperfectoParameters, commandType: CommandType.StoredProcedure);

                    long desperfectoId = desperfectoParameters.Get<long>("@DesperfectoId");


                    // Insertar repuestos para cada desperfecto
                    foreach (var repuesto in desperfecto.Repuestos)
                    {
                        var repuestoParameters = new DynamicParameters();
                        repuestoParameters.Add("@Nombre", repuesto.Nombre, DbType.String, ParameterDirection.Input);
                        repuestoParameters.Add("@Precio", repuesto.Precio, DbType.Decimal, ParameterDirection.Input);
                        repuestoParameters.Add("@Descripcion", repuesto.Descripcion, DbType.String, ParameterDirection.Input);
                        repuestoParameters.Add("@RepuestoId", dbType: DbType.Int64, direction: ParameterDirection.Output);


                        await sqlConnection.ExecuteAsync("RepuestoAlta", repuestoParameters, commandType: CommandType.StoredProcedure);

                        long repuestoId = desperfectoParameters.Get<long>("@DesperfectoId");

                        var repuesto2Parameters = new DynamicParameters();
                        repuesto2Parameters.Add("@IdDesperfecto", desperfectoId, DbType.Int64, ParameterDirection.Input);
                        repuesto2Parameters.Add("@IdRepuesto", repuestoId, DbType.Int64, ParameterDirection.Input);

                        await sqlConnection.ExecuteAsync("DesperfectoRepuestoAlta", repuesto2Parameters, commandType: CommandType.StoredProcedure);

                    }
                }
            }
        } 
        

        public async Task NuevoVehiculoAutomovil(Automovil vehiculo)
        {
            using (var sqlConnection = new SqlConnection(CadenaConexion))
            {
                await sqlConnection.OpenAsync();

                var vehiculoParameters = new DynamicParameters();
                vehiculoParameters.Add("@Marca", vehiculo.Marca, DbType.String, ParameterDirection.Input);
                vehiculoParameters.Add("@Modelo", vehiculo.Modelo, DbType.String, ParameterDirection.Input);
                vehiculoParameters.Add("@Patente", vehiculo.Patente, DbType.String, ParameterDirection.Input);
                vehiculoParameters.Add("@Tipo", "Automovil", DbType.String, ParameterDirection.Input);


                // Insertar vehículo genérico en la tabla Vehiculo
                var vehiculoId = await sqlConnection.ExecuteScalarAsync("VehiculoAlta", vehiculoParameters, commandType: CommandType.StoredProcedure);


                var automovilParameters = new DynamicParameters();
                automovilParameters.Add("@IdVehiculo", vehiculoId, DbType.Int64, ParameterDirection.Input);
                automovilParameters.Add("@Tipo", (int)vehiculo.Tipo, DbType.Int32, ParameterDirection.Input);
                automovilParameters.Add("@CantidadPuertas", vehiculo.CantidadPuertas, DbType.Int32, ParameterDirection.Input);

                await sqlConnection.ExecuteAsync("AutomovilAlta", automovilParameters, commandType: CommandType.StoredProcedure);


                // Insertar los desperfectos asociados al vehículo
                foreach (var desperfecto in vehiculo.Desperfectos)
                {
                    var desperfectoParameters = new DynamicParameters();
                    desperfectoParameters.Add("@IdVehiculo", vehiculoId, DbType.Int64, ParameterDirection.Input);
                    desperfectoParameters.Add("@Descripcion", desperfecto.Descripcion, DbType.String, ParameterDirection.Input);
                    desperfectoParameters.Add("@ManoDeObra", desperfecto.ManoDeObra, DbType.Decimal, ParameterDirection.Input);
                    desperfectoParameters.Add("@Tiempo", desperfecto.Tiempo, DbType.Int32, ParameterDirection.Input);
                    desperfectoParameters.Add("@DesperfectoId", dbType: DbType.Int64, direction: ParameterDirection.Output);


                    await sqlConnection.ExecuteScalarAsync("DesperfectoAlta", desperfectoParameters, commandType: CommandType.StoredProcedure);

                    long desperfectoId = desperfectoParameters.Get<long>("@DesperfectoId");

                    foreach (var repuesto in desperfecto.Repuestos)
                    {
                        var repuestoParameters = new DynamicParameters();
                        repuestoParameters.Add("@Nombre", repuesto.Nombre, DbType.String, ParameterDirection.Input);
                        repuestoParameters.Add("@Precio", repuesto.Precio, DbType.Decimal, ParameterDirection.Input);
                        repuestoParameters.Add("@Descripcion", repuesto.Descripcion, DbType.String, ParameterDirection.Input);
                        repuestoParameters.Add("@RepuestoId", dbType: DbType.Int64, direction: ParameterDirection.Output);


                        await sqlConnection.ExecuteAsync("RepuestoAlta", repuestoParameters, commandType: CommandType.StoredProcedure);

                        long repuestoId = desperfectoParameters.Get<long>("@DesperfectoId");

                        var repuesto2Parameters = new DynamicParameters();
                        repuesto2Parameters.Add("@IdDesperfecto", desperfectoId, DbType.Int64, ParameterDirection.Input);
                        repuesto2Parameters.Add("@IdRepuesto", repuestoId, DbType.Int64, ParameterDirection.Input);

                        await sqlConnection.ExecuteAsync("DesperfectoRepuestoAlta", repuesto2Parameters, commandType: CommandType.StoredProcedure);

                    }
                }
            }
        }
    }
}
