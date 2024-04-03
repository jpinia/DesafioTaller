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
    public class PresupuestoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServicioPresupuesto _presupuestoService;

        public PresupuestoController(IMapper mapper, IServicioPresupuesto presupuestoService)
        {
            _mapper = mapper;
            _presupuestoService = presupuestoService;
        }

        [HttpPost("nuevoPresupuesto")]
        public async Task<IActionResult> NuevoPresupuesto(PresupuestoDTO presupuesto, int recargo, int descuento)
        {
            try
            {
                await _presupuestoService.EmitirPresupuesto(presupuesto, recargo, descuento);
                return Ok("Presupuesto emitido exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al emitir presupuesto: {ex.Message}");
            }
        }
    }
}
