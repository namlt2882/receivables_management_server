using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{

    public class UserVM : UserCM
    {
        public string Id { get; set; }
        public bool IsBanned { get; set; }
    }

    public class UserCM
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? LocationId { get; set; }
    }
    public class UserUM : UserVM
    {

    }

    public class UserLM
    {
        public string Id { get; set; }
        public bool IsBanned { get; set; }
        public int NumberOfAssignedReceivables { get; set; }
    }
}
