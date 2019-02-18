using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    public class CustomerVM : CustomerCM
    {
        public int Id { get; set; }
    }
    public class CustomerCM
    {
        [MaxLength(15)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
    }
    public class CustomerUM : CustomerVM
    {
    }
}
