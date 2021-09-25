using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExampleLogonHours
{
    public class Usuario
    {
        public string Nome { get; set; }

        public List<HorarioRange> Horarios { get; set; } = new List<HorarioRange>(7);
        public void SetHorariosRange(byte[] dados)
        {
            string binfinal = string.Join("", dados.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            Horarios.Clear();

            int l = 3 * 8;
            
            for (int d = 0; d < 7; d++)
            {
                HorarioRange range = new HorarioRange();
                range.Dia = ((DayOfWeek)d);
                string dia = binfinal.Substring(l * d, l);
                range.Inicio = dia.IndexOf('1');
                range.Fim = dia.LastIndexOf('1');
                Horarios.Add(range);
            }
        }
        public void SetHorariosRange(string binaryString)
        {
            SetHorariosRange(Funcoes.FromBinaryString(binaryString));
        }
        public byte[] GetHorariosRange()
        {
            string[]  dias = new string[7];
            foreach (HorarioRange r in this.Horarios)
            {
                string aux = "";
                for (int h = 0; h < 24; h++)
                    aux += h < r.Inicio || h > r.Fim ? "0" : "1";

                dias[(int)r.Dia] = aux;
            }
            string finalstring = String.Join("", dias);
            return Funcoes.FromBinaryString(finalstring);
        }
        public List<HorarioLDAP> HorariosLDAP { get; set; } = new List<HorarioLDAP>(7);
        public void SetHorariosLDAP(byte[] dados)
        {
            string binfinal = string.Join("", dados.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            HorariosLDAP.Clear();
            int l = 3 * 8;
            for (int d = 0; d < 7; d++)
            {
                HorarioLDAP horario = new HorarioLDAP();
                horario.Dia = ((DayOfWeek)d);
                string dia = binfinal.Substring(l * d, l);
                for (int h = 0; h < 24; h++)
                    horario.Horas[h] = dia[h] == '1';
                HorariosLDAP.Add(horario);
            }
        }
        public void SetHorariosLDAP(string binaryString)
        {
            SetHorariosLDAP(Funcoes.FromBinaryString(binaryString));
        }
        public byte[] GetHorariosLDAP()
        {
            string[] dias = new string[7];
            foreach (HorarioLDAP r in this.HorariosLDAP)
            {
                string aux = "";
                for (int h = 0; h < 24; h++)
                    aux += r.Horas[h] ? "1" : "0";

                dias[(int)r.Dia] = aux;
            }
            string finalstring = String.Join("", dias);
            return Funcoes.FromBinaryString(finalstring);
        }
    }

    public class HorarioRange
    {
        public string DiaNome { get=> this.Dia.ToString(); }
        public DayOfWeek Dia { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
    }
    public class HorarioLDAP
    {
        public string DiaNome { get => this.Dia.ToString(); }
        public DayOfWeek Dia { get; set; }
        public bool[] Horas { get; set; } = new bool[24];
    }
    
}
