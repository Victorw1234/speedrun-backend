using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<CategoryExtension> CategoryExtensions { get; set; }
        public IEnumerable<GameAdmin> Admins { get; set; }

    }
}
