using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Gaming.Input;

namespace GamePadUP
{
    public partial class Form1 : Form
    {
        Gamepad controler;
        Timer t = new Timer();
        Graphics g;
        int x = 0;
        int y = 0;
        bool moving = false;
        Pen pen;
        int penSize = 3;
        Color c = Color.Black;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public void DoMouseClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        public Form1()
        {
            InitializeComponent();
            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;
            t.Tick += T_Tick;
            t.Interval = 1;
            t.Start();
            g = panel1.CreateGraphics();
            pen = new Pen(c, penSize);
        }

        private void MoveCursor()
        {
            Cursor = new Cursor(Cursor.Current.Handle);
            var reading = controler.GetCurrentReading();
            int x = (int)reading.LeftThumbstickX * 5;
            int y = (int)reading.LeftThumbstickY * 5;
            Cursor.Position = new Point(Cursor.Position.X + x, Cursor.Position.Y - y);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (Gamepad.Gamepads.Count > 0)
            {
                controler = Gamepad.Gamepads.First();
                var reading = controler.GetCurrentReading();
                Cursor = new Cursor(Cursor.Current.Handle);

                int x1 = (int)reading.LeftThumbstickX * 5;
                int y1 = (int)reading.LeftThumbstickY * 5;

                int y2 = (int)reading.RightThumbstickY;

                Cursor.Position = new Point(Cursor.Position.X + x1, Cursor.Position.Y - y1);
                Cursor.Clip = new Rectangle(Location, Size);
                pen = new Pen(c, penSize);
                if (reading.Buttons == GamepadButtons.A)
                {
                    label1.Text = "A";
                    DoMouseClick();
                }
                if (reading.Buttons == GamepadButtons.B)
                {
                    label1.Text = "B";
                    moving = false;
                }
                if(reading.Buttons == GamepadButtons.X)
                {
                    label1.Text = "X";
                    c = Color.Red;
                }
                if (reading.Buttons == GamepadButtons.Y)
                {
                    label1.Text = "Y";
                    c = Color.Black;
                }
                if(y2 == 1)
                {
                    penSize++;
                }
                if (y2 == -1)
                {
                    penSize--;
                }

            }
        }

        private void Gamepad_GamepadRemoved(object sender, Gamepad e)
        {

        }

        private void Gamepad_GamepadAdded(object sender, Gamepad e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(moving && x!=-1 && y != -1)
            {
                g.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
