using System.ComponentModel.DataAnnotations;

namespace DenRozeWeb.Models
{   
    public class UserModel
    {
        [Required]
        public int UID { get; set;}

        [Required]
        public string Login { get; set;}

        [Required]
        public string Full_name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public override string ToString()
        {
            return $"{UID} {Login} {Full_name} {Phone} {Email} {Address}";
        }
    }
}
