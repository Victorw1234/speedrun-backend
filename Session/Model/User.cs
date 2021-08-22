using Session.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public byte[] salt { get; set; }
        public IEnumerable<Time> Times { get; set; }
        public IEnumerable<GameAdmin> Admin { get; set; }

    }
}
