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

        List<int> horas = new List<int>() {
        0,
1 ,
2 ,
3 ,
4 ,
5 ,
6 ,
7 ,
8 ,
9 ,
10,
11,
12,
13,
14,
15,
16,
17,
18,
19,
20,
21,
22,
23};

        List<Usuario> Usuarios = new List<Usuario>();
        public Form1()
        {
            InitializeComponent();
            Usuario u1 = new Usuario();
            u1.Nome = "Usuario 1";
            u1.SetHorariosRange(horarios1);
            u1.SetHorariosLDAP(horarios1);
            Usuarios.Add(u1);

            Usuario u2 = new Usuario();
            u2.Nome = "Usuario 2";
            u2.SetHorariosRange(horarios2);
            u2.SetHorariosLDAP(horarios2);
            Usuarios.Add(u2);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
            dataGridViewHorarios.AutoGenerateColumns = false;

            ColumnInicio.DataSource = horas;
            ColumnHoraFinal.DataSource = horas;
            dataGridViewUsuarios.DataSource = Usuarios;
        }

        private void dataGridViewUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                Usuario u = Usuarios[dataGridViewUsuarios.SelectedRows[0].Index];
                dataGridViewHorarios.DataSource = u.Horarios;
            }
        }

        private void dataGridViewHorarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            label1.Text = "Horarios Carregados";
        }
    }
}