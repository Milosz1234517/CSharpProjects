using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Windows.Forms;
using System.Threading;
namespace Bluetooth
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        Thread send;
        OpenFileDialog FileRead;
        BluetoothDeviceInfo device;
        int counter = 0;
        int filenumber = 0;

        public void Connect()
        {
            try
            {
                BluetoothAddress address = ((BluetoothDevice)listBox1.SelectedItem).address;
                BluetoothSecurity.PairRequest(address, null);
                device = new BluetoothDeviceInfo(address);

                if (device.Connected == true)
                {
                    MessageBox.Show("Połączono z urządzeniem: " + device.DeviceName + "(" + device.DeviceAddress + ")", "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie wybrano urządzenia", "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }



        public void Send()
        {
            if (device != null && FileRead != null)
            {
                lock (this)
                {
                    try
                    {
                        Uri uri = new Uri("obex://" + device.DeviceAddress + "/" + FileRead.FileName);
                        ObexWebRequest request = new ObexWebRequest(uri);
                        request.ReadFile(FileRead.FileName);
                        ObexWebResponse response = (ObexWebResponse)request.GetResponse();
                        response.Close();
                        MessageBox.Show("Status wysyłania pliku " + FileRead.FileName + response.StatusCode, "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.BeginInvoke(
                                        new Action(() =>
                                        {
                                            progressBar1.Value = counter;
                                        }));
                        counter++;

                        if (counter == filenumber)
                        {
                            Thread.Sleep(1000);
                            counter = 0;
                            filenumber = 0;
                            progressBar1.BeginInvoke(
                new Action(() =>
                {
                    progressBar1.Value = counter;
                    progressBar1.Maximum = filenumber;
                }));
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Wystąpił problem z polaczeniem", "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        progressBar1.BeginInvoke(
                new Action(() =>
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = 0;
                }));

                    }
                }
            }
        }

        public void AddToList()
        {
            listBox1.Items.Clear();
            BluetoothClient client = new BluetoothClient();
            BluetoothDeviceInfo[] devices = client.DiscoverDevices();

            foreach (BluetoothDeviceInfo devi in devices)
            {
                BluetoothDevice dev = new BluetoothDevice();
                dev.name = devi.DeviceName;
                dev.address = devi.DeviceAddress;
                listBox1.Items.Add(dev);
            }
        }

        private void connec_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void search_Click(object sender, EventArgs e)
        {
            AddToList();
        }

        private void chose_Click(object sender, EventArgs e)
        {
            FileRead = new OpenFileDialog();
            FileRead.InitialDirectory = @"c:\";
            FileRead.RestoreDirectory = true;
            FileRead.ShowDialog();
        }

        private void forget_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                BluetoothSecurity.RemoveDevice(device.DeviceAddress);
            }
        }

        private void sending_Click(object sender, EventArgs e)
        {
            filenumber = filenumber + 1;
            progressBar1.Maximum = filenumber;
            send = new Thread(Send);
            send.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = counter;
        }
    }
}
