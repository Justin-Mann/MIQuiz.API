using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models {
    public class QuestionDef {
        [Key]
        public int QuestionId { get; set; }

        [DisplayName( "Question Text" )]
        [MinLength( 2, ErrorMessage = "Question Text must be at least 2 characters long." ),
         MaxLength( 250, ErrorMessage = "Question Text can only be 250 characters long." )]
        public string Text { get; set; }

        [DisplayName( "Question Type" )]
        [Required( ErrorMessage = "Question Type cannot be empty." )]
        public string Type { get; set; }

        [DisplayName( "Active" )]
        public bool IsActive { get; set; }

        public Image QuestionImage { get; set; }

        public Video QuestionVideo { get; set; }

        public virtual ICollection<JoinQuizQuestion> Quizes { get; } = new List<JoinQuizQuestion>();

        public virtual ICollection<JoinQuestionAnswer> Answers { get; } = new List<JoinQuestionAnswer>();
    }
}
