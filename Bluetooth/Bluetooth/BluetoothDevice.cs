using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Windows.Forms;
using System.Threading;

namespace Bluetooth
{
    class BluetoothDevice
    {
        public string name;
        public BluetoothAddress address;

        public override string ToString()
        {
            return name;
        }
    }
}
