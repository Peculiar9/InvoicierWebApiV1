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
        public async Task<IEnumerable<Client>> GetClients()
        {
            var clientList = _context.Clients.ToList();
            return clientList;
        }
        public async Task<Client> GetClientsById(int id)
        {
           return _context.Clients.FirstOrDefault(p => p.Id == id);
        }
        public async Task RemoveClient(Client clientModel)
        {
             if (clientModel != null)
            {
                try
                {
                    _context.Remove(clientModel);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(ex.ToString(), nameof(clientModel));
                }
            }
        }

        public async Task<bool> SaveChanges()
        {
             return (await _context.SaveChangesAsync() >= 0);
        }
    }
}

       
    