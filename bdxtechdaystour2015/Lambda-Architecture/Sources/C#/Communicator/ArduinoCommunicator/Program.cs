using Microsoft.ServiceBus.Messaging;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System;

namespace ArduinoCommunicator
{
    class Program
    {
        private static EventHubClient client = EventHubClient.CreateFromConnectionString("Endpoint=sb://<yourservicebus>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=<your access key>", "techdaystour");
        static void Main(string[] args)
        {
            string com = SerialPort.GetPortNames().FirstOrDefault();
            SerialPort serialPort = new SerialPort(com);
            serialPort.BaudRate = 9600;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
            serialPort.Open();
            serialPort.Write("1");
            Console.ReadLine();
            serialPort.Close();
        }

        private static void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort comPort = (SerialPort)sender;
            if (comPort.IsOpen)
            {
                string json = comPort.ReadLine();
                Console.WriteLine(json);
                EventData @event = new EventData(Encoding.UTF8.GetBytes(json));
                client.Send(@event);
            }
        }
    }
}
