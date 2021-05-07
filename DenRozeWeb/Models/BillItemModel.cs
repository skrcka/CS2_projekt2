using System;
using System.ComponentModel.DataAnnotations;

namespace DenRozeWeb.Models
{   
    public class BillItemModel
    {
        [Required]
        public int BIID { get; set;}

        [Required]
        [Range(0, 100)]
        public int Count { get; set; }

        [Required]
        [Range(0, 9999)]
        public int IID { get; set;}

        [Range(0, 9999)]
        public int? BID { get; set; }

        [Range(0, 9999)]
        public int? OID { get; set; }

        public override string ToString()
        {
            return $"{BIID} {Count} {BID} {OID}";
        }
    }
}
