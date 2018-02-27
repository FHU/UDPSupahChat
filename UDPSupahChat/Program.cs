using System;
using System.Net;
using System.Threading.Tasks;

namespace UDPSupahChat
{
    class Program
    {

        public const int PORT = 12121;

        static void Main(string[] args)
        {
            // create the server
            var server = new UDPListener(new IPEndPoint(IPAddress.Any, PORT));

            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var received = await server.Receive();
                    server.Reply("I heard that, " + received.Sender, received.Sender);
                    Console.WriteLine("Server got a message from "+ received.Sender);
                    if (received.Message == "quit")
                    {
                        break;
                    }
                }
            });

            // create the client 
            var client = UDPUser.ConnectTo("10.5.32.198", PORT);

            Task.Factory.StartNew( async () =>
            {
                while (true)
                {
                    try
                    {
                        var received = await client.Receive();
                        Console.WriteLine(received.Message);
                        if (received.Message.Contains("quit"))
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            });

            //type ahead :-)
            string read;
            do
            {
                read = Console.ReadLine();
                client.Send(read);
            } while (read != "quit");
        }
    }
}
