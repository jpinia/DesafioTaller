using ChallengeTaller.Models;

namespace DesafioTaller.Interfaces
{
    public interface IServicioVehiculo
    {
        public Task NuevoVehiculoMoto(Moto vehiculo);
        public Task NuevoVehiculoAutomovil(Automovil vehiculo);
    }
}
