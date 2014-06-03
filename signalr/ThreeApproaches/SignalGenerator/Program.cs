using System;

using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace SignalGenerator
{
    public class Program
    {
        private static IHubProxy _messageHub;
        public static void Main(string[] args) {
            Initialize();
            Menu();
            string msg;
            while ((msg = Console.ReadLine()) != "0") {
                if (msg == "1") {
                    CreateInvoice();
                }
                if (msg == "2") {
                    Flood();
                }
            }
        }

        private static void Menu() {
            Console.WriteLine(@"
Choose an entry:
1. Create invoice.
2. Generate random signals.
0. Exit.
");
        }

        private static void Initialize() {
            var hubConnection = new HubConnection("http://localhost:1064");
            _messageHub = hubConnection.CreateHubProxy("MessageHub");
            // chat.On<string, string>("broadcastMessage", (name, message) => { Console.Write(name + ": "); Console.WriteLine(message); });
            hubConnection.Start().Wait();
        }
        
        private static void CreateInvoice() {
            Console.WriteLine("Enter invoice number. Empty to go back to menu.");
            string msg;
            while (!string.IsNullOrWhiteSpace(msg = Console.ReadLine())) {
                _messageHub.Invoke("InvoiceCreated", msg); //, hubConnection.ConnectionId);
            }
        }


        public static string[] _messages = { "InvoiceCreated", "DistributionRecordCreated", "BillingRecordCreated", "JournalEntryCreated" };
        public static int _maxWaitMs = 500;
        private static void Flood(){
            Console.Write("How many seconds of flooding? ");
            var answer = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(answer)) return;
            var seconds = int.Parse(answer);
            var start = DateTime.Now;
            var rnd = new Random();
            const string number = "C-{0}-{1}";
            int counter = 0, endPLength = _messages.Length;

            while ((DateTime.Now - start).TotalSeconds < seconds) {
                string endPoint = _messages[rnd.Next(endPLength)];
                _messageHub.Invoke(endPoint, string.Format(number, endPoint.Substring(0, 1), counter++));
                Thread.Sleep(rnd.Next(_maxWaitMs));
            }
            Console.WriteLine("Sent {0} messages.", counter);
        }
    }
}
