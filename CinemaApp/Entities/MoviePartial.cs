using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Entities
{
    public partial class Movie
    {
        public string RentalText
        {
            get
            {
                var now = DateTime.Now;
                if (now <= EndRentalDate && now >= StartRentalDate)
                {
                    return "В прокате";
                }
                else if(now > EndRentalDate)
                {
                    return "Не в прокате";
                }
                else
                {
                    return "Скоро";
                }
            }
        }
    }
}
