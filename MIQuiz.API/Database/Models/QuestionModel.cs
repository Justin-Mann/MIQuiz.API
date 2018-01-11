using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models {
    [Table( "QuestionTbl" )]
    public class QuestionDef {
        public QuestionDef() {
            this.ConsumerQuiz = new QuizDef();
            this.Answers = new List<AnswerDef>();
        }

        [Key]
        public int Id { get; set; }

        [DisplayName( "Question Text" )]
        [MinLength( 2, ErrorMessage = "Question Text must be at least 2 characters long." ),
         MaxLength( 250, ErrorMessage = "Question Text can only be 250 characters long." )]
        public string Text { get; set; }

        [DisplayName( "Question Type" )]
        [Required( ErrorMessage = "Question Type cannot be empty." )]
        public string Type { get; set; }

        [DisplayName( "Question Image" )]
        public byte[] ImageBlob { get; set; }

        [DisplayName( "Question Image URL (external image)" )]
        public string ImageURI { get; set; }

        [DisplayName( "Question Video URL (external image)" )]
        public string videoURI { get; set; }

        [DisplayName( "Active" )]
        public bool IsActive { get; set; }

        [DisplayName( "Order" )]
        public int? Order { get; set; }

        public virtual ICollection<AnswerDef> Answers { get; set; }

        public virtual QuizDef ConsumerQuiz { get; set; }
    }
}
