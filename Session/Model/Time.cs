using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model
{
    public class Time
    {
        public int Id { get; set; }
        public DateTime RunTime { get; set; }
        public string Link { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryExtensionId { get; set; }
        public CategoryExtension CategoryExtension { get; set; }

    }
}
