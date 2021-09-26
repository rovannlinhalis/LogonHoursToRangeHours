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
        Usuario usuarioSelecionado;
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

            montarTabela();

        }
        private void montarTabela()
        {
            tableLayoutPanel1.Controls.Clear();
            for (int h = 0; h <24; h++)
            {
                Label ld = new Label();
                ld.AutoSize = true;
                ld.Text = h.ToString("00");
                ld.Font = new Font("Consolas", 6.5f, FontStyle.Regular);
                tableLayoutPanel1.Controls.Add(ld, h+1, 0);
            }    
            for (int d = 0; d < 7; d++)
            {
                Label ld = new Label();
                ld.AutoSize = true;
                ld.Width = 90;
                ld.Height = 30;
                ld.Text = ((DayOfWeek)d).ToString();
                tableLayoutPanel1.Controls.Add(ld,0, d+1);
                for (int h = 0; h < 24; h++)
                {
                    CheckBox cb = new CheckBox();
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.FlatAppearance.CheckedBackColor = Color.Blue;
                    cb.BackColor = Color.White;
                    cb.Appearance = Appearance.Button;
                    cb.Text = "";
                    cb.Width = 15;
                    cb.Height = 30;
                    cb.Tag = d + ";" + h;
                    cb.AutoSize = false;
                    cb.Name = "cb"+ d + ";" + h;
                    tableLayoutPanel1.Controls.Add(cb, h+1, d+1);
                }
            }
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
                usuarioSelecionado = Usuarios[dataGridViewUsuarios.SelectedRows[0].Index];
                dataGridViewHorarios.DataSource = usuarioSelecionado.Horarios;

                foreach (HorarioLDAP d in usuarioSelecionado.HorariosLDAP)
                {
                    for (int h = 0; h < d.Horas.Length; h++)
                    {
                        CheckBox cb = tableLayoutPanel1.GetControlFromPosition(h + 1, (int)d.Dia + 1) as CheckBox;
                        if (cb!= null)
                        {
                            cb.Checked = d.Horas[h];
                        }
                    }
                }

                //Control
                logonHoursControl1.Value = usuarioSelecionado.GetHorariosLDAP();

            }
        }

        private void dataGridViewHorarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            label1.Text = "Horarios Carregados";
        }

        private void buttonSalvarRange_Click(object sender, EventArgs e)
        {
            if (usuarioSelecionado != null)
            {
                byte[] dadosNovosHorarios = usuarioSelecionado.GetHorariosRange();
                label1.Text ="Dados Salvos\n" + Funcoes.BinaryStringFromByteArray(dadosNovosHorarios);
            }
        }

        private void buttonSalvarLDAP_Click(object sender, EventArgs e)
        {
            foreach (HorarioLDAP d in usuarioSelecionado.HorariosLDAP)
            {
                for (int h = 0; h < d.Horas.Length; h++)
                {
                    CheckBox cb = tableLayoutPanel1.GetControlFromPosition(h + 1, (int)d.Dia + 1) as CheckBox;
                    if (cb != null)
                    {
                        d.Horas[h] = cb.Checked;
                    }
                }
            }
            label9.Text = "Dados Salvos\n" + Funcoes.BinaryStringFromByteArray(usuarioSelecionado.GetHorariosLDAP());

        }

        private void buttonSalvarLogonHoursControl_Click(object sender, EventArgs e)
        {
            byte[] dados = logonHoursControl1.Value;
            label10.Text = "Dados Salvos\n" + Funcoes.BinaryStringFromByteArray(dados);
        }
    }
}