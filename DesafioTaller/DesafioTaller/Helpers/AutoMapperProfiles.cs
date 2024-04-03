using AutoMapper;
using ChallengeTaller.Models;
using DesafioTaller.DTO;

namespace DesafioTaller.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutomovilDTO, Automovil>();
            CreateMap<MotoDTO, Moto>();
            CreateMap<VehiculoDTO, Vehiculo>();
            CreateMap<DesperfectoDTO, Desperfecto>();
            CreateMap<RepuestoDTO, Repuesto>();
            CreateMap<PresupuestoDTO, Presupuesto>();

        }
    }
}
