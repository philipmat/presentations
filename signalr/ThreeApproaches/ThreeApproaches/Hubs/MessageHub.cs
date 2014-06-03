using System.Linq;

using Microsoft.AspNet.SignalR;

using ThreeApproaches.Support;
using System.Threading.Tasks;

namespace ThreeApproaches.Hubs
{
    public class MessageHub : Hub
    {
        public void Hello() {
            Clients.All.hello();
        }

        public Task JoinGroup(string group) {
            return Groups.Add(Context.ConnectionId, group);
        }

        #region Helpdesk
        private readonly ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public override Task OnConnected() {
            string name = Context.User.Identity.Name;
            if (!string.IsNullOrWhiteSpace(name)) {
                _connections.Add(name, Context.ConnectionId);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected() {
            string name = Context.User.Identity.Name;
            if (!string.IsNullOrWhiteSpace(name)) {
                _connections.Remove(name, Context.ConnectionId);
            }
            return base.OnDisconnected();
        }

        public override Task OnReconnected() {
            string name = Context.User.Identity.Name;
            if (!string.IsNullOrWhiteSpace(name)) {
                if (!_connections.GetConnections(name).Contains(Context.ConnectionId)) {
                    _connections.Add(name, Context.ConnectionId);
                }
            }
            return base.OnReconnected();
        }

        public void Help(HelpRequest request) {
            Clients.Group("HelpDesk").helpRequested(request);
        }

        public void PeerToPeerMessage(Message message) {
            message.From = Context.User.Identity.Name;
            Clients.User(message.To).peerToPeerMessage(message);
        }
        #endregion

        #region Business
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
        #endregion
    }
}