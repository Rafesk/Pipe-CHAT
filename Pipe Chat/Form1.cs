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
            pipe = new NamedPipeClientStream("myPipe");
            pipe.Connect();
            SMSREAD();
        }
        public async void SMSREAD()
        {
            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            while (true)
            {
                wr.WriteLine(textBox1.Text);
                wr.Flush();
                textBox2.Text = rd.ReadLine();
                await Task.Delay(2000);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            wr.WriteLine(textBox1.Text);
            wr.Flush();
            textBox2.Text += "Вы: " + textBox1.Text + "/r/n";
            textBox1.Text = "";

        }
    }
}