using ChinookSystem.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class InvoiceInfo
    {

        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string BillingAddress { get; set; }

        public string BillingCity { get; set; }

        public string BillingState { get; set; }

        public string BillingCountry { get; set; }

        public string BillingPostalCode { get; set; }

        public decimal Total { get; set; }
    }
}
