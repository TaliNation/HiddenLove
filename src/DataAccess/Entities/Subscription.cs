using System;
using System.Collections.Generic;

#nullable disable

namespace HiddenLove.DataAccess.Entities
{
    public partial class Subscription : IEntity<int>
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CheckoutPrice { get; set; }
        public int? IdBillingdetails { get; set; }
        public int IdUser { get; set; }
        public int? IdOffer { get; set; }

        public virtual Offer IdOfferNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
