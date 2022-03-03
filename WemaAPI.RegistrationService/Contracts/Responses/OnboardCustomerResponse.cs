using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WemaAPI.RegistrationService.Contracts.Responses
{
    public class OnboardCustomerResponse
    {
        public class CreateUserResponse
        {
            public string Message { get; set; }
        }
        public class ErrorMsg
        {
            public string Description { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
        }
    }
}
