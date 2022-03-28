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
        private IClientService _clientService;

        public InvoiceUseCase(IInvoiceService service, IClientService clientService)
        {
            _services = service;
            _clientService = clientService;
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
        public async Task<Response> MailInvoices(string email)
        {
            return new Response().failed("Email not successful");
        }
        public string InvoiceNumberGenerate()
        {
            var invoiceNo = "";
            var rand = new Random().Next(0, 20);
            var dateTimeString = new DateTime().Day + new DateTime().Minute;
            invoiceNo = $"IV{dateTimeString} {rand}";
            return invoiceNo;
        }
     
        public async Task<int> GetOrganizationId(int clientId)
        {
            var client = await _clientService.GetClientsById(clientId);
            if (client != null) return client.OrganizationId;
            else return 1;
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
                mappedInvoice.InvoiceNumber = InvoiceNumberGenerate();
                mappedInvoice.OrganizationId = await GetOrganizationId(invoiceModel.clientId);
                mappedInvoice.client = await _clientService.GetClientsById(invoiceModel.clientId);
                await _services.CreateInvoice(mappedInvoice);
                if(_services.SaveChanges())
                {
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

