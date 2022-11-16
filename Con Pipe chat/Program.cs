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
        public static async void PipeWork()
        {
            NamedPipeServerStream pipe = new NamedPipeServerStream("myPipe", PipeDirection.InOut, 5);
            pipe.WaitForConnection();
            Console.WriteLine("Пользователь подключился!");
            Thread thread1 = new Thread(PipeWork);
            thread1.Start();
            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            while (true)
            {
                String str = rd.ReadLine();
                Console.WriteLine("Read: " + str);
                msgs.Add(str);
                Console.WriteLine("сообщения:");
                foreach(string str2 in msgs)
                {
                    Console.WriteLine(str2);
                }
                wr.WriteLine("Ответ: "+ str);
                wr.Flush();
                await Task.Delay(2000);
            }
        }
        static void Main(string[] args)
        {
            Thread thread = new Thread(PipeWork);
            thread.Start();
        }
    }
}
