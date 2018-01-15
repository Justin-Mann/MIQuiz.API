using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MIQuizAPI.Database.Models;
using Microsoft.Extensions.Configuration;

namespace MIQuizAPI.Database.Context {
    public class MIQuizContext : DbContext {
        public MIQuizContext( DbContextOptions<MIQuizContext> options ) : base( options ) { }

        public DbSet<AnswerDef> Answers { get; set; }
        public DbSet<QuestionDef> Questions { get; set; }
        public DbSet<QuizDef> Quizes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<JoinQuestionAnswer>().ToTable( "QuestionAnswerTbl" ).HasKey( t => new { t.AnswerId, t.QuestionId } );
            modelBuilder.Entity<AnswerDef>().ToTable( "AnswerTbl" );
            modelBuilder.Entity<JoinQuizQuestion>().ToTable( "QuizQuestionTbl" ).HasKey( t => new { t.QuizId, t.QuestionId } );
            modelBuilder.Entity<QuestionDef>().ToTable( "QuestionTbl" );
            modelBuilder.Entity<QuizDef>().ToTable( "QuizTbl" );
            modelBuilder.Entity<User>().ToTable( "UsersTbl" );
            modelBuilder.Entity<Image>().ToTable( "ImagesTbl" );
            modelBuilder.Entity<Video>().ToTable( "VideosTbl" );
        }
    }
}
