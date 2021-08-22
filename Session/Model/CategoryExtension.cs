using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model
{
    public class CategoryExtension
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlTitle { get
            {
                return Title.ToLower().Trim().Replace(" ", "_");
            }
        }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public IEnumerable<Time> Times { get; set; }
    }
}
