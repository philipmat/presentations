using System; 
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Caching;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using ThreeApproaches.Hubs;

namespace ThreeApproaches.Controllers.API
{
    public class InvoiceController : ApiController
    {
        private readonly static Lazy<IHubConnectionContext> _clients =
            new Lazy<IHubConnectionContext>(() => GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients);
        // GET: api/Invoice
        public IEnumerable<string> Get()
        {
            var invoiceNumbers = MemoryCache.Default.Get("invoices") as string[];
            if (invoiceNumbers == null) return new string[0];
            return invoiceNumbers;
        }

        // GET: api/Invoice/5
        public string Get(string id)
        {
            var invoiceNumbers = MemoryCache.Default.Get("invoices") as string[];
            if (invoiceNumbers == null) return string.Empty;
            var actual = invoiceNumbers.FirstOrDefault(x => string.Equals(x, id, StringComparison.OrdinalIgnoreCase));
            if (actual == null) return string.Empty;
            return actual;
        }

        // POST: api/Invoice
        public void Post([FromBody]string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            var invoiceNumbers = MemoryCache.Default.Get("invoices") as string[];
            if (invoiceNumbers == null) invoiceNumbers = new string[] { value };
            else
            {
                var tmp = new List<string>(invoiceNumbers);
                if (!tmp.Any(x => string.Equals(x, value, StringComparison.OrdinalIgnoreCase)))
                {
                    tmp.Add(value);
                    invoiceNumbers = tmp.ToArray();
                    BroadcastInvoiceAdded(value);
                }
            }
            MemoryCache.Default.Set("invoices", invoiceNumbers, DateTimeOffset.Now.AddHours(2));
        }

        // PUT: api/Invoice/5
        public void Put(string id, [FromBody]string value)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(value)) return;
            var invoiceNumbers = MemoryCache.Default.Get("invoices") as string[];
            if (invoiceNumbers == null) return;
            else
            {
                var index = Array.FindIndex(invoiceNumbers, x => string.Equals(x, id, StringComparison.OrdinalIgnoreCase));
                if (index != -1)
                {
                    invoiceNumbers[index] = value;
                }
            }
            MemoryCache.Default.Set("invoices", invoiceNumbers, DateTimeOffset.Now.AddHours(2));
        }

        // DELETE: api/Invoice/5
        public void Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return;
            var invoiceNumbers = MemoryCache.Default.Get("invoices") as string[];
            if (invoiceNumbers == null) return;
            else
            {
                invoiceNumbers = invoiceNumbers.Except(new[]{ id }, StringComparer.OrdinalIgnoreCase).ToArray();
            }
            MemoryCache.Default.Set("invoices", invoiceNumbers, DateTimeOffset.Now.AddHours(2));
        }

        private void BroadcastInvoiceAdded(string value)
        {
            _clients.Value.All.invoiceCreated(value);
        }
    }
}
