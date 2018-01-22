using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MIQuizAPI.Database.Models;
using MIQuizAPI.Database.Context;
using System;
using System.Threading.Tasks;

namespace MIQuizAPI.Repository {
    #region User Repo Concrete Implementation
    public class UserRepository : IUserRepository {
        private readonly MIQuizContext _quizContext;

        public UserRepository( MIQuizContext quizContext ) {
            _quizContext = quizContext;
        }

        #region Users Repo Members
        // Add A New User
        public async Task Add( User user ) {
            if( user != null ) {
                await _quizContext.Users.AddAsync( user );
                await _quizContext.SaveChangesAsync();
            }
        }

        public async Task Remove( int id ) {
            var user = await _quizContext.Users.FindAsync( id );
            if( user != null ) {
                _quizContext.Users.Remove( user );
                await _quizContext.SaveChangesAsync();
            }
        }

        public async Task Update( User user ) {
            if( user != null ) {
                _quizContext.Users.Update( user );
                await _quizContext.SaveChangesAsync();
            }
        }

        // List All Users
        public async Task<IEnumerable<User>> ListAll() {
            return await _quizContext.Users
                               .Where( u => u.IsActive )
                               .Select( u => new User {
                                   UserName = u.UserName,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName } )
                               .ToListAsync();
        }

        // Get Users
        public async Task<IEnumerable<User>> GetAll() {
            return await _quizContext.Users
                               .Where( u => u.IsActive )
                               .ToListAsync();
        }

        // Get User(s) By Id(s)
        public async Task<IEnumerable<User>> GetByIds( List<int> ids ) {
            return await _quizContext.Users
                               .Where( u => ids.Contains( u.UserId ) && u.IsActive )
                               .ToListAsync();
        }

        // Get User By Id
        public async Task<User> GetById( int id ) {
            return await _quizContext.Users
                               .SingleOrDefaultAsync( u => id.Equals( u.UserId ) && u.IsActive );
        }

        // Get User & All Owned Quiz Info By Id
        public async Task<User> GetByUserId( int id ) {
            return await _quizContext.Users
                               .Include( u => u.CreatedQuizes )
                                .ThenInclude( q => q.Questions )
                                  .ThenInclude( questions => questions.Question )
                                    .ThenInclude( question => question.Answers )
                                      .ThenInclude( answers => answers.Answer )
                               .SingleOrDefaultAsync( u => id.Equals( u.UserId ) );
        }
        #endregion
    }
    #endregion

    #region Quiz Repo Concrete Implementation
    public class QuizRepository : IQuizRepository {
        private readonly MIQuizContext _quizContext;

        public QuizRepository( MIQuizContext quizContext ) {
            _quizContext = quizContext;
        }

        #region Quiz Repo Members
        // Add A New Quiz
        public async Task Add( QuizDef quiz ) {
            if( quiz != null ) {
                await _quizContext.Quizes.AddAsync( quiz );
                await _quizContext.SaveChangesAsync();
            }
        }

        public async Task Remove( int id ) {
            var quiz = await _quizContext.Quizes.FindAsync( id );
            if ( quiz != null ) {
                _quizContext.Quizes.Remove( quiz );
                await _quizContext.SaveChangesAsync();
            }
        }

        public async Task Update( QuizDef quiz ) {
            if( quiz != null ) {
                _quizContext.Quizes.Update( quiz );
                await _quizContext.SaveChangesAsync();
            }
        }

        // List All Quizes
        public async Task<IEnumerable<QuizDef>> ListAll() {
            return await _quizContext.Quizes
                               .Where( q => q.IsActive )
                               .Select( q => new QuizDef {
                                   Name = q.Name,
                                   Description = q.Description,
                                   GradingCriteria = q.GradingCriteria,
                                   Instructions = q.Instructions } )
                               .ToListAsync();
        }

        // Get All Quizes
        public async Task<IEnumerable<QuizDef>> GetAll() {
            return await _quizContext.Quizes
                               .Include( q => q.Questions )
                                .ThenInclude( questions => questions.Question )
                                  .ThenInclude( question => question.Answers )
                                    .ThenInclude( answers => answers.Answer )
                               .Where( q => q.IsActive )
                               .ToListAsync();
        }

        //Get Quiz(es) By Id(s)
        public async Task<IEnumerable<QuizDef>> GetByIds( List<int> ids ) {
            return await _quizContext.Quizes
                               .Include( q => q.Questions )
                                .ThenInclude( questions => questions.Question )
                                  .ThenInclude( question => question.Answers )
                                    .ThenInclude( answers => answers.Answer )
                               .Where( q => ids.Contains( q.QuizId ) && q.IsActive )
                               .ToListAsync();
        }

        //Get Single Quiz By Id
        public async Task<QuizDef> GetById( int id ) {
            return await _quizContext.Quizes
                               .Include( q => q.Questions )
                                .ThenInclude( questions => questions.Question )
                                  .ThenInclude( question => question.Answers )
                                    .ThenInclude( answers => answers.Answer )
                               .SingleOrDefaultAsync( q => id.Equals( q.QuizId ) && q.IsActive );
        }

        ////Get Quizes By OwnerId
        public async Task<IEnumerable<QuizDef>> GetByOwnerIds( List<int> ids ) {
            return await _quizContext.Quizes
                               .Where( q => ids.Contains( q.UserId ) && q.IsActive )
                               .Include( q => q.Questions )
                                .ThenInclude( questions => questions.Question )
                                  .ThenInclude( question => question.Answers )
                                    .ThenInclude( answers => answers.Answer )
                               .ToListAsync();
        }
        #endregion
    }
    #endregion

    #region Question Repo Concrete Implementation
    public class QuestionRepository : IQuestionRepository {
        private readonly MIQuizContext _quizContext;

        public QuestionRepository( MIQuizContext quizContext ) {
            _quizContext = quizContext;
        }

        #region Questions Repo Members
        // Add A New Question
        public async Task Add( QuestionDef question ) {
            if( question != null ) {
                await _quizContext.Questions.AddAsync( question );
                await _quizContext.SaveChangesAsync();
            }
        }

        // Remove An Existing Question
        public async Task Remove( int id ) {
            var question = await _quizContext.Questions.FindAsync( id );
            if( question != null ) {
                _quizContext.Questions.Remove( question );
                await _quizContext.SaveChangesAsync();
            }
        }

        // Update An Existing Question
        public async Task Update( QuestionDef question ) {
            if( question != null ) {
                _quizContext.Questions.Update( question );
                await _quizContext.SaveChangesAsync();
            }
        }

        //List Questions
        public async Task<IEnumerable<QuestionDef>> ListAll() {
            return await _quizContext.Questions
                               .Include( question => question.QuestionImage )
                               .Include( question => question.QuestionVideo )
                               .Include( question => question.Answers )
                                 .ThenInclude( answers => answers.Answer )
                               .Where( question => question.IsActive )
                               .Select( q => new QuestionDef {
                                   Text = q.Text,
                                   Type = q.Type } )
                               .ToListAsync();
        }

        // Get All Quizes
        public async Task<IEnumerable<QuestionDef>> GetAll() {
            return await _quizContext.Questions
                               .Include( question => question.QuestionImage )
                               .Include( question => question.QuestionVideo )
                               .Include( question => question.Answers )
                                 .ThenInclude( answers => answers.Answer )
                               .Where( question => question.IsActive )
                               .ToListAsync();
        }

        //List Questions On A Quiz By QuizId
        public async Task<IEnumerable<QuestionDef>> ListByQuizId( int quizId ) {
            return await _quizContext.Questions
                               .Include( question => question.QuestionImage )
                               .Include( question => question.QuestionVideo )
                               .Include( question => question.Answers )
                                 .ThenInclude( answers => answers.Answer )
                                   .ThenInclude( ai => ai.AnswerImage )
                               .Where( question => question.Quizes.Select( quiz => quiz.QuestionId ).Contains( quizId ) &&
                                                   question.IsActive )
                               .Select( q => new QuestionDef {
                                   Text = q.Text,
                                   Type = q.Type } )
                               .ToListAsync();
        }

        //Get Questions By Ids
        public async Task<IEnumerable<QuestionDef>> GetByIds( List<int> ids ) {
            return await _quizContext.Questions
                               .Include( question => question.QuestionImage )
                               .Include( question => question.QuestionVideo )
                               .Include( question => question.Answers )
                                 .ThenInclude( answers => answers.Answer )
                                   .ThenInclude( ai => ai.AnswerImage )
                               .Where( q => ids.Contains( q.QuestionId ) && q.IsActive )
                               .ToListAsync();
        }

        //Get Question By Id
        public async Task<QuestionDef> GetById( int id ) {
            return await _quizContext.Questions
                               .Include( q => q.QuestionImage )
                               .Include( q => q.QuestionVideo )
                               .Include( question => question.Answers )
                                 .ThenInclude( a => a.Answer )
                                   .ThenInclude( ai => ai.AnswerImage )
                               .SingleOrDefaultAsync( q => id.Equals( q.QuestionId ) && q.IsActive );
        }

        //Get Correct Answer(s) For A Question By Question Id
        public async Task<IEnumerable<AnswerDef>> GetCorrectAnswersByQuestionId( int questionId ) {
            return await _quizContext.Answers
                              .Include( a => a.AnswerImage )
                              .Include( a => a.AnswerVideo )
                              .Include( a => a.Question.Where( q => questionId.Equals( q.QuestionId ) && q.IsCorrectAnswer ) )
                              .Where( a => a.IsActive )
                              .ToListAsync();
        }
        #endregion
    }
    #endregion

    #region Answer Repo Concrete Implementation
    public class AnswerRepository : IAnswerRepository {
        private readonly MIQuizContext _quizContext;

        public AnswerRepository( MIQuizContext quizContext ) {
            _quizContext = quizContext;
        }

        #region Answers Repo Members
        // Add A New Answer
        public async Task Add( AnswerDef answer ) {
            if( answer != null ) {
                await _quizContext.Answers.AddAsync( answer );
                await _quizContext.SaveChangesAsync();
            }
        }

        // Remove An Existing Answer
        public async Task Remove( int id ) {
            var answer = await _quizContext.Answers.FindAsync( id );
            if( answer != null ) {
                _quizContext.Answers.Remove( answer );
                await _quizContext.SaveChangesAsync();
            }
        }

        // Update An Existing Answer
        public async Task Update( AnswerDef answer ) {
            if( answer != null ) {
                _quizContext.Answers.Update( answer );
                await _quizContext.SaveChangesAsync();
            }
        }

        //List Answers To A Question By QuestionId
        public async Task<IEnumerable<AnswerDef>> ListByQuestionId( int questionId ) {
            return await _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Select( a => new AnswerDef {
                                   Text = a.Text,
                                   AnswerImage = a.AnswerImage,
                                   AnswerVideo = a.AnswerVideo
                               } )
                               .Where( a => a.Question.Select( q => q.QuestionId ).Contains( questionId ) && a.IsActive )
                               .ToListAsync();
        }

        //Get All Answers To A Set Of Questions By QuestionId
        public async Task<IEnumerable<AnswerDef>> GetByQuestionIds( List<int> questionIds ) {
            return await _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Where( a => a.Question.Any( q => questionIds.Contains( q.QuestionId ) ) && a.IsActive )
                               .ToListAsync();
        }

        //Get All Answers To A Question By QuestionId
        public async Task<IEnumerable<AnswerDef>> GetByQuestionId( int questionId ) {
            return await _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Where( a => a.Question.Any( q => questionId.Equals( q.QuestionId ) ) && a.IsActive )
                               .ToListAsync();
        }

        //Get Single/Several Answers By Ids
        public async Task<IEnumerable<AnswerDef>> GetByIds( List<int> ids ) {
            return await _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Where( a => ids.Contains( a.AnswerId ) && a.IsActive )
                               .ToListAsync();
        }

        //Get Correct Answer By QuestionId
        public async Task<IEnumerable<AnswerDef>> GetCorrectAnswersByQuestionId( int questionId ) {
            return await _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Include( a => a.Question.Where( q => questionId.Equals( q.QuestionId ) && q.IsCorrectAnswer ) )
                                .ThenInclude( q => q.Question )
                                  .ThenInclude( qu => qu.QuestionImage )
                               .Where( a => a.IsActive )
                               .ToListAsync();
        }

        //Get Answer By Id
        public async Task<AnswerDef> GetById( int id ) {
            return await _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .SingleOrDefaultAsync( a => id.Equals( a.AnswerId ) && a.IsActive );
        }

        //List Correct Questions/Answers For A Quiz
        public async Task<IEnumerable<QuestionDef>> GetAnswersByQuizId( int quizId ) {
            var quiz = await _quizContext.Quizes
                                    .Include( q => q.Questions )
                                     .ThenInclude( questions => questions.Question )
                                      .ThenInclude( question => question.Answers.Where( a => a.IsCorrectAnswer ) )
                                       .ThenInclude( answers => answers.Answer )
                                    .SingleOrDefaultAsync( (q => quizId.Equals( q.QuizId ) && q.IsActive) );

            return quiz.Questions.Select( q => q.Question ).ToList();
        }
        #endregion 
    }
    #endregion

    #region Images Repo Concrete Implementation
    public class ImageRepository : IImageRepository {
        private readonly MIQuizContext _quizContext;

        public ImageRepository( MIQuizContext quizContext ) {
            _quizContext = quizContext;
        }

        // Add A New Image
        public async Task Add( Image image ) {
            if( image != null ) {
                await _quizContext.Images.AddAsync( image );
                await _quizContext.SaveChangesAsync();
            }
        }

        // Remove A Image
        public async Task Remove( int id ) {
            var image = await _quizContext.Images.FindAsync( id );
            if( image != null ) {
                _quizContext.Images.Remove( image );
                await _quizContext.SaveChangesAsync();
            }
        }

        // Update A Image
        public async Task Update( Image image ) {
            if( image != null ) {
                _quizContext.Images.Update( image );
                await _quizContext.SaveChangesAsync();
            }
        }

        //List All Images
        //TODO

        //Get All Images
        //TODO

        //Get Images By Ids
        public async Task<IEnumerable<Image>> GetByIds( List<int> ids ) {
            return await _quizContext.Images
                            .Where( i => ids.Contains( i.ImageId ) && i.IsActive )
                            .ToListAsync();
        }

        //Get Image By Id
        public async Task<Image> GetById( int id ) {
            return await _quizContext.Images
                            .SingleOrDefaultAsync( i => id.Equals( i.ImageId ) && i.IsActive );
        }

        //Get Image By Question Id
        //TODO
    }
    #endregion

    #region Videos Repo Concrete Implementation
    public class VideoRepository : IVideoRepository {
        private readonly MIQuizContext _quizContext;

        public VideoRepository( MIQuizContext quizContext ) {
            _quizContext = quizContext;
        }

        // Add A New Video
        public async Task Add( Video video ) {
            if( video != null ) {
                await _quizContext.Videos.AddAsync( video );
                await _quizContext.SaveChangesAsync();
            }
        }

        // Remove A Video
        public async Task Remove( int id ) {
            var video = await _quizContext.Videos.FindAsync( id );
            if( video != null ) {
                _quizContext.Videos.Remove( video );
                await _quizContext.SaveChangesAsync();
            }
        }

        // Update A Video
        public async Task Update( Video video ) {
            if( video != null ) {
                _quizContext.Videos.Update( video );
                await _quizContext.SaveChangesAsync();
            }
        }

        //List All Images
        //TODO

        //Get All Images
        //TODO

        //Get Videos By Ids
        public async Task<IEnumerable<Video>> GetByIds( List<int> ids ) {
            return await _quizContext.Videos
                            .Where( i => ids.Contains( i.VideoId ) && i.IsActive )
                            .ToListAsync();
        }

        //Get Video By Id
        public async Task<Video> GetById( int id ) {
            return await _quizContext.Videos
                            .SingleOrDefaultAsync( i => id.Equals( i.VideoId ) && i.IsActive );
        }

        //Get Image By Question 
        //TODOId
    }
    #endregion
}
