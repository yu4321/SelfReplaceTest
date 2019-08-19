using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace SelfReplaceTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        int lastVersion = 0;
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("UpdateHistory.txt"))
            {
                string[] datas = File.ReadAllLines("UpdateHistory.txt");
                lastVersion = int.Parse(datas[0]);
            }
            else
            {
                btn.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("UpdateNotifier.txt"))
            {
                string[] datas = File.ReadAllLines("UpdateNotifier.txt");
                if (int.Parse(datas[0]) > lastVersion)
                {
                    if (datas.Length > 2)
                    {
                        Util.DirectoryCopyWithRenameFile(datas[1], datas[2], true);
                    }
                    else
                    {
                        Util.DirectoryCopyWithRenameFile(datas[1], Directory.GetCurrentDirectory(), true);
                    }
                    File.WriteAllText("UpdateHistory.txt", datas[0]);
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show($"최신 버전 {datas[0]} 현재 버전 {lastVersion}\n업데이트 불필요");
                }
            }
        }
    }
}
