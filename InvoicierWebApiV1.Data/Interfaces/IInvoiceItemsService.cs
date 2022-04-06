using InvoicierWebApiV1.Core.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Interfaces
{
    public interface IInvoiceItemsService
    {
        Task<IEnumerable<InvoiceItems>> GetInvoiceItems();
        Task<InvoiceItems> GetInvoiceItemsByID(int id);
        Task<IEnumerable<InvoiceItems>> GetInvoiceItemsByInvoiceId(int invoiceId);
        Task CreateInvoiceItems(List<InvoiceItems> invoiceitems);
    }
}
