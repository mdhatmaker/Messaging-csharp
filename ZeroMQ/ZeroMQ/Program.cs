using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZMQ;

namespace ZeroMqClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                /*using (var socket = context.Socket(SocketType.REQ))
                {
                    socket.Connect("tcp://127.0.0.1:5000");

                    while (true)
                    {
                        Thread.Sleep(1000);
                        var reqMsg = "this is a request";
                        Console.WriteLine("CLIENT Sending: " + reqMsg);
                        socket.Send(reqMsg, Encoding.UTF8);
                        var replyMsg = socket.Recv(Encoding.UTF8);
                        Console.WriteLine("CLIENT Received: " + replyMsg);
                    }
                } // socket*/

                using (var socket = context.Socket(SocketType.PUB))
                {
                    socket.Bind("tcp://127.0.0.1:5000");

                    int messagedata = 88;

                    while (true)
                    {                        
                        Thread.Sleep(1000);  

                        var topic = "russia";
                        ++messagedata;
                        var sendMsg = string.Format("{0} {1}", topic, messagedata);
                        Console.WriteLine("publishing: " + sendMsg);
                        socket.Send(sendMsg, Encoding.UTF8);

                        topic = "turkey";
                        ++messagedata;
                        sendMsg = string.Format("{0} {1}", topic, messagedata);
                        Console.WriteLine("publishing: " + sendMsg);
                        socket.Send(sendMsg, Encoding.UTF8);

                    }
                } // socket

            } // context
        }

    } // CLASS
} // NAMESPACE
