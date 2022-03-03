using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAPI.Models
{
    /// <summary>
    /// Customer Model 
    /// </summary>
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        

        public int StateOfResidenceId { get; set; }
        [ForeignKey("StateOfResidenceId")]
        public StateOfResidence StateOfResidence { get; set; }


        public int LGAId { get; set; }
        [ForeignKey("LGAId")]
        public LGA LGA { get; set; }

    }


    /// <summary>
    /// StateOfResidence
    /// </summary>
    public class StateOfResidence
    {
        public int Id { get; set; }
        public string State { get; set; }
    }


    /// <summary>
    /// LGA Model
    /// </summary>
    public class LGA
    {
        public int Id { get; set; }
        public string LocalGovernmentArea { get; set; }


        public int StateOfResidenceId { get; set; }

        [ForeignKey("StateOfResidenceId")]
        public StateOfResidence StateOfResidence { get; set; }
    }
}
