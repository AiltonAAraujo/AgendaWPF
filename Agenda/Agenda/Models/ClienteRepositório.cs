using System;
using System.Collections.Generic;


namespace Agenda.Models
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private List<Cliente> clientes = new List<Cliente>();
        //private int _nextId = 1;
        public ClienteRepositorio()
        {
            Add(new Cliente { nome = "André", id = 1, sobrenome = "Monteiro", telefone = 34556543 });
            Add(new Cliente { nome = "Jefferson", id = 2, sobrenome = "Nascimento", telefone = 24875498 });
            Add(new Cliente { nome = "Miriam", id = 3, sobrenome = "Alves", telefone = 35439823 });
            Add(new Cliente { nome = "Janice", id = 4, sobrenome = "Silva", telefone = 34219628 });
            Add(new Cliente { nome = "Jessica", id = 5, sobrenome = "Couto", telefone = 43258765 });
        }
        public IEnumerable<Cliente> GetAll()
        {
            return clientes;
        }

        public Cliente Get(int id)
        {
            return clientes.Find(s => s.id == id);
        }

        public bool Add(Cliente cliente)
        {
            bool addResult = false;
            if (cliente == null)
            {
                return addResult;
            }
            int index = clientes.FindIndex(s => s.id == cliente.id);
            if (index == -1)
            {
                clientes.Add(cliente);
                addResult = true;
                return addResult;
            }
            else
            {
                return addResult;
            }
        }

        public void Remove(int id)
        {
            clientes.RemoveAll(s => s.id == id);
        }

        public bool Update(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException("Cliente");
            }
            int index = clientes.FindIndex(s => s.id == cliente.id);
            if (index == -1)
            {
                return false;
            }
            clientes.RemoveAt(index);
            clientes.Add(cliente);
            return true;
        }
    }
}