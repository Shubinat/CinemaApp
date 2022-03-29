//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaApp.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket()
        {
            this.TicketHistories = new HashSet<TicketHistory>();
        }
    
        public string Number { get; set; }
        public System.DateTime RealeseDate { get; set; }
        public int SessionID { get; set; }
        public int PlaceID { get; set; }
        public int StatusID { get; set; }
    
        public virtual Place Place { get; set; }
        public virtual Session Session { get; set; }
        public virtual TicketStatu TicketStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
    }
}