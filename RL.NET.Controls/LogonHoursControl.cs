using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLControls
{
    /// <summary>
    /// LogonHours Winforms Control
    /// Represents a LDAP user logon hours data (byte[])
    /// V1 27/09/2021
    /// Rovann Linhalis
    /// rovann@gmail.com
    /// </summary>
    public partial class LogonHoursControl : UserControl
    {
        Dictionary<string, CheckBox> checkBoxes = new Dictionary<string, CheckBox>();
        public LogonHoursControl()
        {
            InitializeComponent();
            MontarCheckBoxes();
        }
        
        private void MontarCheckBoxes()
        {
            for (int d = 0; d < 7; d++)
            {
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
                    cb.Padding = new Padding(0);
                    cb.Margin = new Padding(0);
                    cb.FlatAppearance.BorderSize = 0;
                    cb.AutoSize = false;
                    cb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    cb.Name = "cb" + d + ";" + h;
                    checkBoxes.Add(cb.Name, cb);
                    tableLayoutPanel1.Controls.Add(cb, h + 1, d + 1);
                }
            }
        }

        byte[] _value;

        public byte[] Value
        {
            get
            {
                string bs = "";
                for (int d = 0; d < 7; d++)
                {
                    for (int h = 0; h < 24; h++)
                        bs += checkBoxes["cb" + d + ";" + h].Checked ? '1' : '0';
                }
                _value = FromBinaryString(bs);
                return _value;
            }
            set
            {
                _value = value;

                string bs = BinaryStringFromByteArray(_value);
                if (!String.IsNullOrWhiteSpace(bs))
                {
                    int l = 3 * 8;
                    for (int d = 0; d < 7; d++)
                    {
                        string dia = bs.Substring(l * d, l);
                        for (int h = 0; h < 24; h++)
                            checkBoxes["cb" + d + ";" + h].Checked = dia[h] == '1';
                    }
                }
                else
                {
                    Limpar();
                }
            }
        }
        Color checkedColor, backgroundColor, mouseOverColor;

        public Color CheckedColor
        {
            get => checkedColor;
            set
            {
                checkedColor = value;
                foreach (var kp in checkBoxes)
                    kp.Value.FlatAppearance.CheckedBackColor = checkedColor;

            }
        }
        [Browsable(true)]
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                foreach (var kp in checkBoxes)
                    kp.Value.BackColor = backgroundColor;
            }
        }
        [Browsable(true)]
        public Color MouseOverColor
        {
            get => mouseOverColor;
            set
            {
                mouseOverColor = value;
                foreach (var kp in checkBoxes)
                    kp.Value.FlatAppearance.MouseOverBackColor = mouseOverColor;
            }
        }

        public void Limpar()
        {
            foreach (var kp in checkBoxes)
            {
                kp.Value.Checked = false;
            }
        }

        private void labelHora_DoubleClick(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            if (lb != null)
            {
                int h = tableLayoutPanel1.GetColumn(lb);
                h--;

                for (int d = 0; d<7;d++)
                {
                    checkBoxes["cb" + d + ";" + h].Checked = !checkBoxes["cb" + d + ";" + h].Checked;
                }
            }
        }

        private void labelDow_DoubleClick(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            if (lb != null)
            {
                int d = tableLayoutPanel1.GetRow(lb);
                d--;

                for (int h = 0; h < 24; h++)
                {
                    checkBoxes["cb" + d + ";" + h].Checked = !checkBoxes["cb" + d + ";" + h].Checked;
                }
            }
        }

        private byte[] FromBinaryString(string binaryString)
        {
            if (!String.IsNullOrWhiteSpace(binaryString))
            {
                binaryString = binaryString.Replace(" ", "").Replace("\r", "").Replace("\n", "");
                var byteArray = Enumerable.Range(0, int.MaxValue / 8)
                                          .Select(i => i * 8)    // get the starting index of which char segment
                                          .TakeWhile(i => i < binaryString.Length)
                                          .Select(i => binaryString.Substring(i, 8)) // get the binary string segments
                                          .Select(s => Convert.ToByte(s, 2)) // convert to byte
                                          .ToArray();
                return byteArray;
            }
            else return null;
        }

        private string BinaryStringFromByteArray(byte[] data)
        {
            if (data != null)
            return string.Join("", data.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

            return "";
        }
        int clickCount = 0;
        private void label31_Click(object sender, EventArgs e)
        {
            clickCount++;
            if (clickCount >= 10)
            {
                clickCount = 0;
                MessageBox.Show("Criado por Rovann Linhalis - rovann@gmail.com", "Logon Hours Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
