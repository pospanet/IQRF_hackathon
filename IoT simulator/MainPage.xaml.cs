using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoT_simulator
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly Random _random = new Random();
        private bool _isRunning;
        private Task _task;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            if (!_isRunning)
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    button.IsEnabled = false;
                    button.Content = "Stop";
                    textBox.IsReadOnly = true;
                    _isRunning = true;
                    var client = DeviceClient.CreateFromConnectionString(textBox.Text, TransportType.Http1);
                    _tokenSource = new CancellationTokenSource();
                    _task = DoWork(client, _tokenSource.Token, _regex.Match(textBox.Text).Groups["device"].Value);
                    button.IsEnabled = true;
                });
            else
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    _tokenSource.Cancel();
                    button.IsEnabled = false;
                    button.Content = "Start";
                    textBox.IsReadOnly = false;
                    _isRunning = false;
                    button.IsEnabled = true;
                });
        }

        readonly Regex _regex = new Regex(@"DeviceId=(?<device>[^;]*);");

        private async Task DoWork(DeviceClient client, CancellationToken token, string deviceName)
        {
            while (!token.IsCancellationRequested)
            {
                var data = new Data {Value = _random.Next(-10, DateTime.Now.Minute + 10), Device = deviceName, Time = DateTime.Now };
                var dataString = JsonConvert.SerializeObject(data);
                var message = new Message(Encoding.UTF8.GetBytes(dataString));
                await Task.Delay(TimeSpan.FromSeconds(1), token);
                await client.SendEventAsync(message);
            }
        }
    }
}