using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SignalGenerator
{
    public class Program
    {
        public static void Main(string[] args) {
            var hubConnection = new HubConnection("http://localhost:1064");
            var messageHub = hubConnection.CreateHubProxy("MessageHub");
            // chat.On<string, string>("broadcastMessage", (name, message) => { Console.Write(name + ": "); Console.WriteLine(message); });
            hubConnection.Start().Wait();
            string msg = null;

            while ((msg = Console.ReadLine()) != null) {
                messageHub.Invoke("InvoiceCreated", msg); //, hubConnection.ConnectionId);
            }

        }
    }
}
