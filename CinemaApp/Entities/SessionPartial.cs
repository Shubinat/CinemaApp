using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Entities
{
    public partial class Session
    {
        public string Time => $"{DateTime.Hour:D2}:{DateTime.Minute:D2}";
    }
}
