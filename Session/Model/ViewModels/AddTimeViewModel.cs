using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model.ViewModels
{
    public class AddTimeViewModel
    {
        [Required]
        public DateTime RunTime { get; set; }
        [DataType(DataType.Url)]
        [Required]
        public string Link { get; set; }

    }
}
