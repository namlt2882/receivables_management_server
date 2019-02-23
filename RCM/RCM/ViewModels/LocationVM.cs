using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    
    public class LocationVM : LocationCM
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
    public class LocationCM
    {

        [MaxLength(100)]
        public string Description { get; set; }

    }
    public class LocationUM : LocationVM
    {
    }
}
