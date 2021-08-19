using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<Time> Times { get; set; }
        public IEnumerable<GameAdmin> Admin { get; set; }

    }
}
