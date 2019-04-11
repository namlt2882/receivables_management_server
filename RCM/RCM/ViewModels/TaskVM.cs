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
    public class TaskMobileVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime ExecutionDay { get; set; }
        public DateTime? UpdateDay { get; set; }
        public string Evidence { get; set; }
        public int ReceivableId { get; set; }
        public string CollectorName { get; set; }
        public string UserId { get; set; }
        public string Partner { get; set; }
        public string Debtor { get; set; }
    }
}
