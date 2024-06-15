using System.ComponentModel.DataAnnotations;

namespace FitHub.Models.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "NationalId is required")]
        public int NationalId { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "Photo is required")]
        public string Photo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
