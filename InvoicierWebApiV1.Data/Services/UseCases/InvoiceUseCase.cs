using AutoMapper;
using InvoicierWebApiV1.Core.Dtos.InvoiceDtos;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces;
using InvoicierWebApiV1.Core.Interfaces.OrganizationServices;
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
        private readonly IOrganizationServices _orgServices;
        private readonly IInvoiceItemsService _invoiceItemService;
        private readonly IMapper _mapper;

        public InvoiceUseCase(IInvoiceService service, IClientService clientService, IOrganizationServices orgServices, IInvoiceItemsService invoiceItemService, IMapper mapper)
        {
            _services = service;
            _clientService = clientService;
            _orgServices = orgServices;
            _invoiceItemService = invoiceItemService;
            _mapper = mapper;
        }
        public async Task<Response> GetInvoices()
        {
            Response response = new Response();
            try
            {
                var invoices = await _services.GetInvoices();
                var invoicesRead = new List<InvoiceReadDto>();
                if (invoices != null)
                    invoices.ToList().ForEach(async x =>
                    {
                        var invoiceItems = await _invoiceItemService.GetInvoiceItemsByInvoiceId(x.Id);
                        var invoiceItemsMapped = _mapper.Map<List<InvoiceItemsReadDTO>>(invoiceItems);
                        var client = await _clientService.GetClients();
                        var clientInv = client.FirstOrDefault(clnt => clnt.OrganizationId == x.OrganizationId);
                        invoicesRead.Add(new InvoiceReadDto(x, invoiceItemsMapped, clientInv));
                    });
                if (invoices == null)
                   return response.failed("You do not have invoices try adding them");
                return response.success("Request Successful", invoicesRead);
            }
            catch (Exception ex)
            {
               return response.failed(ex.Message, ResponseType.ServerError);    
                throw;
            }
        }

        public async Task<Response> CreateInvoice(int clientId, InvoiceCreateDto invoiceModel)
        {
            Response response = new Response();
            var message = "";
            if (invoiceModel == null)
            {
                return response.failed("Null Request", null, ResponseType.NotFound);
            }
            try
            {
                var invoiceMapped = new Invoice(invoiceModel, clientId);
                invoiceMapped.InvoiceNumber = InvoiceNumberGenerate();
                var client = await _clientService.GetClientsById(clientId);
                var invoiceItems = _mapper.Map<List<InvoiceItems>>(invoiceModel.Items);
                invoiceMapped.OrganizationId = client.OrganizationId;
                await _services.CreateInvoice(invoiceMapped);
                await _invoiceItemService.CreateInvoiceItems(invoiceItems);
                if (_services.SaveChanges())
                    invoiceItems.ForEach(invoiceItem => invoiceItem.InvoiceId = invoiceMapped.Id);
                    return response.success($"Invoice {invoiceMapped.InvoiceNumber} created");
            }
            catch (Exception ex)
            {
                message = ex.Message ?? ex.InnerException.Message;
                throw;
            }
            return response.failed($"Something went wrong");
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
            invoiceNo = $"IV{dateTimeString}{rand}";
            return invoiceNo;
        }
     
      
                


        public async Task<Response> DeleteInvoice(int id)
        {
            var response = new Response();
            var invoice = await _services.GetInvoiceById(id);
            if (invoice == null)
                return response.failed("Invoice does not Exist");
            return response.success($"Invoice {invoice.InvoiceNumber} Deleted Successfully");
        }

        public async Task<Response> GetInvoiceById(int id)
        {
            var response = new Response();
            var invoice = await _services.GetInvoiceById(id);
            if (invoice == null)
                return response.failed("Invoice does not Exist");
            return response.success("", invoice);
        }

        public Task<Response> UpdateInvoice(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}

