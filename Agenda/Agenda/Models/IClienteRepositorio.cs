using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public interface IClienteRepositorio
    {
        IEnumerable<Cliente> GetAll();
        Cliente Get(int id);
        bool Add(Cliente cliente);
        void Remove(int id);
        bool Update(Cliente cliente);
    }
}
