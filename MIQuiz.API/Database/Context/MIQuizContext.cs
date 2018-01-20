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
            #region Fluent Definitions

            #region Table Mapping & Key Assignments
            modelBuilder.Entity<User>()
                .ToTable( "UsersTbl" )
                .HasKey( t => t.UserId );
            modelBuilder.Entity<QuizDef>()
                .ToTable( "QuizTbl" )
                .HasKey( q => q.QuizId);
            modelBuilder.Entity<JoinQuizQuestion>()
                .ToTable( "QuizQuestionTbl" )
                .HasKey( t => new { t.QuizId, t.QuestionId } );
            modelBuilder.Entity<JoinQuestionAnswer>()
                .ToTable( "QuestionAnswerTbl" )
                .HasKey( t => new { t.AnswerId, t.QuestionId } );
            modelBuilder.Entity<AnswerDef>()
                .ToTable( "AnswerTbl" )
                .HasKey( t => t.AnswerId );
            modelBuilder.Entity<QuestionDef>()
                .ToTable( "QuestionTbl" )
                .HasKey( t => t.QuestionId );
            modelBuilder.Entity<Image>()
                .ToTable( "ImagesTbl" )
                .HasKey( t => t.ImageId );
            modelBuilder.Entity<Video>()
                .ToTable( "VideosTbl" )
                .HasKey( t => t.VideoId );
            #endregion

            #region Table Property Annotations
            // Users Table
            modelBuilder.Entity<User>()
                .Property( u => u.UserName )
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property( u => u.FirstName )
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property( u => u.LastName )
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property( u => u.Role )
                .HasDefaultValue( 0 );

            // Quizes Table
            modelBuilder.Entity<QuizDef>()
                .Property( q => q.UserId )
                .IsRequired();
            modelBuilder.Entity<QuizDef>()
                .Property( q => q.Name )
                .IsRequired();

            // Quiz To Questions / Questions To Quizes Joiner Table
            modelBuilder.Entity<JoinQuizQuestion>()
                .Property( qq => qq.QuizId )
                .IsRequired();
            modelBuilder.Entity<JoinQuizQuestion>()
                .Property( qq => qq.QuestionId )
                .IsRequired();

            // Question To Answers / Answer To Questions Joiner Table
            modelBuilder.Entity<JoinQuestionAnswer>()
                .Property( qa => qa.QuestionId )
                .IsRequired();
            modelBuilder.Entity<JoinQuestionAnswer>()
                .Property( qa => qa.AnswerId )
                .IsRequired();

            // Answers Table
            modelBuilder.Entity<AnswerDef>()
                .Property( a => a.Text )
                .IsRequired();

            // Questions Table
            modelBuilder.Entity<QuestionDef>()
                .Property( qu => qu.Text )
                .IsRequired();
            modelBuilder.Entity<QuestionDef>()
                .Property( qu => qu.Type )
                .IsRequired();            

            // Video Table
            modelBuilder.Entity<Video>()
                .Property( v => v.VideoURI )
                .IsRequired();

            // Image Table

            #endregion

            #region Simple Relations (One-To-One, One-To-Many)
            modelBuilder.Entity<QuizDef>()
                .HasOne( q => q.Owner )
                .WithMany( u => u.CreatedQuizes ).OnDelete( DeleteBehavior.Restrict )
                .HasForeignKey( q => q.UserId )
                .HasPrincipalKey( u => u.UserId );
            modelBuilder.Entity<QuizDef>()
                .HasMany( q => q.Questions )
                .WithOne( qu => qu.Quiz ).OnDelete( DeleteBehavior.Restrict )
                .HasForeignKey( q => q.QuizId )
                .HasPrincipalKey( qu => qu.QuizId );

            modelBuilder.Entity<QuestionDef>()
                .HasOne( qu => qu.QuestionImage )
                .WithMany( qi => qi.Questions ).OnDelete( DeleteBehavior.Restrict )
                .HasForeignKey( qu => qu.ImageId );
            modelBuilder.Entity<QuestionDef>()
                .HasOne( qu => qu.QuestionVideo )
                .WithMany( qv => qv.Questions ).OnDelete( DeleteBehavior.Restrict )
                .HasForeignKey( qu => qu.VideoId );

            modelBuilder.Entity<AnswerDef>()
                .HasOne( an => an.AnswerImage )
                .WithMany( ai => ai.Answers ).OnDelete( DeleteBehavior.Restrict )
                .HasForeignKey( an => an.ImageId );
            modelBuilder.Entity<AnswerDef>()
                .HasOne( an => an.AnswerVideo )
                .WithMany( av => av.Anwsers ).OnDelete( DeleteBehavior.Restrict )
                .HasForeignKey( an => an.VideoId );
            #endregion

            #region Complex Relations (Many-To-Many)
            modelBuilder.Entity<JoinQuizQuestion>()
                .HasOne( qq => qq.Quiz )
                .WithMany( qu => qu.Questions ).OnDelete(DeleteBehavior.Restrict )
                .HasForeignKey( qq => qq.QuizId );
            modelBuilder.Entity<JoinQuizQuestion>()
                .HasOne( qq => qq.Question )
                .WithMany( qu => qu.Quizes ).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey( qq => qq.QuestionId );

            modelBuilder.Entity<JoinQuestionAnswer>()
                .HasOne( qa => qa.Question )
                .WithMany( a => a.Answers ).OnDelete( DeleteBehavior.Restrict )
                .HasForeignKey( qa => qa.QuestionId );
            modelBuilder.Entity<JoinQuestionAnswer>()
                .HasOne( qa => qa.Answer )
                .WithMany( a => a.Question ).OnDelete( DeleteBehavior.Restrict )
                .HasForeignKey( qa => qa.AnswerId );
            #endregion

            #endregion
        }
    }
}
