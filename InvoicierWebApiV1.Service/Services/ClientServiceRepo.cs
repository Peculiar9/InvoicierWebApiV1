using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces;

namespace InvoicierWebApiV1.Infrastructure.Service
{
    public class ClientServiceRepo : IClientService
    {
        private readonly InvoicierDbContext _context;
        public ClientServiceRepo(InvoicierDbContext context)
        {
            _context = context;
        }
        public async Task CreateClient(Client clientModel)
        {
           await _context.Clients.AddAsync(clientModel);
        }

        public async Task<IEnumerable<Client>> GetClientByOrgId(int id)
        {
           var client = _context.Clients.Where(org => org.Id == id).ToList();
            return client;
        }


        public async Task<IEnumerable<Client>> GetClients()
        {
            var clientList = _context.Clients;
            return clientList;
        }
        public async Task<Client> GetClientsById(int id)
        {
          if(id < 1) throw new ArgumentNullException("Id required");
           return _context.Clients.FirstOrDefault(p => p.Id == id);
        }
        public async Task RemoveClient(Client clientModel)
        {
            if (clientModel != null) throw new Exception("Client to be deleted required");
            _context.Remove(clientModel);
        }

            
        public async Task<bool> SaveChanges()
        {
             return (await _context.SaveChangesAsync() >= 0);
        }
    }
}

       
    