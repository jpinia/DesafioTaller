namespace ChallengeTaller.Models
{
    using System;
    using System.Collections.Generic;

    // Clase abstracta Vehículo
    public abstract class Vehiculo
    {
        public long Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public List<Desperfecto> Desperfectos { get; set; }

        // Otros atributos y métodos comunes a todos los vehículos
    }

    // Enumerador para los tipos de automóviles
    public enum TipoAutomovil
    {
        Compacto,
        Sedan,
        Monovolumen,
        Utilitario,
        Lujo
    }

    // Clase Automóvil que hereda de Vehículo
    public class Automovil : Vehiculo
    {
        public int Id { get; set; }
        public int IdVehiculo { get; set; }
        public TipoAutomovil Tipo { get; set; }
        public int CantidadPuertas { get; set; }
        // Otros atributos y métodos específicos de los automóviles
    }

    // Clase Moto que hereda de Vehículo
    public class Moto : Vehiculo
    {
        public int Id { get; set; }
        public int IdVehiculo { get; set; }
        public string Cilindrada { get; set; }
        // Otros atributos y métodos específicos de las motos
    }

    // Clase Desperfecto
    public class Desperfecto
    {
        public long Id { get; set; }
        public long IdVehiculo { get; set; }
        public string Descripcion { get; set; }
        public decimal ManoDeObra { get; set; }
        public int Tiempo { get; set; }
        public List<Repuesto> Repuestos { get; set; } // Lista de repuestos asociados al desperfecto
    }

    // Clase Presupuesto
    public class Presupuesto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }
        public int IdVehiculo { get; set; }
        public List<Desperfecto> Desperfectos { get; set; } // Lista de desperfectos asociados al presupuesto
    }

    // Clase Repuesto
    public class Repuesto
    {
        public long Id { get; set; }
        public long IdDesperfecto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }

    }

}
