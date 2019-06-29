using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZMQ;

namespace ZeroMqServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                /*using (var socket = context.Socket(SocketType.REP))
                {
                    socket.Bind("tcp://127.0.0.1:5000");

                    while (true)
                    {
                        Thread.Sleep(1000);
                        var rcvdMsg = socket.Recv(Encoding.UTF8);
                        Console.WriteLine("SERVER Received: " + rcvdMsg);
                        var replyMsg = "This is your reply!";
                        Console.WriteLine("SERVER Sending: " + replyMsg);
                        socket.Send(replyMsg, Encoding.UTF8);
                    }
                } // socket*/

                using (var socket = context.Socket(SocketType.SUB))
                {
                    //socket.Subscribe(Encoding.UTF8.GetBytes("russia"));
                    socket.Subscribe("", Encoding.UTF8);
                    socket.Connect("tcp://127.0.0.1:5000");
                    
/*# Subscribe to zipcode, default is NYC, 10001
topicfilter = "10001"
socket.setsockopt(zmq.SUBSCRIBE, topicfilter)*/

                    while (true)
                    {
                        //Thread.Sleep(1000);
                        Console.WriteLine("Waiting for SUB receive...");
                        var rcvdMsg = socket.Recv(Encoding.UTF8);
                        Console.WriteLine("SUBSCRIBED DATA RECEIVED: {0}", rcvdMsg);
                        //string[] split = rcvdMsg.Split();
                        //_topic = split[0];
                        //_data = split[1];
                    }
                }

            } // context
        }

    } // CLASS
} // NAMESPACE
