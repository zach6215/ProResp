namespace LI7000Connection
{
    using System;
    using LibUsbDotNet;
    using LibUsbDotNet.Main;
    using System.Text;

    public class LI7000Connection
    {
        private UsbEndpointReader messageReader;
        private UsbEndpointReader dataReader;
        private UsbEndpointWriter writer;
        private UsbDevice LI7000;
        private UsbDeviceFinder LI7000Finder;
        private int readTimeLimit = 1000;
        private int writeTimeLimit = 1000;
        private string? dataHeader;

        public LI7000Connection()
        {
            this.LI7000Finder = new UsbDeviceFinder(0x1509);
            this.LI7000 = UsbDevice.OpenUsbDevice(LI7000Finder);

            IUsbDevice wholeLI7000 = LI7000 as IUsbDevice;

            //Setup interface if necessary
            if (!ReferenceEquals(wholeLI7000, null))
            {
                wholeLI7000.SetConfiguration(1);
                wholeLI7000.ClaimInterface(0);
            }

            if (LI7000 == null)
            {
                throw new Exception("LI7000 not found!");
            }

            //Open readers and writer
            this.messageReader = LI7000.OpenEndpointReader(ReadEndpointID.Ep01);
            this.dataReader = LI7000.OpenEndpointReader(ReadEndpointID.Ep06);
            this.writer = LI7000.OpenEndpointWriter(WriteEndpointID.Ep02);

            this.SetupLI7000();
        }

        private void SetupLI7000()
        {
            ErrorCode errorCode = ErrorCode.None;
            string configMessage = "(USB(Rate Polled)(Timestamp None)(Sources(\"CO2B um/m\" \"H2OB mm/m\" \"T C\")))"; // 
            int bytesWritten;
            string? response;

            errorCode = this.writer.Write(Encoding.Default.GetBytes(configMessage), writeTimeLimit, out bytesWritten);

            response = this.GetResponse(this.messageReader);

            if (response != "\nOK\n")
            {
                //response += this.BufferClear();
                throw new Exception("Invalid LI7000 configuration! LI7000 responded with: " + response);
            }

            this.dataHeader = this.Poll();
        }

        private string? GetResponse(UsbEndpointReader argReader)
        {
            string? response = null;
            ErrorCode errorCode = ErrorCode.None;
            int bytesRead;

            do
            {
                byte[] readBuffer = new byte[1024];
                errorCode = argReader.Read(readBuffer, readTimeLimit, out bytesRead);

                if(bytesRead > 0)
                {
                    response += Encoding.UTF8.GetString(readBuffer, 0, bytesRead);
                }

            } while (bytesRead > 0);

            return response;
        }

        //Sometimes the message endpoint won't clear if theres an error until you write again. This funciton is to fix this.
        private string? MessageBufferClear()
        {
            int bytesWritten;
            int bytesRead;
            ErrorCode errorCode = ErrorCode.None;
            string response = null;

            this.writer.Write(Encoding.Default.GetBytes(")"), 1000, out bytesWritten);

            do
            {
                byte[] readBuffer = new byte[1024];
                errorCode = this.messageReader.Read(readBuffer, 1000, out bytesRead);

                if (bytesRead > 0)
                {
                    response += Encoding.UTF8.GetString(readBuffer, 0, bytesRead);
                }

            } while (bytesRead > 0);

            return response;
        }

        public string? Poll()
        {
            string? responseMessage = null;
            string? responseData = null;
            ErrorCode errorCode = ErrorCode.None;
            int bytesWritten;


            errorCode = this.writer.Write(Encoding.Default.GetBytes("(USB(Poll Now))"), this.writeTimeLimit,out bytesWritten);

            responseMessage = this.GetResponse(this.messageReader);

            if(responseMessage == "\nOK\n")
            {
                responseData = this.GetResponse(this.dataReader);
            }


            return responseData;
        }
    }
}