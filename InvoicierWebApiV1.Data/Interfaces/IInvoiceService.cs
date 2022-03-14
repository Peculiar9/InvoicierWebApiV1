using InvoicierWebApiV1.Core.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Interfaces
{
    public interface IInvoiceService
    {
         Task<IEnumerable<Invoice>> GetInvoices();
         Task<Invoice> GetInvoiceById(int id);
         Task CreateInvoice(Invoice model);
         Task RemoveInvoice(Invoice invoice);
         bool SaveChanges();
    }
}