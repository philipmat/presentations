using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ThreeApproaches.Hubs
{
    public class MessageHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void NewInvoice(string invoiceNumber)
        {
            Clients.All.newInvoice(invoiceNumber);
        }
    }
}