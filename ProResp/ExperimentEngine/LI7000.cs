using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentEngine
{
    using LibUsbDotNet;
    using LibUsbDotNet.Main;
    internal class LI7000
    {
        private UsbDevice device;
        private UsbEndpointReader reader;
        private UsbEndpointWriter writer;

        public LI7000()
        {

        }

        private bool connect()
        {
            try
            {
                UsbDeviceFinder usbFinder = new UsbDeviceFinder(0x1509);
                this.device = UsbDevice.OpenUsbDevice(usbFinder);

                IUsbDevice? wholeDevice = this.device as IUsbDevice;

                if (!ReferenceEquals(wholeDevice, null))
                {
                    wholeDevice.SetConfiguration(1);
                    wholeDevice.ClaimInterface(0);
                }

                this.reader = device.OpenEndpointReader(ReadEndpointID.Ep01);
                this.writer = device.OpenEndpointWriter(WriteEndpointID.Ep02);

                if (this.device == null)
                {
                    throw new Exception("LI7000 not found!");
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
