using ChallengeTaller.Models;
using DesafioTaller.DTO;

namespace DesafioTaller.Interfaces
{
    public interface IServicioPresupuesto
    {
        public Task<Presupuesto> EmitirPresupuesto(PresupuestoDTO presuDTO, int recargo, int descuento);
    }
}
