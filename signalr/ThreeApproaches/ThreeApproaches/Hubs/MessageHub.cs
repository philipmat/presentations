using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace ThreeApproaches.Hubs
{
    public class MessageHub : Hub
    {
        public void Hello() {
            Clients.All.hello();
        }

        public void InvoiceCreated(string number) {
            Clients.All.invoiceCreated(number);
        }

        public void DistributionRecordCreated(string number) {
            Clients.All.distributionRecordCreated(number);
        }

        public void BillingRecordCreated(string number) {
            Clients.All.billingRecordCreated(number);
        }

        public void JournalEntryCreated(string number) {
            Clients.All.journalEntryCreated(number);
        }
    }
}