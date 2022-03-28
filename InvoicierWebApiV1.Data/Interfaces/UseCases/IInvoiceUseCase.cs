using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Shared;
using InvoicierWebApiV1.Dtos.InvoiceDtos;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Interfaces.UseCases
{
    public interface IInvoiceUseCase
    {
        Task<Response> GetInvoices();
        Task<Response> CreateInvoice(InvoiceCreateDto invoiceModel);
        Task<Response> GetInvoiceById(int id);
        Task<Response> UpdateInvoice(Invoice invoice);
        Task<Response> DeleteInvoice(int id);
        Task<Response> MailInvoices(string email);
       
    }
}
