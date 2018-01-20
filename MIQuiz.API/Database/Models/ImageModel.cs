using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models {
    public class Image {
        [Key]
        public int ImageId { get; set; }

        [DisplayName( "Image BLOB" )]
        public byte[] ImageBLOB { get; set; }

        [DisplayName( "Image URL/URI" )]
        public string ImageURI { get; set; }

        [DisplayName( "Active" )]
        public bool IsActive { get; set; }

        public virtual IEnumerable<QuestionDef> Questions { get; set; }

        public virtual IEnumerable<AnswerDef> Answers { get; set; }
    }
}
