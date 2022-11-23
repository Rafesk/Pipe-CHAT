using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.NetworkInformation;

namespace Con_Pipe_chat
{
    internal class Program
    {
        public static List<string> msgs = new List<string>();
        public static void PipeWork(object counts)
        {
            NamedPipeServerStream pipe = new NamedPipeServerStream("myPipe", PipeDirection.InOut, 5,PipeTransmissionMode.Message,PipeOptions.Asynchronous);
            pipe.WaitForConnection();
            Console.WriteLine("Пользователь подключился!");
            Thread thread1 = new Thread(new ParameterizedThreadStart(PipeWork));
            thread1.Start((int)counts+1);
            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            int msgCount = 0;
            Task<string> t = rd.ReadLineAsync();
            while (true)
            {
                if (t.IsCompleted)
                {
                    msgs.Add("Пользователь "+counts+" : "+t.Result);
                    t = rd.ReadLineAsync();
                }
                if (msgCount < msgs.Count)
                {
                    int k = msgCount;
                    for (int i = k; i < msgs.Count ; i++)
                    {
                        wr.WriteLine(msgs[i]);
                        wr.Flush();
                        msgCount++;
                    }

                }
            }
            
            
               // await Task.Delay(2000);
            //}
        }
        static void Main(string[] args)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(PipeWork));
            thread.Start(1);
        }
    }
}
