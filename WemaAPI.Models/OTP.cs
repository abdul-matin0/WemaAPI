using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAPI.Models
{
    /// <summary>
    /// Model for customer OTP 
    /// </summary>
    public class OTP
    {
        [Key]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
