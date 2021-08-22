using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [RegularExpression("(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")] // https://stackoverflow.com/questions/12018245/regular-expression-to-validate-username första svaret på denna sida
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(100)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
