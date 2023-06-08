

namespace LICommunicationConsoleApp
{
    using System;
    using System.IO.Ports;
    using System.Management;
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