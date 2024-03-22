using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isProcessing = false;
        bool canceler = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            if (!isProcessing)
            {
                isProcessing = true;
                button.Content = "キャンセル";

                await Do();

                isProcessing = false;
                progressBar.Value = 0;
                button.Content = "実行";
            }
            else
            {
                canceler = true;
                button.Content = "実行";
            }
        }

        private async Task Do()
        {
            for(int i = 0; i < 10; i++)
            {
                if (canceler)
                {
                    progressBar.Value = 0;
                    isProcessing = false;
                    canceler = false;
                    MessageBox.Show("キャンセル");
                    return;
                }

                await Task.Delay(1000);
                progressBar.Value = i+1;
            }

            MessageBox.Show("処理完了");
        }
    }
}