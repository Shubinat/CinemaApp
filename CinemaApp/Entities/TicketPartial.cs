using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Entities
{
    public partial class Ticket
    {
        public decimal Price
        {
            get
            {
                return App.Context.TicketPrices.ToList().FirstOrDefault(x => x.SessionID == Session.ID && x.SectorID == Place.HallSectorID).Price;
            }
        }
    }
}
