using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Infrastructure.Services
{
    public class InvoiceItemsServices : IInvoiceItemsService
    {
        private readonly InvoicierDbContext _context;

        public InvoiceItemsServices(InvoicierDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<InvoiceItems>> GetInvoiceItems()
        {
            var invoiceItems = new List<InvoiceItems>();
            try
            {
                invoiceItems = _context.InvoiceItems.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something Happened {ex}");
            }
            return invoiceItems;
        }

        public async Task<InvoiceItems> GetInvoiceItemsByID(int id)
        {
            return _context.InvoiceItems.FirstOrDefault(x => x.ItemId == id);
        }

        public async Task<IEnumerable<InvoiceItems>> GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            var invoiceItme  = new List<InvoiceItems>();
            invoiceItme = _context.InvoiceItems.ToList().Select(invoice => invoice).Where(inv => inv.InvoiceId == invoiceId).ToList();
            return invoiceItme;
        }
        public async Task CreateInvoiceItems(List<InvoiceItems> invoiceItems)
        {
            await _context.AddRangeAsync(invoiceItems);
        }


    }
}
