using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class P2PServer
{
    static void Main(string[] args)
    {
        // Create an IP endpoint and a TCP listener
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int port = 1234;
        TcpListener listener = new TcpListener(ip, port);

        // Start listening for incoming connections
        listener.Start();
        Console.WriteLine("P2P server started on " + ip + ":" + port);

        while (true)
        {
            // Wait for a client to connect
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected: " + client.Client.RemoteEndPoint);

            // Handle the client connection
            HandleClient(client);
        }
    }

    static void HandleClient(TcpClient client)
    {
        // Get the client's network stream
        NetworkStream stream = client.GetStream();

        // Read the message sent by the client
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);
        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        // Print the message and close the connection
        Console.WriteLine("Received message: " + message);
        client.Close();
    }
}
