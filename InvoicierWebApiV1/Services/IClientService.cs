using System.Collections.Generic;
using System.Threading.Tasks;
using InvoicierWebApiV1.Data.EntityModels;

namespace InvoicierWebApiV1.Services
{
    public interface IClientService
    {
         Task<bool> SaveChanges();
         Task<IEnumerable<Client>> GetClients();
         Task<Client> GetClientsById(int id);
         Task RemoveClient(Client clientModel);
         Task CreateClient(Client clientModel);
    }
}