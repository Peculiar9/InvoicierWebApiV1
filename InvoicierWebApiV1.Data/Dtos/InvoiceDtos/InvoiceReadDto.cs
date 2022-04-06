using InvoicierWebApiV1.Core.Dtos.InvoiceDtos;
using InvoicierWebApiV1.Core.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Dtos.InvoiceDtos
{
    public class InvoiceReadDto
    {
        public InvoiceReadDto(Invoice invoiceModel, List<InvoiceItemsReadDTO> invoiceItems, Client client)
        {
            Price = invoiceModel.Price;
            Discount = invoiceModel.Discount;
            CreatedOn = invoiceModel.CreatedOn;
            ExpiredOn = invoiceModel.ExpiredOn;
            Comment = invoiceModel.Comment;
            Email = client.Email;
            InvoiceItems = invoiceItems;
            Total = invoiceModel.Total;
            Concept = invoiceModel.Concept;
            clientId = client.Id;
            IsPaid = invoiceModel.IsPaid;
            InvoiceItems = invoiceItems.Count < 1 ? new List<InvoiceItemsReadDTO>() : invoiceItems;
        }
        public string Price { get; private set; }
        public string Discount { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime ExpiredOn { get; private set; }
        public string Comment { get; private set; }
        public string Email { get; private set; }
        public string Concept { get; private set; }
        [Required]
        public bool IsPaid { get; private set; }
        public string Total { get; private set; }
        public int clientId { get; private set; }
        public List<InvoiceItemsReadDTO> InvoiceItems { get; private set; }
    }
}