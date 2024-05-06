using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User_UserApi.Models;

namespace User_UserApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        [ValidateNever]
        public int Status {  get; set; }

        [ValidateNever]
        public int Count { get; set; }

        // Navigation property for Country
        [ForeignKey("CountryId")]
        [ValidateNever]
        public virtual Countries Country { get; set; }

        // Navigation property for Country
        [ForeignKey("StateId")]
        [ValidateNever]
        public virtual State States { get; set; }

        // Navigation property for Country
        [ForeignKey("CityId")]
        [ValidateNever]
        public virtual City Cities { get; set; }
    }
}
