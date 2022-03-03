using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WemaAPI.CustomerProfileService.Contracts.Responses
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StateOfResidence { get; set; }
        public string LGA { get; set; }
    }
}
