using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZMQ;
using Prowl;
using Growl.Connector;

namespace MessagePipe
{
    public partial class MessagePipeForm : Form
    {
        private string RTD_SUBSCRIBER_ADDRESS = "tcp://127.0.0.1:5000";
        private string RTD_PUBLISHER_ADDRESS = "tcp://127.0.0.1:5001";

        private string APP_NAME = "Hydra Message Pipe";

        private ZeroMqServer _zmqObject;
        private Thread _zmqThread;

        private ProwlClient _prowl;

        private GrowlConnector _growl = new GrowlConnector();

        public MessagePipeForm()
        {
            InitializeComponent();

            //startProwl(APP_NAME);
            //registerGrowlApp();
            startZMQ();
            _zmqObject.OnMessageReceived += MessagePipeForm_OnMessageReceived;
        }

        void MessagePipeForm_OnMessageReceived(string msg)
        {
            const int MAX_LIST_ITEMS = 100;

            //Console.WriteLine("#### MESSAGE PIPE: {0}", msg);
            string timestampedMessage = DateTime.Now.ToShortTimeString() + "  " + msg;
            this.Invoke((MethodInvoker)delegate() { listDisplay.Items.Insert(0, timestampedMessage); });
            this.Invoke((MethodInvoker)delegate() { if (listDisplay.Items.Count > MAX_LIST_ITEMS) listDisplay.Items.RemoveAt(listDisplay.Items.Count-1); });
        }

        private void registerGrowlApp()
        {
            Growl.Connector.Application app = new Growl.Connector.Application(APP_NAME);
            //app.Icon = 

            var notifyMessage = new NotificationType("NOTIFY_MESSAGE", "A Growl message arrived");

            _growl.Register(app, new NotificationType[] { notifyMessage });
        }

        private void sendGrowlMessage(string notificationTypeName, string msg1, string msg2)
        {
            string id = "";
            Notification notification = new Notification(APP_NAME, "NOTIFY_MESSAGE", id, msg1,msg2);
            _growl.Notify(notification);
        }

        private void sendProwlMessage(string prowlEvent, string description, ProwlNotificationPriority priority = ProwlNotificationPriority.Normal)
        {
            var notification = new ProwlNotification();
            notification.Event = prowlEvent;
            notification.Description = description;
            notification.Priority = priority;
            _prowl.PostNotification(notification);
        }

        private void startProwl(string applicationName)
        {
            var config = new ProwlClientConfiguration();
            config.ApplicationName = applicationName;
            config.ApiKeychain = "75fcd3a230ac062616e14c15ed5f79bdb5d0d199";
            config.BaseUrl = "https://api.prowlapp.com/publicapi";
            config.ProviderKey = "";
            _prowl = new ProwlClient(config);            
        }

        private void startZMQ()
        {
            _zmqObject = new ZeroMqServer(RTD_SUBSCRIBER_ADDRESS, RTD_PUBLISHER_ADDRESS);
            _zmqThread = new Thread(_zmqObject.DoWork);
            _zmqThread.Start();

            lblInfo.Text = string.Format("Subscribed to {0}      Publishing to {1}", RTD_SUBSCRIBER_ADDRESS, RTD_PUBLISHER_ADDRESS);
        }

        private void stopZMQ()
        {
            _zmqObject.RequestStop();
            _zmqThread.Join();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sendProwlMessage("MyEvent", "something happened here...");
            sendGrowlMessage("NOTIFY_MESSAGE", "here is some text", "and then a more detailed description of the message...");
        }

    } // END OF CLASS 

    public class ZeroMqServer
    {
        private string _rtdSubscriberAddress;   // = "tcp://127.0.0.1:5000";
        private string _rtdPublisherAddress;    // = "tcp://127.0.0.1:5001";

        public delegate void MessageReceived(string msg);
        public event MessageReceived OnMessageReceived;


        // Volatilie is used as hint to the compiler that this data member will be accessed by multiple threads
        private volatile bool _shouldStop;

        //private Context _context;
        //private Socket _socket;
        //private string _rtdData = "(waiting for data)";
        public volatile string _topic = "(wait)";
        public volatile string _data = "(wait)";

        private Dictionary<string, string> _rtdUpdates = new Dictionary<string, string>();

        public ZeroMqServer(string subscriberAddress, string publisherAddress)
        {
            _rtdSubscriberAddress = subscriberAddress;
            _rtdPublisherAddress = publisherAddress;
        }

        public void DoWork()
        {
            using (var context = new Context())
            {
                using (Socket subsock = context.Socket(SocketType.SUB), pubsock = context.Socket(SocketType.PUB))
                {
                    pubsock.Bind(_rtdPublisherAddress);

                    subsock.Subscribe("", Encoding.UTF8);
                    //socket.Connect(ZEROMQ_ENDPOINT);
                    subsock.Bind(_rtdSubscriberAddress);

                    while (!_shouldStop)
                    {
                        Thread.Sleep(100);
                        var rcvdMsg = subsock.Recv(Encoding.UTF8);
                        
                        //Console.WriteLine("#### MESSAGE PIPE: {0}", rcvdMsg);
                        if (OnMessageReceived != null) OnMessageReceived(rcvdMsg);

                        pubsock.Send(rcvdMsg, Encoding.UTF8);
                    }
                }

            }
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }
    } // END OF CLASS ZeroMqServer

} // END OF NAMESPACE
