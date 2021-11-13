using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model.ViewModels
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCategoryExtensionViewModel : ControllerBase
    {
        public string gameTitle { get; set; }
        public string CategoryName { get; set; }
    }
}
