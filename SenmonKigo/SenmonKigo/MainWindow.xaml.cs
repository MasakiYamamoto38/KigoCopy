using System;
using System.Collections.Generic;
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
using System.IO;
using System.Collections;

namespace SenmonKigo
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        public void MyInitializeComponent()
        {
            KigoSearch.inuptKigoData(@"C:\Users\goodw\Documents\Visual Studio 2017\Projects\SenmonKigo\SenmonKigo\bin\Debug\専門学校学科記号.csv");

            this.button1.Content = "該当する学科はありません";
            this.button2.Content = "該当する学科はありません";
            this.button1.IsEnabled = false;
            this.button2.IsEnabled = false;
            this.Topmost = true;
            this.Title = "C#産App v0.1";
        }

        private List<string> nowKigoList = new List<string>();
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            nowKigoList.Clear();
            listBox1.Items.Clear();

            if (textBox1.Text == "")
            {
                button1.IsEnabled = false; button2.IsEnabled = false;
                button1.Content = "該当する学科はありません"; button2.Content = "該当する学科はありません";
                var km = KigoSearch.kigoManager;
                for(int i = 0; i < km.colleges.Count; i++)
                {
                    var ini = km.initials[i].data;
                    var str = "-"; if (ini[0] != null) str = ini[0];

                    listBox1.Items.Add(
                        km.colleges[i] + "カレッジ  |　"+ km.departments[i] + "   | " + "\"" + str  + "\"などと入力すれば候補に出ます");
                    
                }

                return;
            }

            var ls = KigoSearch.getResult(textBox1.Text);
            if (ls.Length == 0) { button1.IsEnabled = false; button2.IsEnabled = false; return; }
            if (ls.Length == 1)
            {
                button1.IsEnabled = true; button2.IsEnabled = false;
                button1.Content = ls[0].department + " : " + ls[0].kigo;
                nowKigoList.Add(ls[0].kigo);
            }
            if (ls.Length >= 2)
            {
                button1.IsEnabled = true; button2.IsEnabled = true;
                button1.Content = ls[0].department + " : " + ls[0].kigo; ;
                button2.Content = ls[1].department + " : " + ls[1].kigo; ;
                nowKigoList.Add(ls[0].kigo);
                nowKigoList.Add(ls[1].kigo);

                for(int i = 2; i < ls.Length; i++)
                {
                    listBox1.Items.Add(
                        ls[i].department + " : " + ls[i].kigo);
                }
            }

            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (nowKigoList.Count < 1) return;
            Clipboard.SetText(nowKigoList[0]);
            textBox1.Text = "";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (nowKigoList.Count < 2) return;
            Clipboard.SetText(nowKigoList[1]);
            textBox1.Text = "";
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var idx = listBox1.SelectedIndex;
            if (idx == -1) return;

        }
    }
}
