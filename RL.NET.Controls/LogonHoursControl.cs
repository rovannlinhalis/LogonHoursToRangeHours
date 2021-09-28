using System;
using System.Collections;
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
        private BitArray _BitContainer;
        int fusoHorario =-3;
        Color checkedColor, backgroundColor, mouseOverColor;
        Dictionary<string, CheckBox> checkBoxes = new Dictionary<string, CheckBox>();

        public delegate void ValueChangedHandler(object sender, EventArgs e);
        public event ValueChangedHandler OnValueChanged;


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
                    cb.CheckedChanged += (ss, ee) => {
                        OnValueChanged?.Invoke(ss, ee);
                    };
                    checkBoxes.Add(cb.Name, cb);
                    tableLayoutPanel1.Controls.Add(cb, h + 1, d + 1);
                }
            }
        }

        public byte[] Value
        {
            get
            {
                bool[] auxbool = new bool[7 * 24];

                for (int d = 0; d < 7; d++)
                    for (int h = 0; h < 24; h++)
                        auxbool[d * 24 + h] = checkBoxes["cb" + d + ";" + h].Checked;

                _BitContainer = new BitArray(auxbool);
                BitArray currentValue = BitArrayOffset(_BitContainer, -1 * this.FusoHorario);
                return ToByteArray(currentValue);
            }
            set
            {
                if (value != null)
                {
                    _BitContainer = BitArrayOffset(new BitArray(value), this.FusoHorario);
                    bool[] auxbool = ToBooleanArray(_BitContainer);
                    for (int d = 0; d < 7; d++)
                        for (int h = 0; h < 24; h++)
                            checkBoxes["cb" + d + ";" + h].Checked = auxbool[d * 24 + h];
                }
                else
                {
                    Limpar();
                }
            }
        }
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
        [Browsable(true)]
        public int FusoHorario
        {
            get => fusoHorario; 
            set
            {
                fusoHorario = value;
                if (_BitContainer != null)
                    _BitContainer = BitArrayOffset(_BitContainer, fusoHorario);
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
        int clickCount = 0;
        private void labelHelp_Click(object sender, EventArgs e)
        {
            clickCount++;
            if (clickCount >= 10)
            {
                clickCount = 0;
                MessageBox.Show("Criado por Rovann Linhalis - rovann@gmail.com", "Logon Hours Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        internal static byte[] ToByteArray(BitArray _btarray)
        {
            int toplevel = _btarray.Count / 8;
            byte[] rtnbytearr = new byte[toplevel];
            bool[] conbin;
            int mult = 0;
            for (int i = 0; i < toplevel; i++)
            {
                conbin = new bool[8];
                for (int b = 0; b < 8; b++)
                    conbin[b] = _btarray.Get(b + mult);
                rtnbytearr[i] = BinToByte(conbin);
                mult += 8;
            }
            return rtnbytearr;
        }
        internal static byte BinToByte(bool[] boolvals)
        {
            int multiplier = 1;
            int rtnval = 0;

            for (int i = 0; i < boolvals.Length; i++)
            {
                rtnval = rtnval + (Convert.ToInt16(boolvals[i]) * multiplier);
                multiplier = multiplier * 2;
            }
            return (byte)rtnval;
        }
        internal static BitArray BitArrayOffset(BitArray ArrayToOffsetFrom, int OffsetValue)
        {
            if (OffsetValue == 0)
                return ArrayToOffsetFrom;
            BitArray rtnbitArr = new BitArray(ArrayToOffsetFrom.Count);
            int Offset = 0;
            for (int i = 0; i < ArrayToOffsetFrom.Count; i++)
            {
                Offset = i + OffsetValue;
                if (Offset > ArrayToOffsetFrom.Count - 1)
                    Offset = Offset - ArrayToOffsetFrom.Count;
                if (Offset < 0)
                    Offset = ArrayToOffsetFrom.Count + Offset;
                rtnbitArr.Set(Offset, ArrayToOffsetFrom[i]);
            }
            return rtnbitArr;
        }
        internal static bool[] ToBooleanArray(BitArray _btarray)
        {
            bool[] rtnboolarr = new bool[_btarray.Count];
            for (int b = 0; b < _btarray.Count; b++)
                rtnboolarr[b] = _btarray[b];
            return rtnboolarr;
        }
    }
}
