using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RCM.ViewModels.Mobile
{
    public class UpdateTaskModel
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public string Note { get; set; }
    }
}
