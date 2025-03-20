using Project1.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Star> list = new ObservableCollection<Star>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string n = name.Text;
            string date = dob.Text;
            string des = desc.Text;
            bool ismale = isMale.IsChecked ?? false;
            string nat = nal.Text;

            var star = new Star
            {
                Description = des,
                Dob = DateTime.Parse(date),
                Name = n,
                Male = ismale,
                Nationality = nat
            };

            list.Add(star);
            starGrid.ItemsSource = list;
            starGrid.Items.Refresh();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            //try
            //{
            //    TcpClient client = new TcpClient("127.0.0.1", 5000);
            //    NetworkStream stream = null;

            //    JsonArray jsonArray = new JsonArray();

            //    foreach (var star in list)
            //    {
            //        JsonObject jsonObject = new JsonObject();
            //        jsonObject.Add("Name", star.Name);
            //        jsonObject.Add("Dob", star.Dob);
            //        jsonObject.Add("Description", star.Description);
            //        jsonObject.Add("Male", star.Male);
            //        jsonObject.Add("Nationality", star.Nationality);
            //        jsonArray.Add(jsonObject);
            //    }

            //    Byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonArray.ToString());

            //    // Get a client stream for reading and writing 
            //    stream = client.GetStream();

            //    // Send the message to the connected TcpServer
            //    stream.Write(data, 0, data.Length);
            //    //Console.WriteLine($"Sent: {message}");

            //    // Receive the TcpServer response
            //    // Use Buffer to store the response bytes

            //    data = new Byte[256];

            //    // Read the first batch of the TcpServer response bytes
            //    int bytes = stream.Read(data, 0, data.Length);
            //    string responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //    //Console.WriteLine($"Received: {responseData}");

            //    // Shutdown and end connection
            //    client.Close();

            //    MessageBox.Show(responseData);
            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            string serverIp = "127.0.0.1";
            int port = 5000;
            try
            {
                using TcpClient client = new(serverIp, port);
                using NetworkStream stream = client.GetStream();

                // send data
                string jsonData = JsonSerializer.Serialize(list);
                byte[] buffer = Encoding.UTF8.GetBytes(jsonData);
                await stream.WriteAsync(buffer);
                Console.WriteLine("List of objects sent to server.");

                // process response
                byte[] responseBuffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(responseBuffer, 0, responseBuffer.Length);
                string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);

                MessageBox.Show(response);

            }
            catch (Exception ex)
            {
            }
        }
    }
}