using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models {
    [Table( "UsersTbl" )]
    public class User {
        public User() {
            this.CreatedQuizes = new List<QuizDef>();
        }

        [Key]
        public int Id { get; set; }

        [DisplayName( "First Name" )]
        [MinLength( 2, ErrorMessage = "First Name must be at least 2 characters long." ),
         MaxLength( 20, ErrorMessage = "First Name can only be 20 characters long." )]
        public string FirstName { get; set; }

        [DisplayName( "Last Name" )]
        [MinLength( 2, ErrorMessage = "Last Name must be at least 2 characters long." ),
         MaxLength( 20, ErrorMessage = "Last Name can only be 20 characters long." )]
        public string LastName { get; set; }

        [DisplayName( "User Name" )]
        [Required( ErrorMessage = "A valid Username must be provided" )]
        [StringLength( 25, MinimumLength = 2 )]
        public string UserName { get; set; }

        [DisplayName( "UserRole" )]
        public int Role { get; set; }

        [DisplayName( "Active" )]
        public bool IsActive { get; set; }

        public virtual ICollection<QuizDef> CreatedQuizes { get; set; }
    }
}
