using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MIQuizAPI.Database.Models;

namespace MIQuizAPI.Database.Context
{
    public class MIQuizContext : DbContext
    {
        public MIQuizContext(DbContextOptions<MIQuizContext> options) : base(options) { }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizDef> Quizes { get; set; }
    }
}
