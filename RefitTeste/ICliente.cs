using Refit;
using RefitModel;

namespace RefitTeste
{

    [Headers("accept: application/json", "Authorization: Bearer")]
    public interface ICliente
    {
        [Get("/cliente?nome={nome}")]
        Task<IEnumerable<Cliente>> Clientes(string nome);

        [Get("/cliente/{id}")]
        Task<Cliente> Cliente(int id);

        [Headers("Content-Type: application/json;charset=utf-8")]
        [Post("/cliente/")]
        Task<Cliente> Incluir([Body] ClienteIncluir clienteIncluir);

        [Delete("/cliente/{id}")]
        Task<bool> Excluir(int id);
    }
}
