using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MIQuizAPI.Database.Models;
using MIQuizAPI.Database.Context;

namespace MIQuizAPI.Repository {
    #region User Repo Concrete Implementation
    public class UserRepository : IUserRepository {
        private readonly MIQuizContext _quizContext;

        public UserRepository( MIQuizContext quizContext ) {
            _quizContext = quizContext;
        }

        #region Users Repo Members
        // Add A New User
        public void AddUser( User user ) {
            // more to do here...
            //example here ... http://www.thereformedprogrammer.net/updating-many-to-many-relationships-in-entity-framework-core/
            _quizContext.Add( user );
        }

        /* TODO
        void RemoveUser(int id);
        void Update(Contacts item);
         * */

        // List All Users
        public HashSet<User> ListUsers() {
            return _quizContext.Users
                               .Where( u => u.IsActive )
                               .Select( u => new User {
                                   UserName = u.UserName,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName } )
                               .ToHashSet();
        }

        // Get Users
        public HashSet<User> GetUsers() {
            return _quizContext.Users
                               .Where( u => u.IsActive )
                               .ToHashSet();
        }

        // Get User(s) By Id(s)
        public HashSet<User> GetUsers( List<int> ids ) {
            return _quizContext.Users
                               .Where( u => ids.Contains( u.UserId ) && u.IsActive )
                               .ToHashSet();
        }

        // Get User By Id
        public User GetUser( int id ) {
            return _quizContext.Users
                               .SingleOrDefault( u => id.Equals( u.UserId ) && u.IsActive );
        }

        // Get User & All Owned Quiz Info By Id
        public User GetAdminUserQuizes( int id ) {
            return _quizContext.Users
                               .Include( u => u.CreatedQuizes )
                                .ThenInclude( q => q.Questions )
                                  .ThenInclude( questions => questions.Question )
                                    .ThenInclude( question => question.Answers )
                                      .ThenInclude( answers => answers.Answer )
                               .SingleOrDefault( u => id.Equals( u.UserId ) && u.Role >= 1 );
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
        public void AddQuiz( QuizDef quiz ) {
            // more to do here...
            //example here ... http://www.thereformedprogrammer.net/updating-many-to-many-relationships-in-entity-framework-core/
            _quizContext.Add( quiz );
        }

        /* TODO
        void RemoveQuiz(int id);
        void UpdateQuiz(QuizDef quiz);
         * */

        // List All Quizes
        public HashSet<QuizDef> ListQuizes() {
            return _quizContext.Quizes
                               .Where( q => q.IsActive )
                               .Select( q => new QuizDef {
                                   Name = q.Name,
                                   Description = q.Description,
                                   GradingCriteria = q.GradingCriteria,
                                   Instructions = q.Instructions } )
                               .ToHashSet();
        }

        // Get All Quizes
        public HashSet<QuizDef> GetQuizes() {
            return _quizContext.Quizes
                               .Include( q => q.Questions )
                                .ThenInclude( questions => questions.Question )
                                  .ThenInclude( question => question.Answers )
                                    .ThenInclude( answers => answers.Answer )
                               .Where( q => q.IsActive )
                               .ToHashSet();
        }

        //Get Quiz(es) By Id(s)
        public HashSet<QuizDef> GetQuizes( List<int> ids ) {
            return _quizContext.Quizes
                               .Include( q => q.Questions )
                                .ThenInclude( questions => questions.Question )
                                  .ThenInclude( question => question.Answers )
                                    .ThenInclude( answers => answers.Answer )
                               .Where( q => ids.Contains( q.QuizId ) && q.IsActive )
                               .ToHashSet();
        }

        //Get Single Quiz By Id
        public QuizDef GetQuiz( int id ) {
            return _quizContext.Quizes
                               .Include( q => q.Questions )
                                .ThenInclude( questions => questions.Question )
                                  .ThenInclude( question => question.Answers )
                                    .ThenInclude( answers => answers.Answer )
                               .SingleOrDefault( q => id.Equals( q.QuizId ) && q.IsActive );
        }

        ////Get Quizes By OwnerId
        public HashSet<QuizDef> GetQuizesByOwnerIds( List<int> ids ) {
            return _quizContext.Quizes
                               .Where( q => ids.Contains( q.UserId ) && q.IsActive )
                               .Include( q => q.Questions )
                                .ThenInclude( questions => questions.Question )
                                  .ThenInclude( question => question.Answers )
                                    .ThenInclude( answers => answers.Answer )
                               .ToHashSet();
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
        public void AddQuestion( QuestionDef question ) {
            // more to do here...
            //example here ... http://www.thereformedprogrammer.net/updating-many-to-many-relationships-in-entity-framework-core/
            //_quizContext.Add( question );
            //_quizContext.SaveChanges();
        }

        /* TODO
        void RemoveQuiz(int id);
        void UpdateQuiz(QuizDef quiz);
         * */


        //List Questions
        public HashSet<QuestionDef> ListQuestions() {
            return _quizContext.Questions
                               .Where( question => question.IsActive )
                               .Select( q => new QuestionDef {
                                   Text = q.Text,
                                   Type = q.Type } )
                               .ToHashSet();
        }

        //List Questions On A Quiz By QuizId
        public HashSet<QuestionDef> ListQuestionsForQuiz( int quizId ) {
            return _quizContext.Questions
                               .Where( question => question.Quizes.Select( quiz => quiz.QuestionId ).Contains( quizId ) && 
                                                   question.IsActive )
                               .Select( q => new QuestionDef {
                                   Text = q.Text,
                                   Type = q.Type } )
                               .ToHashSet();
        }

        //Get Questions By Ids
        public HashSet<QuestionDef> GetQuestions( List<int> ids ) {
            return _quizContext.Questions
                               .Include( question => question.Answers )
                               .ThenInclude( answers => answers.Answer )
                               .Where( q => ids.Contains( q.QuestionId ) && q.IsActive )
                               .ToHashSet();
        }

        //Get Question By Id
        public QuestionDef GetQuestion( int id ) {
            return _quizContext.Questions
                               .Include( q => q.QuestionImage )
                               .Include( q => q.QuestionVideo )
                               .Include( question => question.Answers )
                               .ThenInclude( a => a.Answer )
                               .ThenInclude( ai => ai.AnswerImage )
                               .SingleOrDefault( q => id.Equals( q.QuestionId ) && q.IsActive );
        }

        //Get Correct Answer For A Question By Question Id
        public HashSet<AnswerDef> GetCorrectAnswers( int questionId ) {
            return _quizContext.Questions                               
                               .Include( question => question.Answers
                                                             .Where( answers => answers.Answer.IsActive && answers.IsCorrectAnswer ) )
                               .ThenInclude( a => a.Answer )
                               .Single( q => questionId.Equals( q.QuestionId ) && q.IsActive ).Answers
                               .Select( a => a.Answer )
                               .ToHashSet();
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
        // Add A New Question
        public void AddAnswer( int questionId, AnswerDef answer, bool isCorrectAnswer = false ) {
            //Check that the question exists by question id...  bail if it doesn't exist
            //answer.Question = build a new JoinQuestionAnswer object using quetionId and isCorrectAnswer
            //example here ... http://www.thereformedprogrammer.net/updating-many-to-many-relationships-in-entity-framework-core/
            //_quizContext.Add( answer );
            //_quizContext.SaveChanges();
        }

        /* TODO
        void RemoveAnswer(int id);
        void UpdateAnswer(AnswerDef answer);
         * */

        //List Answers To A Question By QuestionId
        public HashSet<AnswerDef> ListAnswersForQuestion( int questionId ) {

            // Try This.... might not work out...
            //  for this to be correct I think I have To Go The Other Way,  Get Question First.

            return _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Select( a => new AnswerDef {
                                   Text = a.Text,
                                   AnswerImage = a.AnswerImage,
                                   AnswerVideo = a.AnswerVideo
                               } )
                               .Where( a => a.Question.Select( q => q.QuestionId ).Contains( questionId ) && a.IsActive )
                               .ToHashSet();
        }

        //Get All Answers To A Set Of Questions By QuestionId
        public HashSet<AnswerDef> GetAnswersForQuestions( List<int> questionIds ) {
            return _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Where( a => a.Question.Any( q => questionIds.Contains(q.QuestionId) ) && a.IsActive )
                               .ToHashSet();
        }

        //Get All Answers To A Question By QuestionId
        public HashSet<AnswerDef> GetAnswersForQuestion( int questionId ) {
            return _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Where( a => a.Question.Any( q => questionId.Equals( q.QuestionId ) ) && a.IsActive )
                               .ToHashSet();
        }

        //Get Single/Several Answers By Ids
        public HashSet<AnswerDef> GetAnswers( List<int> ids ) {
            return _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Where( a => ids.Contains( a.AnswerId ) && a.IsActive )
                               .ToHashSet();
        }

        //Get Answer By Id
        public AnswerDef GetAnswer( int id ) {
            return _quizContext.Answers
                               .Include( a => a.AnswerImage )
                               .Include( a => a.AnswerVideo )
                               .Single( a => id.Equals( a.AnswerId ) && a.IsActive );
        }

        //Check Correctness of an Answer By AnswerId
        public bool CheckAnswer( int questionId, int answerId ) {
            return _quizContext.Answers
                               .SingleOrDefault( a => answerId.Equals( a.AnswerId ) && a.IsActive )
                               .Question.Single( q => questionId.Equals( q.QuestionId ) )
                               .IsCorrectAnswer ? true : false;
        }

        //List Correct Questions/Answers For A Quiz
        public HashSet<QuestionDef> AnswerSheetForQuiz( int quizId ) {
            var theseQuestion = _quizContext.Quizes
                                            .Include( q => q.Questions )
                                             .ThenInclude( questions => questions.Question )
                                             .ThenInclude( question => question.Answers.Where( a => a.IsCorrectAnswer ) )
                                             .ThenInclude( answers => answers.Answer )
                                             .Single( (q => quizId.Equals( q.QuizId ) && q.IsActive) ).Questions;

            return theseQuestion.Select( q => q.Question )
                                .ToHashSet();
        }
        #endregion 

        /*  
         * 
         * 
         * TODO: Get quiz working thru API, then work in admin stuff to create update delete quizes/question/answers...
         *       -- build another repo and apiController for this
         *                
         * TODO: After quiz stuff is done could build user controls.
         *       -- build another repo and apiController for this
         * 
         */
    }
    #endregion
}
