using System.IO;
using System.IO.Pipes;
namespace Pipe_Chat
{
    public partial class Form1 : Form
    {
        NamedPipeClientStream pipe;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pipe = new NamedPipeClientStream(".", "myPipe", PipeDirection.InOut,PipeOptions.Asynchronous);
            pipe.Connect();
            rd = new StreamReader(pipe);
            wr = new StreamWriter(pipe);
            t = rd.ReadLineAsync();
            timer1.Enabled = true;
        }
        
        Task<string> t;
        StreamReader rd;
        StreamWriter wr;
        private void button2_Click(object sender, EventArgs e)
        {
            wr.WriteLine(textBox1.Text);
            wr.Flush();
            textBox2.Text += "Вы: " + textBox1.Text + "\r\n";
            textBox1.Text = "";
            
            //String text = rd.ReadLine();
            //textBox2.Text += "Ответ: "+ text + "\r\n";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t.IsCompleted)
            {
                textBox2.Text +=  t.Result + "\r\n";
                t = rd.ReadLineAsync();
            }
        }
    }
}