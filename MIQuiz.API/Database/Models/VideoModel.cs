using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models {
    public class Video {
        [Key]
        public int VideoId { get; set; }

        [DisplayName( "Video URL/URI" )]
        public string VideoURI { get; set; }

        [DisplayName( "Active" )]
        public bool IsActive { get; set; }

        public virtual IEnumerable<QuestionDef> Questions { get; set; }

        public virtual IEnumerable<AnswerDef> Anwsers { get; set; }
    }
}
