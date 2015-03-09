using System;
using System.IO;
using Java.IO;
using Java.Net;
using IOException = Java.IO.IOException;

namespace KS21.MvvmCross.Plugins.SimpleTcp.Droid
{
    public class TcpClientTask : ITcpClientTask
    {
        public string OpenClientAndConnectToServer(string ipAddress, int port)
        {
            String st = "no no no";

            try
            {
                var s = new Socket(ipAddress, port);
                var input = new BufferedReader(new InputStreamReader(s.InputStream));

                //read line(s)
                st = input.ReadLine();

                if (!string.IsNullOrEmpty(st))
                {
                    //outgoing stream redirect to socket
                    Stream outs = s.OutputStream;

                    var output = new PrintWriter(outs);
                    output.Println("Hello Android-Ser!");
                }

                //Close connection
                s.Close();
            }
            catch (UnknownHostException e)
            {
                throw new UnknownHostException();
            }
            catch (IOException e)
            {
                throw new IOException();
            }

            return st;
        }
    }
}