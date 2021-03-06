﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database.Models
{
    public class JoinQuizQuestion
    {
        public int QuizId { get; set; }
        public virtual QuizDef Quiz { get; set; }

        public int QuestionId { get; set; }
        public virtual QuestionDef Question { get; set; }
    }
}
