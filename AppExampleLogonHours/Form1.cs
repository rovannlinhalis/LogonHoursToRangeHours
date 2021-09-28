using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppExampleLogonHours
{
    public partial class Form1 : Form
    {
        string horarios1 =
@"
11111111 11111111 11111111
11100000 00000000 00001111
11111111 11111100 00000000
00000000 00000000 11111111
11100000 00000000 00000111
11111111 11111000 00000000
00100000 00000000 11111111
";

        string horarios2 =
@"
00001111 11111111 11110000
00001111 11111110 00001111
00001111 11111100 00000000
00000000 00000000 11111111
00000000 00111111 00011100
11111111 11111000 00000000
11111111 11111111 11111111
";

        byte[] testeDados = { 0xff, 0xff, 0xff, 0x07, 0x00, 0xf0, 0xff, 0x3f, 0x00, 0x00, 0x00, 0xff, 0x07, 0x00, 0xe0, 0xff, 0x1f, 0x00, 0x00, 0x00, 0xff };

        List<Usuario> Usuarios = new List<Usuario>();
        Usuario usuarioSelecionado;
        public Form1()
        {
            InitializeComponent();
            Usuario u1 = new Usuario();
            u1.Nome = "Usuario 1";
            u1.LogonHours = Funcoes.FromBinaryString(horarios1);
            Usuarios.Add(u1);

            Usuario u2 = new Usuario();
            u2.Nome = "Usuario 2";
            u2.LogonHours = Funcoes.FromBinaryString(horarios2);
            Usuarios.Add(u2);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridViewUsuarios.DataSource = Usuarios;
        }

        private void dataGridViewUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                usuarioSelecionado = Usuarios[dataGridViewUsuarios.SelectedRows[0].Index];
                //Control
                byte[] dados = usuarioSelecionado.LogonHours;
                //byte[] dados = { 0xff, 0xff, 0xff,0x07, 0x00, 0xf0, 0xff,0x3f, 0x00,0x00,0x00,0xff,0x07,0x00, 0xe0, 0xff,0x1f, 0x00,0x00,0x00,0xff };
                logonHoursControl1.Value = dados;
            }
        }
       

        private void logonHoursControl1_OnValueChanged(object sender, EventArgs e)
        {
            byte[] dados = logonHoursControl1.Value;
            label10.Text = "Dados Salvos\n" + Funcoes.BinaryStringFromByteArray(dados);
            if (usuarioSelecionado != null)
                usuarioSelecionado.LogonHours = dados;
        }

        
     
    }
}