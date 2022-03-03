using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WemaAPI.OTPService.Contracts.Request
{
    public class OTPRequest
    {
        public class SendOTPRequest
        {
            [Required]
            public string PhoneNumber { get; set; }
        }
    }
}
