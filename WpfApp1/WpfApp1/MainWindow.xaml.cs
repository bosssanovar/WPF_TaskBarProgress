using System.Text;
using System.Threading;
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
        CancellationTokenSource? cancellationToken;

        public MainWindow()
        {
            InitializeComponent();

            TaskbarItemInfo = new System.Windows.Shell.TaskbarItemInfo();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            if (!isProcessing)
            {
                isProcessing = true;
                button.Content = "キャンセル";

                TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
                TaskbarItemInfo.ProgressValue = 0;

                cancellationToken = new CancellationTokenSource();
                var result = await DoAsync(cancellationToken);
                if (result)
                {
                    MessageBox.Show("処理完了");
                }
                else
                {
                    TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Error;
                    progressBar.Foreground = Brushes.Red;
                    MessageBox.Show("キャンセル");
                }

                init();
            }
            else
            {
                cancellationToken?.Cancel();
            }
        }

        private async Task<bool> DoAsync(CancellationTokenSource cancellationToken)
        {
            for (int i = 0; i < 10; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }

                await Task.Delay(1000);

                progressBar.Value = i + 1;
                TaskbarItemInfo.ProgressValue = 0.1 * (i + 1);
            }

            return true;
        }

        private void init()
        {
            progressBar.Value = 0;
            progressBar.Foreground = Brushes.LightGreen;
            isProcessing = false;
            button.Content = "実行";

            TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
        }
    }
}