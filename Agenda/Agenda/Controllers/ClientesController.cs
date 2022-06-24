using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Agenda.Models;


namespace Agenda.Controllers
{
    public class ClientesController : ApiController
    {
        static readonly IClienteRepositorio clienteRepositorio = new ClienteRepositorio();

        public HttpResponseMessage GetAllClientes()
        {
            List<Cliente> listaClientes = clienteRepositorio.GetAll().ToList();
            return Request.CreateResponse<List<Cliente>>(HttpStatusCode.OK, listaClientes);
        }

        public HttpResponseMessage GetCliente(int id)
        {
            Cliente cliente = clienteRepositorio.Get(id);
            if (cliente == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cliente não localizado para o Id informado");
            }
            else
            {
                return Request.CreateResponse<Cliente>(HttpStatusCode.OK, cliente);
            }
        }

        public IEnumerable<Cliente> GetClientesPorSobrenome(string sobrenome)
        {
            return clienteRepositorio.GetAll().Where(
                e => string.Equals(e.sobrenome, sobrenome, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Cliente> GetClientesPorTelefone(int telefone)
        {
            return clienteRepositorio.GetAll().Where(
               e => string.Equals(e.telefone.ToString(), telefone.ToString(), StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostCliente(Cliente cliente)
        {
            bool result = clienteRepositorio.Add(cliente);
            if (result)
            {
                var response = Request.CreateResponse<Cliente>(HttpStatusCode.Created, cliente);
                string uri = Url.Link("DefaultApi", new { id = cliente.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cliente não foi incluído com sucesso");
            }
        }

        public HttpResponseMessage PutCliente(int id, Cliente cliente)
        {
            cliente.id = id;
            if (!clienteRepositorio.Update(cliente))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Não foi possível atualizar o cliente para o id informado");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        public HttpResponseMessage DeleteCliente(int id)
        {
            clienteRepositorio.Remove(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
