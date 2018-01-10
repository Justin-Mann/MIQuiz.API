using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models {
    [Table( "QuizTbl" )]
    public class QuizDef {
        public QuizDef() {
            this.Owner = new User();
            this.Questions = new HashSet<QuestionDef>();
        }

        [Key]
        public int Id { get; set; }

        //TODO: Index On This Field
        public int OwnerId { get; set; }

        [DisplayName( "Quiz Name" )]
        [Required( ErrorMessage = "Quiz must have a name." )]
        [MinLength( 5, ErrorMessage = "Quiz Name must be at least 5 characters long." ),
         MaxLength( 20, ErrorMessage = "Quiz Name can only be 20 characters long." )]
        public string Name { get; set; }

        [DisplayName( "Quiz Description" )]
        [MaxLength( 500, ErrorMessage = "Description can only be 500 characters long." )]
        public string Description { get; set; }

        [DisplayName( "Quiz Instructions" )]
        [MaxLength( 500, ErrorMessage = "Description can only be 500 characters long." )]
        public string Instructions { get; set; }

        [DisplayName( "Quiz Grading Criteria" )]
        [MaxLength( 500, ErrorMessage = "Grading Criteria can only be 500 characters long." )]
        public string GradingCriteria { get; set; }

        [DisplayName( "Active" )]
        public bool IsActive { get; set; }

        [DisplayName( "Order" )]
        public int? Order { get; set; }

        [ForeignKey( "OwnerId" )]
        public virtual User Owner { get; set; }

        public virtual ICollection<QuestionDef> Questions { get; set; }
    }
}
