using Microsoft.AspNetCore.Mvc;

namespace SistemaMedicacoes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TesteController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("API do Sistema de Controle de Medicacoes funcionando.");
    }
}
