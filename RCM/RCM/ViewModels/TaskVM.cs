using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    public class TaskVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int StartTime { get; set; }
        public int ExecutionDay { get; set; }
        public int ReceivableId { get; set; }
    }
}
