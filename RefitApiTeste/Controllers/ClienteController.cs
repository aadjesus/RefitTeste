using Microsoft.AspNetCore.Mvc;
using RefitModel;
using System.ComponentModel.DataAnnotations;

namespace RefitTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly List<Cliente> _clientes;

        public ClienteController(List<Cliente> clientes) => 
            _clientes = clientes;

        [HttpGet]
        public OkObjectResult Clientes([FromQuery][Required] string nome) =>
            new(_clientes.Where(w => w.Nome.ToUpper().Contains(nome.ToUpper())));

        [HttpGet("{id}")]
        public OkObjectResult Cliente(int id) =>
            new(_clientes.FirstOrDefault(w => w.Id == id));

        [HttpPost]
        public OkObjectResult Incluir([FromBody] ClienteIncluir request)
        {
            var id = _clientes.Count + 1;
            var cliente = new Cliente { Id = id, Nome = request.Nome };
            _clientes.Add(cliente);

            return new OkObjectResult(cliente);
        }

        [HttpDelete]
        public OkObjectResult Excluir(int id) =>
            new(_clientes.Remove(_clientes.FirstOrDefault(w => w.Id == id)));
    }
}