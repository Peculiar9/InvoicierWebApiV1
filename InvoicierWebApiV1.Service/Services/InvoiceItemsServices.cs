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
            invoiceItems = _context.InvoiceItems.ToList();
            return invoiceItems;
        }
                    
        public async Task<InvoiceItems> GetInvoiceItemsByID(int id)
        {
            if (id <= 0) throw new Exception("Provide valid id");
            return _context.InvoiceItems.FirstOrDefault(x => x.ItemId == id);
        }

        public async Task<IEnumerable<InvoiceItems>> GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            if (invoiceId <= 0) throw new Exception("Provide valid id");
            var invoiceItem  = new List<InvoiceItems>();
            invoiceItem = _context.InvoiceItems.ToList().FindAll(x => x.InvoiceId == invoiceId);
            if (invoiceItem.Count < 1) throw new Exception("Invoice does not have an item attached to it");
            return invoiceItem;
        }
        public async Task CreateInvoiceItems(List<InvoiceItems> invoiceItems)
        {
            await _context.AddRangeAsync(invoiceItems);
        }


    }
}
