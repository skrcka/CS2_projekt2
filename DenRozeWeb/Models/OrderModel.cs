using System;
using System.ComponentModel.DataAnnotations;

namespace DenRozeWeb.Models
{   
    public class OrderModel
    {
        [Required]
        public int OID { get; set;}

        [StringLength(250)]
        public string? Note { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Created_at { get; set;}

        [DataType(DataType.Date)]
        public DateTime? Edited_at { get; set; }

        [Required]
        [Range(0, 9999)]
        public int UID { get; set; }

        public override string ToString()
        {
            return $"{OID} {Note} {Created_at}";
        }
    }
}
