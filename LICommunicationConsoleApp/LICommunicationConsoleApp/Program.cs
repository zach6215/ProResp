

namespace LICommunicationConsoleApp
{
    using System;
    using System.IO.Ports;
    public class Program
    {
        public static void Main()
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
                Console.WriteLine("Enter command for LI-7000: ");

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