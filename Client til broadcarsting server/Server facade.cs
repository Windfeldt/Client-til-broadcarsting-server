using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;
namespace Client_til_broadcarsting_server
{
    class Server_facade
    {
        TcpClient client;
        NetworkStream nWS;
        StreamReader sR;
        StreamWriter sW;
        int port;
        string servername;

        public Server_facade(int port, string servername)
        {
            this.port = port;
            this.servername = servername;
            client = new TcpClient();
            ConnectToSever();
            nWS = client.GetStream();
            sR = new StreamReader(nWS);
            sW = new StreamWriter(nWS);

            Thread sendThred = new Thread(Send);
            sendThred.Start();
            Thread reciveThread = new Thread(Recive);
            reciveThread.Start();
        }
        private void ConnectToSever()
        {
            client.Connect(servername, port);
        }

        private void Close()
        {
            client.GetStream().Close();
        }

        private void Dispose()
        {
            client.Close();
        }

        public void Recive()
        {
            string recivemessage;
            while (true)
            {
                recivemessage = sR.ReadLine();
                Console.WriteLine(recivemessage);
            }
        }

        public void Send()
        {
            string message;
            while (true)
            {
                message = Console.ReadLine();
                sW.WriteLine(message);
                sW.Flush();
            }
        }
    }
}
