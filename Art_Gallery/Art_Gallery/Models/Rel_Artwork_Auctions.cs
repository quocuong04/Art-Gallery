//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Art_Gallery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rel_Artwork_Auctions
    {
        public int ArtworkId { get; set; }
        public int AuctionId { get; set; }
        public string Name { get; set; }
    
        public virtual Artwork Artwork { get; set; }
        public virtual Auction Auction { get; set; }
    }
}
