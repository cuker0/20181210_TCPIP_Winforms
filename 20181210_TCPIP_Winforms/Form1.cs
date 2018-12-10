using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20181210_TCPIP_Winforms
{
    public partial class Form1 : Form
    {
        private TcpClient _tcpclient;

        public Form1()
        {
            InitializeComponent();
        }

        public string Connect(String server, Int32 port, byte[] message)
        {
            String responseData = String.Empty;

            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.          
       
               // TcpClient _tcpclient = new TcpClient(server, port);
                NetworkStream stream = _tcpclient.GetStream();

                // Translate the passed message into ASCII and store it as a Byte array.
                byte[] data =message;

                BCCHelper.CalculateAndAppendBcc(ref data);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length); // czekamy na informacje zwrotna z odpowiedzia

               //  Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.
                // Buffer to store the response bytes.

                data = new byte[256];

                // String to store the response ASCII representation.

                // Read the first batch of the TcpServer response bytes.

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
               // Console.WriteLine("Received: {0}", responseData);
                 
               // Close everything.
              //  stream.Close();
              //  _tcpclient.Close();

               
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            return responseData;
        }

        public string Send(String server, Int32 port, byte[] message)
        {
            String responseData = String.Empty;

            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.          


                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();

                // Translate the passed message into ASCII and store it as a Byte array.
                byte[] data = message;

                BCCHelper.CalculateAndAppendBcc(ref data);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length); // czekamy na informacje zwrotna z odpowiedzia

                //  Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.
                // Buffer to store the response bytes.

                data = new byte[256];

                // String to store the response ASCII representation.



                // Read the first batch of the TcpServer response bytes.

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                // Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();


            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            return responseData;
        }


        async Task<string> SendAsync(String server, Int32 port, String message)
        {
            String responseData = String.Empty;

            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.          


                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();

                // Translate the passed message into ASCII and store it as a Byte array.
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                BCCHelper.CalculateAndAppendBcc(ref data);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                await stream.WriteAsync(data, 0, data.Length); // czekamy na informacje zwrotna z odpowiedzia

                //  Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.
                // Buffer to store the response bytes.

                data = new byte[256];

                // String to store the response ASCII representation.



                // Read the first batch of the TcpServer response bytes.

                Int32 bytes = await stream.ReadAsync(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                // Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }

            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

           return responseData;
        }

        private void button1_ClickAsync(object sender, EventArgs e)
        {
            byte[] bytesData = Encoding.ASCII.GetBytes(textBox1.Text);
            textBox2.Text= Connect(textBox3.Text, (int)numericUpDown1.Value, bytesData);
            //textBox2.Text = reply.Result;

         //   string reply = SendAsync(textBox3.Text, (int)numericUpDown1.Value, textBox1.Text);
          //  textBox2.Text = reply;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] hexData = StringToByteArray(textBox4.Text);
            textBox2.Text = Connect(textBox3.Text, (int)numericUpDown1.Value, hexData);
        }

        public static byte[] StringToByteArray(string hex)

        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _tcpclient = new TcpClient(textBox3.Text, (int)numericUpDown1.Value);

            if (_tcpclient.Connected)
            {
                button3.BackColor = Color.Green;
            }
            else
            {
                button3.BackColor = Color.Red;
            }
            
        }
    }
}
