namespace LICommunicationConsoleApp
{
    using System;
    using System.IO.Ports;
    using System.Management;
    using System.Text;
    using LibUsbDotNet;
    using LibUsbDotNet.Main;
    using LibUsbDotNet.Descriptors;
    using LibUsbDotNet.Info;
    using LibUsbDotNet.Internal;

    public class Program
    {
        public static void Main()
        {
            string result;

            while (true)
            {
                Console.WriteLine("Menu");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1: Serial Port");
                Console.WriteLine("2: USB Port");
                Console.WriteLine("3: Quit");
                Console.WriteLine("What connection do you want to test?");
                result = Console.ReadLine();

                switch (result)
                {
                    case "1":
                        SerialPortTest();
                        break;

                    case "2":
                        USBPortTest();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        private static void USBPortTest()
        {
            //USB is identified by it's serial number
            UsbDevice LI7000;
            string userInput = string.Empty;
            UsbEndpointReader readerLI7000;
            UsbEndpointWriter writerLI7000;
            UsbEndpointReader dataReaderLI7000;
            int bytesWritten;
            

            try
            {
                UsbDeviceFinder usbFinder = new UsbDeviceFinder(0x1509);

                LI7000 = UsbDevice.OpenUsbDevice(usbFinder);

                IUsbDevice wholeLI7000 = LI7000 as IUsbDevice;

                if (!ReferenceEquals(wholeLI7000, null))
                {
                    wholeLI7000.SetConfiguration(1);
                    wholeLI7000.ClaimInterface(0);
                }

                readerLI7000 = LI7000.OpenEndpointReader(ReadEndpointID.Ep01);
                writerLI7000 = LI7000.OpenEndpointWriter(WriteEndpointID.Ep02);
                dataReaderLI7000 = LI7000.OpenEndpointReader(ReadEndpointID.Ep06); //6
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (LI7000 != null)
            {
                Console.WriteLine("Device Found!");
            }
            else
            {
                Console.WriteLine("Device NOT Found!");
                return;
            }



            while(true)
            {
                Console.WriteLine("Menu");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1: Input command");
                Console.WriteLine("2: Quit");

                userInput = Console.ReadLine();
                switch(userInput)
                {
                    case "1":
                        ErrorCode ec = ErrorCode.None;
                        Console.WriteLine("Input command:");
                        userInput = Console.ReadLine();
                        ec = writerLI7000.Write(Encoding.Default.GetBytes(userInput), 1000, out bytesWritten);

                        if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

                        
                        int bytesRead;
                        int dataBytesRead;
                        string response = string.Empty;

                        do
                        {
                            byte[] readBuffer = new byte[1024];
                            ec = readerLI7000.Read(readBuffer, 1000, out bytesRead);

                            response += Encoding.UTF8.GetString(readBuffer, 0, bytesRead);

                            ec = dataReaderLI7000.Read(readBuffer, 1000, out dataBytesRead);

                            response += Encoding.UTF8.GetString(readBuffer, 0, dataBytesRead);

                            //if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);
                        } while (bytesRead > 0 || dataBytesRead > 0);
                        Console.WriteLine("Response: " + response);

                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
                
            }
        }

        /// <summary>
        /// Finds USB devices and outputs DeviceID (contains VID and PID)
        /// </summary>
        private void FindUSBDevice()
        {
            ManagementObjectCollection collection;
            using (var finddevice = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity where DeviceID Like ""USB%"""))
                collection = finddevice.Get();
            foreach (var device in collection)
            {
                Console.WriteLine("-----------------------------");

                Console.WriteLine(device.GetPropertyValue("Description"));
                string[] IDs = (string[])device.GetPropertyValue("CompatibleID");

                foreach (string ID in IDs)
                {
                    Console.WriteLine(ID);
                }

                Console.WriteLine(device.GetPropertyValue("DeviceID"));
            }
        }

        private static void SerialPortTest()
        {
            SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One); // Name, Baudrate, Parity, Databits, Stopbits

            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string command;
            string result;

            do
            {
                Console.WriteLine("Enter 'quit' to quit.");
                Console.WriteLine("Enter command for LI-7000 (must be in format (Command(sub-command element))<lf> ): ");

                command = Console.ReadLine();

                if (command != "" && command != "quit" && serialPort.IsOpen)
                {
                    serialPort.WriteLine(command);
                    result = serialPort.ReadExisting();

                    Console.WriteLine("LI-7000 responded: ");
                    Console.WriteLine(result);
                }


            } while (command != "quit" && command != "");

            serialPort.Close();
        }
    }
}