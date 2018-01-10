using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models
{
    [Table("CredentialTbl")]
    public class Credential
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "A Valid Password Must Be Provided")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", 
            ErrorMessage = "Password must be between 8 and 20 characters in length, have at least one capital letter and one number.")]
        [MinLength(6),MaxLength(20)]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
