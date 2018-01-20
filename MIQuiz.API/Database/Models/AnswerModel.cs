using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models {
    public class AnswerDef {
        [Key]
        public int AnswerId { get; set; }

        public int? ImageId { get; set; }

        public int? VideoId { get; set; }

        [DisplayName( "Answer Text" )]
        [MinLength( 2, ErrorMessage = "Answer Text must be at least 2 characters long." ),
         MaxLength( 250, ErrorMessage = "Answer Text can only be 250 characters long." )]
        public string Text { get; set; }

        public Image AnswerImage { get; set; }

        public Video AnswerVideo { get; set; }

        [DisplayName( "Active" )]
        public bool IsActive { get; set; }

        public virtual ICollection<JoinQuestionAnswer> Question { get; } = new List<JoinQuestionAnswer>();
    }
}
