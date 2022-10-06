using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace Console_Pipe_Chat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NamedPipeServerStream pipe = new NamedPipeServerStream("myPipe", PipeDirection.InOut,2);
            pipe.WaitForConnection();
            Console.WriteLine("Пользователь подключился!");
            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            wr.AutoFlush = true;
            string str = rd.ReadLine();
            Console.WriteLine("Read: " + str);
            rd.Close();
            wr.Close();
        }
    }
}
