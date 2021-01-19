using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace check_site
{
    public partial class Form1 : Form
    {
        private String url;
        private FormWindowState _OldFormState;
        private Icon icon;
        private Timer timer = new Timer();
        private SiteCheck siteCheck;
        public Form1()
        {
 //---------------------------------------------------------------------------------//
            InitializeComponent();
            //загрузка иконки и ссылки на сайт
            try
            {
                icon = new Icon("icon/connect.ico");
                url = System.IO.File.ReadAllText(@"link.txt").Replace("\n", " ");
                if (url=="") throw new Exception("url empty");

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);     
                System.Environment.Exit(-1);
            }
            siteCheck = new SiteCheck(url);

//--------------------------------------------------------------------------------------//
            //настройка главной формы
            this.Text = url;
            //this.WindowState = FormWindowState.Minimized;
            
            label1.Text = "Strart";
            //цикл работы программы
            timer.Tick += new EventHandler(Refresh);
            timer.Interval = 3000; // Здесь измени интервал на 5000 (5 сек)
            timer.Start();
            //программа в правом нижнем углу
            this.StartPosition = FormStartPosition.Manual;
            var wArea = Screen.PrimaryScreen.WorkingArea;
            this.Left = wArea.Width + wArea.Left - this.Width;
            this.Top = wArea.Height + wArea.Top - this.Height;

//-----------------------------------------------------------------------------------------------//
            //настройка всплывающих сообщений
            /*
          //задаем всплывающий текст-подсказку (появляется при наведении указателя на иконку в трее)
          notifyIcon1.Text = "контроль работоспособности сайта";
          //устанавливаем значок, отображаемый в трее:
          //либо один из стандартных:
          //notifyIcon1.Icon = SystemIcons.Error;
          //либо свой из файла:
          notifyIcon1.Icon = checkMailIcon;
          */
            //задаем всплывающий текст-подсказку (появляется при наведении указателя на иконку в трее)
            notifyIcon1.Text = "контроль работоспособности сайта";
            notifyIcon1.Icon = SystemIcons.Exclamation;
            notifyIcon1.BalloonTipTitle = url;
            notifyIcon1.BalloonTipText = "start";
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = icon;
            //подписываемся на событие клика мышкой по значку в трее
            notifyIcon1.MouseClick += new MouseEventHandler(_notifyIcon_MouseClick);
            //подписываемся на событие изменения размера формы
            this.Resize += new EventHandler(FormForTray_Resize);
           // notifyIcon1.ShowBalloonTip(1);
        }
//---------------------------------------------------------------------------------------------------------------------//
//                                          функции
//---------------------------------------------------------------------------------------------------------------------//
        //реакция на нажатие по всплывающим сообщениям
        /// <summary>
        /// обрабатываем событие клика мышкой по значку в трее
        /// </summary>
        void _notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            ///проверяем, какой кнопкой было произведено нажатие
            if (e.Button == MouseButtons.Left)//если левой кнопкой мыши
            {
                //проверяем текущее состояние окна
                if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)//если оно развернуто
                {
                    //сохраняем текущее состояние
                    _OldFormState = WindowState;
                    //сворачиваем окно
                    WindowState = FormWindowState.Minimized;
                    //скрываться в трей оно будет по событию Resize (изменение размера), которое сгенерировалось после 
                    //минимизации строчкой выше
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

        /// <summary>
        /// обрабатываем событие изменения размера
        /// </summary>
        void FormForTray_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)//если окно "свернуто"
            {
                //то скрываем его
                Hide();
            }
        }
 //-------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// обновление статуса сайта
        /// </summary>
        public void Refresh(object sender, EventArgs e)
        {
            label1.Text = siteCheck.Refresh().message;
            if (!siteCheck.Refresh().flag) {
                ShowBalloon(siteCheck.Refresh().message);
            }
        }

        /// <summary>
        /// отображение сообщения
        /// </summary>
        void ShowBalloon (String balloon)
        {
          
            notifyIcon1.BalloonTipText = balloon;
            notifyIcon1.ShowBalloonTip(1);
        }
        //----------------------------------------------------------------------------------------------------------------------------//
        //действия contextMenuStrip1
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //выход из программы
            //Application.Exit();
            notifyIcon1.Visible = false;
            System.Environment.Exit(0);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //отоброжение информации обо мне))
            About about = new About();
            about.Show();
           
        }
    }
}