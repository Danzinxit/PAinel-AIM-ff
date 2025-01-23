using System.Diagnostics;
using Memory;


namespace painelff
{
    public partial class Form1 : Form
    {
        Mem memory = new Mem();
        public Form1()
        {
            InitializeComponent();
        }
        private async void btnActive_Click(object sender, EventArgs e)
        {
            Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
            memory.OpenProcess(proc);

            var result = await memory.AoBScan("FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00",true, true );

             if (result.Any())
            {

                foreach (var CurrentAddress in result)
                {
                    Int64 EndereçoLeitura = CurrentAddress + 0x60;
                    Int64 EndereçoEscrita = CurrentAddress + 0x5C;

                    var Read = memory.ReadMemory<int>(EndereçoLeitura.ToString("X"));
                    memory.WriteMemory(EndereçoEscrita.ToString("X"), "int", Read.ToString());
                    MessageBox.Show("Finalizado!");

                }
            }
            else
            {

                MessageBox.Show("Nenhum Valor Encontrado!");
            }
             //

        }
    }

 }


