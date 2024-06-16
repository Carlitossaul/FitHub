using System.ComponentModel.DataAnnotations;

namespace FitHub.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public int NationalId { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
