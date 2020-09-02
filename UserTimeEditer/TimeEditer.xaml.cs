using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserTimeEditer
{
    /// <summary>
    /// TimeEditer.xaml 的交互逻辑
    /// </summary>
    public partial class TimeEditer : UserControl, INotifyPropertyChanged
    {
        public bool IsJump = false;
        public TimeEditer()
        {
            InitializeComponent();
            DataContext = this;
        }
        public void Delay()
        {
            IsJump = false;
            int setPara = (Hour * 3600 + Minute * 60 + Second) * 1000;//单位毫秒
            int start = Environment.TickCount;
            while (true)//毫秒
            {
                int time = Math.Abs(Environment.TickCount - start);
                if (time >= setPara || IsJump == true)
                {
                    IsJump = false;
                    break;
                }
                int res_time = (setPara - time) / 1000;//剩余时间 单位秒
                Hour = res_time / 3600;
                Minute = res_time % 3600 / 60;
                Second = res_time % 60;
                Thread.Sleep(10);
                myBorder.Dispatcher.Invoke(new Action(() =>
                {
                    TimeForeground = new SolidColorBrush(Colors.Red);//延时倒计时改变前景色
                }));
            }
            myBorder.Dispatcher.Invoke(new Action(() =>
            {
                TimeForeground = new SolidColorBrush(Colors.Black);//延时结束后恢复前景色
            }));
            //恢复之前设定的时间
            Hour = setPara / 1000 / 3600;
            Minute = setPara / 1000 % 3600 / 60;
            Second = setPara / 1000 % 60;
        }

        public void Delay(SolidColorBrush brush)
        {
            IsJump = false;
            int setPara = (Hour * 3600 + Minute * 60 + Second) * 1000;//单位毫秒
            int start = Environment.TickCount;
            while (true)//毫秒
            {
                int time = Math.Abs(Environment.TickCount - start);
                if (time >= setPara || IsJump == true)
                {
                    IsJump = false;
                    break;
                }
                int res_time = (setPara - time) / 1000;//剩余时间 单位秒
                Hour = res_time / 3600;
                Minute = res_time % 3600 / 60;
                Second = res_time % 60;
                Thread.Sleep(10);
                myBorder.Dispatcher.Invoke(new Action(() =>
                {
                    TimeForeground = brush;//延时倒计时改变前景色
                }));
            }
            myBorder.Dispatcher.Invoke(new Action(() =>
            {
                TimeForeground = new SolidColorBrush(Colors.Black);//延时结束后恢复前景色
            }));
            //恢复之前设定的时间
            Hour = setPara / 1000 / 3600;
            Minute = setPara / 1000 % 3600 / 60;
            Second = setPara / 1000 % 60;
        }
        private int _fontTime = 0;
        public int FontTime
        {
            get { return _fontTime; }
            set { _fontTime = value; NotifyPropertyChanged(); }
        }
        private SolidColorBrush _backgroud;
        public SolidColorBrush TimeBackgroud
        {
            get { return _backgroud; }
            set { _backgroud = value; NotifyPropertyChanged(); }
        }

        private SolidColorBrush _foreground = new SolidColorBrush(Colors.Black);
        public SolidColorBrush TimeForeground
        {
            get { return _foreground; }
            set { _foreground = value; NotifyPropertyChanged(); }
        }
        private int _hour = 0;
        public int Hour
        {
            get { return _hour; }
            set { _hour = value; NotifyPropertyChanged(); }
        }

        private int _minute = 0;
        public int Minute
        {
            get { return _minute; }
            set { _minute = value; NotifyPropertyChanged(); }
        }

        private int _second = 0;
        public int Second
        {
            get { return _second; }
            set { _second = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void tb_HourPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //Regex re = new Regex("[^1-9]+");//正则表达式
            //e.Handled = re.IsMatch(e.Text);

            //decimal score;
            //TextBox textBox = e.Source as TextBox;
            ////e.Text是当前输入的单个字符 e.Source.Text是整个文本框输入的字符串集合
            //if (decimal.TryParse(textBox.Text, out score))
            //{
            //    if (score < 0 || score > 24)
            //    {
            //        MessageBox.Show("数值超出范围，请输入0-24内的整数");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("格式错误，请输入0-24内的整数");

            //}
        }


        private void tb_HourTextChanged(object sender, TextChangedEventArgs e)
        {
            //Regex re = new Regex("[^1-9]+");//正则表达式
            //e.Handled = re.IsMatch(e.Text);

            decimal score;
            TextBox textBox = e.Source as TextBox;
            //e.Text是当前输入的单个字符 e.Source.Text是整个文本框输入的字符串集合
            if (textBox.Text == "")
            {
                Hour = 0;
                return;
            }
            if (decimal.TryParse(textBox.Text, out score))
            {
                if (score < 0 || score > 23)
                {
                    Hour = 0;
                    MessageBox.Show("数值超出范围，请输入0-23内的整数");
                }
            }
            else
            {
                Hour = 0;
                MessageBox.Show("格式错误，请输入0-23内的整数");
            }
        }

        private void tb_MinuteTextChanged(object sender, TextChangedEventArgs e)
        {
            decimal score;
            TextBox textBox = e.Source as TextBox;
            //e.Text是当前输入的单个字符 e.Source.Text是整个文本框输入的字符串集合
            if (textBox.Text == "")
            {
                Minute = 0;
                return;
            }
            if (decimal.TryParse(textBox.Text, out score))
            {
                if (score < 0 || score > 59)
                {
                    Minute = 0;
                    MessageBox.Show("数值超出范围，请输入0-59内的整数");
                }
            }
            else
            {
                Minute = 0;
                MessageBox.Show("格式错误，请输入0-59内的整数");
            }
        }

        private void tb_SecondTextChanged(object sender, TextChangedEventArgs e)
        {
            decimal score;
            TextBox textBox = e.Source as TextBox;
            //e.Text是当前输入的单个字符 e.Source.Text是整个文本框输入的字符串集合
            if (textBox.Text == "")
            {
                Second = 0;
                return;
            }
            if (decimal.TryParse(textBox.Text, out score))
            {
                if (score < 0 || score > 59)
                {
                    Second = 0;
                    MessageBox.Show("数值超出范围，请输入0-59内的整数");
                }
            }
            else
            {
                Second = 0;
                MessageBox.Show("格式错误，请输入0-59内的整数");
            }
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            tb.SelectAll();
            tb.PreviewMouseDown -= new MouseButtonEventHandler(OnPreviewMouseDown);//所实现的功能是当第二次单击的时候，不再是全选文字，而是显示光标。
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            tb.Focus();
            e.Handled = true;//由这里引发GotFocus事件 并且设置Handled 标记阻止路由事件继续传播
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            tb.PreviewMouseDown += new MouseButtonEventHandler(OnPreviewMouseDown);
        }
    }
}

