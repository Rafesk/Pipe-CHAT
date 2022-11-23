using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Con_Pipe_chat
{
    internal class Program
    {
        public static List<string> msgs = new List<string>();
        public static void PipeWork()
        {
            NamedPipeServerStream pipe = new NamedPipeServerStream("myPipe", PipeDirection.InOut, 5,PipeTransmissionMode.Message,PipeOptions.Asynchronous);
            pipe.WaitForConnection();
            Console.WriteLine("Пользователь подключился!");
            Thread thread1 = new Thread(PipeWork);
            thread1.Start();
            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            int msgCount = 0;
            Task<string> t = rd.ReadLineAsync();
            while (true)
            {
                if (t.IsCompleted)

                {
                    msgs.Add(t.Result);
                    t = rd.ReadLineAsync();
                }
                if (msgCount < msgs.Count)
                {
                    ////Console.WriteLine("сообщения:");
                    foreach (string str2 in msgs)
                    {
                        wr.WriteLine("Ответ: " + str2);
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
            Thread thread = new Thread(PipeWork);
            thread.Start();
        }
    }
}
