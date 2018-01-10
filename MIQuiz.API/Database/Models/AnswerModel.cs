using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models
{
    [Table("AnswerTbl")]
    public class AnswerDef
    {
        public AnswerDef()
        {
            this.Question = new QuestionDef();
        }

        [Key]
        public int Id { get; set; }

        //TODO: Index On This
        public int QuestionId { get; set; }

        [DisplayName("Answer Text")]
        [MinLength(2, ErrorMessage = "Answer Text must be at least 2 characters long."),
         MaxLength(250, ErrorMessage = "Answer Text can only be 250 characters long.")]
        public string Text { get; set; }

        [DisplayName("Correct Answer")]
        public bool IsCorrectAnswer { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Order")]
        public int? Order { get; set; }

        [ForeignKey("QuestionId")]
        public virtual QuestionDef Question { get; set; }
    }
}
