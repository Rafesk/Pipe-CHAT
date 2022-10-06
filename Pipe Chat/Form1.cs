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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            wr.WriteLine(textBox1.Text);
            textBox1.Text = "";
            rd.Close();
            wr.Close();
        }
    }
}