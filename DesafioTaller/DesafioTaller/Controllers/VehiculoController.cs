using AutoMapper;
using ChallengeTaller.Models;
using DesafioTaller.DTO;
using DesafioTaller.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTaller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServicioVehiculo _vehiculoService;

        public VehiculoController(IMapper mapper, IServicioVehiculo vehiculoService)
        {
            _mapper = mapper;
            _vehiculoService = vehiculoService;
        }

        [HttpPost("nuevaMoto")]
        public async Task<IActionResult> NuevoVehiculoMoto(MotoDTO moto)
        {
            try
            {
                var vehiculo = _mapper.Map<Moto>(moto);

                await _vehiculoService.NuevoVehiculoMoto(vehiculo);
                return Ok("Vehículo Moto agregado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar vehículo: {ex.Message}");
            }
        }

        [HttpPost("nuevoAutomovil")]
        public async Task<IActionResult> NuevoVehiculoAutomovil(AutomovilDTO auto)
        {
            try
            {
                var vehiculo = _mapper.Map<Automovil>(auto);

                await _vehiculoService.NuevoVehiculoAutomovil(vehiculo);
                return Ok("Vehículo Automovil agregado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar vehículo: {ex.Message}");
            }
        }
    }
}
