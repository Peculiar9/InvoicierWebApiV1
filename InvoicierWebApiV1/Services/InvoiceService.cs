using System.Collections.Generic;
using System.Threading.Tasks;
using InvoicierWebApiV1.Data.EntityModels;

namespace InvoicierWebApiV1.Services
{
    public interface InvoiceService
    {
         Task<IEnumerable<Invoice>> GetInvoices();
         Task<Invoice> GetInvoiceById(int id);
         Task CreateInvoice(Invoice model);
         Task RemoveInvoice(Invoice invoice);
         bool SaveChanges();
    }
}