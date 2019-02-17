using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class Notification : BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string NData { get; set; }
        public bool IsSeen { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
