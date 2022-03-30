using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Entities
{
    public partial class Session
    {
        public string Time
        {
            get
            {
                return $"{DateTime.Hour:D2}:{DateTime.Minute:D2}";
            }
            set
            {
                var time = value.Split(':');
                DateTime = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day,
                    int.Parse(time[0]),
                    int.Parse(time[1]),
                    0); 
            }
        } 

        public string Date
        {
            get
            {
                return DateTime.ToString("dd.MM.yyyy");
            }
        }
    }
}
