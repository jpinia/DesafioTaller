using ChallengeTaller.Models;

namespace DesafioTaller.DTO
{
    public class VehiculoDTO
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public List<DesperfectoDTO> Desperfectos { get; set; }
        public TipoVehiculo Tipo { get; set; }

        // Propiedades específicas para Automovil
        public TipoAutomovil? TipoAutomovil { get; set; }
        public int? CantidadPuertas { get; set; }

        // Propiedad específica para Moto
        public string Cilindrada { get; set; }
    }

    public enum TipoVehiculo
    {
        Automovil,
        Moto
    }

    public class DesperfectoDTO
    {
        public string Descripcion { get; set; }
        public decimal ManoDeObra { get; set; }
        public int Tiempo { get; set; }
        public List<RepuestoDTO> Repuestos { get; set; }
    }

    public class PresupuestoDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int IdVehiculo { get; set; }
    }

    public class RepuestoDTO
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
    }

    public class AutomovilDTO
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public List<DesperfectoDTO> Desperfectos { get; set; }

        // Propiedades específicas para Automovil
        public TipoAutomovil? TipoAutomovil { get; set; }
        public int? CantidadPuertas { get; set; }

    }

    public class MotoDTO
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public List<DesperfectoDTO> Desperfectos { get; set; }
        public string Cilindrada { get; set; }

    }
}
