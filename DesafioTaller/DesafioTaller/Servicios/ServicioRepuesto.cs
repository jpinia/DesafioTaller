using ChallengeTaller.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using DesafioTaller.Interfaces;

namespace DesafioTaller.Servicios
{
    public class ServicioRepuesto : IServicioRepuesto
    {
        private string CadenaConexion;

        public ServicioRepuesto(ConexionBaseDatos conex)
        {
            CadenaConexion = conex.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task NuevoRepuesto(Repuesto e)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();

                param.Add("@Nombre", e.Nombre, DbType.String, ParameterDirection.Input);
                param.Add("@Precio", e.Precio, DbType.Decimal, ParameterDirection.Input);
                param.Add("@Descripcion", e.Descripcion, DbType.String, ParameterDirection.Input);
                param.Add("@RepuestoId", dbType: DbType.Int64, direction: ParameterDirection.Output);


                await sqlConexion.ExecuteScalarAsync("RepuestoAlta", param, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                throw new Exception("Se produjo un error al dar de alta" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

        }
    }
}
