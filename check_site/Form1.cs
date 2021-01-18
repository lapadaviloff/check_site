using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


namespace check_site
{
    public partial class Form1 : Form
    {
        private FormWindowState _OldFormState;
        private Icon empetyIcon;
        private Icon checkMailIcon;
        Timer timer = new Timer();

        public Form1()
        {
            
            
                InitializeComponent();
            timer.Tick += new EventHandler(RefreshLabel);
            timer.Interval = 1000; // Здесь измени интервал на 5000 (5 сек)
            timer.Start();
            // InitializeComponent();
            //программа в правом нижнем углу
            this.StartPosition = FormStartPosition.Manual;
                var wArea = Screen.PrimaryScreen.WorkingArea;
                this.Left = wArea.Width + wArea.Left - this.Width;
                this.Top = wArea.Height + wArea.Top - this.Height;

                //инициализация всплывающего сообщения

                empetyIcon = new Icon("icon/empety.ico");
                checkMailIcon = new Icon("icon/checkMail.ico");
                notifyIcon1.Icon = SystemIcons.Exclamation;
                notifyIcon1.BalloonTipTitle = "Balloon Tip Title";
                notifyIcon1.BalloonTipText = "Balloon Tip Text.";
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(30000);
            

               
                //задаем всплывающий текст-подсказку (появляется при наведении указателя на иконку в трее)
                notifyIcon1.Text = "Текст-подсказка";
                //устанавливаем значок, отображаемый в трее:
                //либо один из стандартных:
                //notifyIcon1.Icon = SystemIcons.Error;
                //либо свой из файла:
                notifyIcon1.Icon = checkMailIcon;
                //подписываемся на событие клика мышкой по значку в трее
                notifyIcon1.MouseClick += new MouseEventHandler(_notifyIcon_MouseClick);
                //подписываемся на событие изменения размера формы
                this.Resize += new EventHandler(FormForTray_Resize);

              
            
            
        }
        void _notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //проверяем, какой кнопкой было произведено нажатие
            if (e.Button == MouseButtons.Left)//если левой кнопкой мыши
            {
                //проверяем текущее состояние окна
                if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)//если оно развернуто
                {
                    //сохраняем текущее состояние
                    _OldFormState = WindowState;
                    //сворачиваем окно
                    WindowState = FormWindowState.Minimized;
                    //скрываться в трей оно будет по событию Resize (изменение размера), которое сгенерировалось после минимизации строчкой выше
                }
                else//в противном случае
                {
                    //и показываем на нанели задач
                    Show();
                    //разворачиваем (возвращаем старое состояние "до сворачивания")
                    WindowState = _OldFormState;
                }
            }
        }

        ///
        /// обрабатываем событие изменения размера
        ///
        void FormForTray_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)//если окно "свернуто"
            {
                //то скрываем его
                Hide();
            }
        }

        public bool TestSite(string url)
        {
            
            Uri uri = new Uri(url);


            
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
              //  label1.Text = httpWebResponse.StatusDescription;
                httpWebResponse.Close();
            }
            catch
            {
                return false;
            }
            return true;
        
            
            }

        public void RefreshLabel(object sender, EventArgs e)
        {

            //label1.Text = DateTime.Now.ToString("HH:mm:ss"); // Сюда вставь свое обновление label
          
        if (TestSite("http://miam-devsoft.ru/edit/"))
        {
            label1.Text = "all correct";
        }
        else
        {
            label1.Text = "not work";
        }
   
        }


    }
}
