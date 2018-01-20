using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models
{
    public class JoinQuestionAnswer
    {
        public int QuestionId { get; set; }
        public virtual QuestionDef Question { get; set; }

        public int AnswerId { get; set; }
        public virtual AnswerDef Answer { get; set; }

        [DisplayName( "Correct Answer" )]
        public bool IsCorrectAnswer { get; set; }
    }
}
