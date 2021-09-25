using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAD
{
    public class LogonTime
    {
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public int TimeZoneOffSet { get; set; }

        public LogonTime(DayOfWeek dayOfWeek, DateTime beginTime, DateTime endTime)
        {
            DayOfWeek = dayOfWeek;
            BeginTime = beginTime;
            EndTime = endTime;

            SetOffset(TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));//("E. South America Standard Time"));
           //ValidateTimes();
        }

        public LogonTime(DayOfWeek dayOfWeek, TimeSpan begin, TimeSpan end)
        {
            DayOfWeek = dayOfWeek;
            BeginTime = new DateTime(begin.Ticks);
            EndTime = new DateTime(end.Ticks);

            SetOffset(TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
           // ValidateTimes();
        }

        public LogonTime(DayOfWeek dayOfWeek, DateTime beginTime, DateTime endTime, TimeZoneInfo timeZone)
        {
            DayOfWeek = dayOfWeek;
            BeginTime = beginTime;
            EndTime = endTime;

            SetOffset(timeZone);
//ValidateTimes();
        }

        public LogonTime(DayOfWeek dayOfWeek, TimeSpan begin, TimeSpan end, TimeZoneInfo timeZone)
        {
            DayOfWeek = dayOfWeek;
            BeginTime = new DateTime(begin.Ticks);
            EndTime = new DateTime(end.Ticks);

            SetOffset(timeZone);
           // ValidateTimes();
        }

        private void SetOffset(TimeZoneInfo timeZone)
        {
            TimeZoneOffSet = (-1) * (timeZone.BaseUtcOffset.Hours);
            //TimeZoneOffSet = timeZone.IsDaylightSavingTime(DateTime.Now) ? (-1) * (timeZone.GetUtcOffset(DateTime.Now).Hours - 1) : (-1)*(timeZone.GetUtcOffset(DateTime.Now).Hours);
        }

        private void ValidateTimes()
        {
            //if ( (EndTime.Hour < BeginTime.Hour) && 
            //    (EndTime.Hour != 0))
            //{
            //   // throw new ArgumentException("O horário de início não pode ser posterior ao horário de término.");

            //    MessageBox.Show("O horário de início não pode ser posterior ao horário de término.", "SGAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //if (EndTime.Hour == 0)
            ////{

            ////} 

            

            if (EndTime.Hour < BeginTime.Hour && (EndTime.Hour == 0 && EndTime.Day == BeginTime.Day))
            {
                //throw new ArgumentException("Begin time cannot be after End time.");
                MessageBox.Show("O horário de início não pode ser posterior ao horário de término.", "SGAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
