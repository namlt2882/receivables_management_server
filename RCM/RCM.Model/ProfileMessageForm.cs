using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RCM.Model
{
    public class ProfileMessageForm : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        
    }
}
