using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces;
using InvoicierWebApiV1.Core.Interfaces.UseCases;
using InvoicierWebApiV1.Core.Shared;
using InvoicierWebApiV1.Dtos.InvoiceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Services.UseCases
{
    public class InvoiceUseCase : IInvoiceUseCase
    {
        private IInvoiceService _services;

        public InvoiceUseCase(IInvoiceService service)
        {
            _services = service;
        }
        public async Task<Response> GetInvoices()
        {
            Response response = new Response();
            try
            {
                var invoices = await _services.GetInvoices();
                if (invoices == null)
                   return response.failed("You do not have invoices try adding them");
                return response.success("Request Successful", invoices);
            }
            catch (Exception ex)
            {
               return response.failed(ex.Message, ResponseType.ServerError);
                throw;
            }
        }
        public string InvoiceNumberGenerate()
        {
            var invoiceNo = "";
            var rand = new Random();
            var dateTimeString = new DateTime().Minute;
            invoiceNo = $"IV{dateTimeString} {rand}";
            return invoiceNo;
        }
        public async Task<Response> CreateInvoice(InvoiceCreateDto invoiceModel)
        {
            Response response = new Response();
            var message = "";
            if(invoiceModel == null)
            {
                return response.failed("Null Request", null, ResponseType.NotFound);
            }
            try
            {
                var mappedInvoice = new Invoice(invoiceModel);
                await _services.CreateInvoice(mappedInvoice);
                if(_services.SaveChanges())
                {
                    mappedInvoice.InvoiceNumber = InvoiceNumberGenerate();
                    return response.success($"{mappedInvoice.InvoiceNumber} created successfully");
                }
            }
            catch (Exception ex)
            {
                message = ex.Message ?? ex.InnerException.Message;
                throw;
            }
            return response.failed($"Something went wrong");


        }

        public Task<Response> DeleteInvoice(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetInvoiceById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateInvoice(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}

