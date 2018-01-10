using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MIQuizAPI.Database.Models
{
    [Table("SavedQuizTbl")]
    public class SavedQuiz
    {
        public SavedQuiz() {
            this.User = new User();
            this.QuizDefinition = new QuizDef();
        }

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public int QuizDefId { get; set; }

        [DisplayName("Complete")]
        public bool IsComplete { get; set; }

        [DisplayName("Number Correct")]
        public int? NumberCorrect { get; set; }

        [DisplayName("Total Questions")]
        public int? TotalQuestions { get; set; }

        [DisplayName("Completion Date")]
        public DateTime CompletionDate { get; set; }

        [DisplayName("Last Modified")]
        public DateTime LastModifiedDate { get; set; }

        [DisplayName("JSON Snapshot Of Quiz Progress")]
        public string JSONQuizState { get; set; }

        [ForeignKey("User")]
        public virtual User User { get; set; }

        [ForeignKey("Quiz")]
        public virtual QuizDef QuizDefinition { get; set; }
    }
}
