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
    public class RepuestoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServicioRepuesto _repuestoService;

        public RepuestoController(IMapper mapper, IServicioRepuesto repuestoService)
        {
            _mapper = mapper;
            _repuestoService = repuestoService;
        }

        [HttpPost("nuevoRepuesto")]
        public async Task<IActionResult> NuevoRepuesto(RepuestoDTO repdto)
        {
            try
            {
                var repuesto = _mapper.Map<Repuesto>(repdto);

                await _repuestoService.NuevoRepuesto(repuesto);
                return Ok("Repuesto agregado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar repuesto: {ex.Message}");
            }
        }
    }
}
