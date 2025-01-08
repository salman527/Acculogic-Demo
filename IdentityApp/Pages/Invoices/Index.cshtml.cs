﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdentityApp.Data;
using IdentityApp.Models;

namespace IdentityApp.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly IdentityApp.Data.ApplicationDbContext _context;
        public Dictionary<string, int> revenueSubmitted;
        public Dictionary<string, int> revenueApproved;
        public Dictionary<string, int> revenueRejected;

        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger,
           ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        public void OnGet()
        {
            InitDict(ref revenueSubmitted);
            InitDict(ref revenueApproved);
            InitDict(ref revenueRejected);

            var invoices = _context.Invoice.ToList();

            foreach (var invoice in invoices)
            {

                switch (invoice.Status)
                {
                    case InvoiceStatus.Submitted:
                        revenueSubmitted[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    case InvoiceStatus.Approved:
                        revenueApproved[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    case InvoiceStatus.Rejected:
                        revenueRejected[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    default:
                        break;
                }
            }

        }
        private void InitDict(ref Dictionary<string, int> dict)
        {
            dict = new Dictionary<string, int>()
            {
                { "January",0 },
                { "February",0 },
                { "March",0 },
                { "April",0 },
                { "May",0 },
                { "June",0 },
                { "July",0 },
                { "August",0 },
                { "September",0 },
                { "October",0 },
                { "November",0 },
                { "December",0 }
            };
        }
    }
}