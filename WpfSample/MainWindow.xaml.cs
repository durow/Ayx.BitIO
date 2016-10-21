using Ayx.BitIO;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfSample.Codec;

namespace WpfSample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        IDecoder decoder;
        IEncoder encoder;
        byte[] buff;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Char6Radio.IsChecked == true)
            {
                SetCodec6();
            }

            if (Char7Radio.IsChecked == true)
            {
                SetCodec7();
            }
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataText.Text))
            {
                EncodeText.Clear();
                return;
            }
            buff = encoder.Encode(DataText.Text);
            EncodeText.Text = Encoding.ASCII.GetString(buff);
            ShowBinString();
        }

        private void Char7Radio_Checked(object sender, RoutedEventArgs e)
        {
            SetCodec7();
        }

        private void Char6Radio_Checked(object sender, RoutedEventArgs e)
        {
            SetCodec6();
        }

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataText.Text))
            {
                DecodeText.Clear();
                return;
            }

            DecodeText.Text = decoder.Decode(buff);
        }

        private void ShowBinString()
        {
            Task.Factory.StartNew(() =>
            {
                if (buff == null) return;
                var reader = new BitReader(buff);
                var byteLen = reader.Length / 8;
                var format = "D" + byteLen.ToString().Length;
                var bin = new StringBuilder();
                for (int i = 0; i < byteLen; i++)
                {
                    bin.Append(i.ToString(format)).Append(" ").Append(reader.ReadBinString(8)).Append("\r\n");
                }
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    BinText.Text = bin.ToString();
                }));
            });
        }

        private void SetCodec6()
        {
            decoder = Decoder6.Default;
            encoder = Encoder6.Default;
        }

        private void SetCodec7()
        {
            decoder = Decoder7.Default;
            encoder = Encoder7.Default;
        }
    }
}
