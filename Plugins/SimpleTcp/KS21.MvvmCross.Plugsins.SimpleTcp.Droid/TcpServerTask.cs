using System;
using System.IO;
using System.Threading.Tasks;
using Java.IO;
using Java.Net;
using IOException = Java.IO.IOException;

namespace KS21.MvvmCross.Plugins.SimpleTcp.Droid
{
    public class TcpServerTask : ITcpServerTask
    {
        public async void StartServerAndWaitingForClient(int port)
        {
            try
            {
                Boolean end = false;
                var ss = new ServerSocket(port);
                while (!end)
                {
                    //Server is waiting for client here, if needed
                    //Socket s = ss.Accept();

                    Task<Socket> task = ss.AcceptAsync();

                    Socket s = await task;

                    Stream outs = s.OutputStream;
                    var output = new PrintWriter(outs);
                    output.Println("Hello Android-Client!");

                    var input = new BufferedReader(new InputStreamReader(s.InputStream));
                    String st = input.ReadLine();

                    s.Close();

                    if (!string.IsNullOrEmpty(st))
                    {
                        end = true;
                    }
                }
                ss.Close();
            }
            catch (UnknownHostException e)
            {
                throw new UnknownHostException();
            }
            catch (IOException e)
            {
                throw new IOException();
            }
        }
    }
}