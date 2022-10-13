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
        static void Main(string[] args)
        {
            NamedPipeServerStream pipe = new NamedPipeServerStream("myPipe", PipeDirection.InOut, 2);
            pipe.WaitForConnection();
            Console.WriteLine("Пользователь подключился!");
            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            wr.AutoFlush = true;
            string str = rd.ReadLine();
            Console.WriteLine("Read: " + str);
            
            
            wr.Close();
            rd.Close();
        }
    }
}
