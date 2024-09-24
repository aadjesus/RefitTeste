using Microsoft.AspNetCore.Mvc;
using RefitModel;
using System.ComponentModel.DataAnnotations;

namespace RefitTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ICliente _cliente;

        public ClienteController(ICliente cliente) =>
            _cliente = cliente;

        [HttpGet]
        public async Task<OkObjectResult> Clientes([FromQuery][Required] string nome) =>
            new(await _cliente.Clientes(nome));

        [HttpGet("{id}")]
        public async Task<OkObjectResult> Cliente(int id) =>
            new(await _cliente.Cliente(id));

        [HttpPost]
        public async Task<OkObjectResult> Incluir([FromBody] ClienteIncluir request) =>
            new(await _cliente.Incluir(request));

        [HttpDelete]
        public async Task<OkObjectResult> Excluir(int id) =>
            new(await _cliente.Excluir(id));
    }
}
