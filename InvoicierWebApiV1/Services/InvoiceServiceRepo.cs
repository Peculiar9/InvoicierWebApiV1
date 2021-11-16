using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoicierWebApiV1.Data.EntityModels;

namespace InvoicierWebApiV1.Services
{
    public class InvoiceServiceRepo : InvoiceService
    {
        private readonly InvoicierDbContext _context;

        public InvoiceServiceRepo(InvoicierDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            var invoices = _context.Invoices.ToList();
           
            return invoices;
        }
        public async Task CreateInvoice(Invoice model)
        { 
            if (model != null)
            {
                try
                {
                    await _context.AddAsync(model);
                }
                catch (System.Exception ex)
                {
                    throw new ArgumentNullException(ex.ToString(), nameof(model));
                }
            }
        }
        public async Task<Invoice> GetInvoiceById(int id) => _context.Invoices.FirstOrDefault(p => p.Id == id);


        public async Task RemoveInvoice(Invoice invoice)
        {
            if (invoice != null)
            { try
                {
                    await _context.AddAsync(invoice);
                }
                catch (System.Exception ex)
                {
                    throw new ArgumentNullException(ex.ToString(), nameof(invoice));
                }
            }
        }
            

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}