using MIQuizAPI.Database.Context;
using MIQuizAPI.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Repository {
    public class QuizRepository : IQuizRepository {
        private readonly MIQuizContext _quizContext;

        public QuizRepository( MIQuizContext quizContext ) {
            _quizContext = quizContext;
        }

        #region Users
        // List All Users
        public HashSet<User> ListUsers() {
            return _quizContext.Users
                               .Select( u => new User {
                                   UserName = u.UserName,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   IsActive = u.IsActive
                               } )
                               .ToHashSet();
        }

        // Get Users
        public HashSet<User> GetUsers() {
            return _quizContext.Users
                               .Where( u => u.IsActive )
                               .ToHashSet();
        }

        // Get User(s) By Id(s)
        public HashSet<User> GetUsers( List<int> ids) {
            return _quizContext.Users
                               .Where( u => ids.Contains(u.Id) && u.IsActive )
                               .ToHashSet();
        }

        // Get User By Id
        public User GetUser( int id ) {
            return _quizContext.Users
                               .SingleOrDefault( u => id.Equals( u.Id ) && u.IsActive );
        }

        // Get User By Id -- synonym for GetQuizesByOwnerIds
        public HashSet<QuizDef> GetAdminUserQuizes( int id ) {
            return _quizContext.Quizes
                               .Where( q => id.Equals( q.OwnerId ) && q.IsActive )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }
        #endregion

        #region Quizes
        // List All Quizes
        public HashSet<QuizDef> ListQuizes() {
            return _quizContext.Quizes
                               .Select( q => new QuizDef {
                                   Name = q.Name,
                                   Description = q.Description,
                                   GradingCriteria = q.GradingCriteria,
                                   IsActive = q.IsActive,
                                   Instructions = q.Instructions,
                                   Order = q.Order
                               } )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        // Get All Quizes
        public HashSet<QuizDef> GetQuizes() {
            return _quizContext.Quizes
                               .Where( q => q.IsActive )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        //Get Quiz(es) By Id(s)
        public HashSet<QuizDef> GetQuizes( List<int> ids ) {
            return _quizContext.Quizes
                               .Where( q => ids.Contains( q.Id ) && q.IsActive )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        //Get Single Quiz By Id
        public QuizDef GetQuiz( int id ) {
            return _quizContext.Quizes
                               .SingleOrDefault( q => id.Equals( q.Id ) && q.IsActive );
        }

        //Get Quizes By OwnerId
        public HashSet<QuizDef> GetQuizesByOwnerIds( List<int> ids ) {
            return _quizContext.Quizes
                               .Where( q => ids.Contains( q.OwnerId ) && q.IsActive )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }
        #endregion

        #region Questions
        //List Questions
        public HashSet<QuestionDef> ListQuestions( ) {
            return _quizContext.Questions
                               .Select( q => new QuestionDef {
                                   Text = q.Text,
                                   Type = q.Type,
                                   IsActive = q.IsActive,
                                   Order = q.Order } )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        //List Questions On A Quiz By QuizId
        public HashSet<QuestionDef> ListQuestionsForQuiz( int id ) {
            return _quizContext.Quizes.Where(q=> id.Equals(q.Id)).Single()
                                      .Questions.Select( q => new QuestionDef {
                                            Text = q.Text,
                                            Type = q.Type,
                                            IsActive = q.IsActive,
                                            Order = q.Order } )
                                      .OrderBy( a => a.Order )
                                      .ToHashSet();
        }


        //Get Questions By Ids
        public HashSet<QuestionDef> GetQuestions( List<int> ids ) {
            return _quizContext.Questions
                               .Where( q => ids.Contains( q.Id ) && q.IsActive )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        //Get Question By Id
        public QuestionDef GetQuestion( int id ) {
            return _quizContext.Questions
                               .SingleOrDefault( q => id.Equals(q.Id) && q.IsActive );
        }
        #endregion

        #region Answers
        //List Answers To A Question By QuestionId
        public HashSet<AnswerDef> ListAnswersForQuestion( int id ) {
            return _quizContext.Questions
                               .Where( q => id.Equals(q.Id) ).Single()
                               .Answers.Select( a => new AnswerDef {
                                   Text = a.Text,
                                   IsActive = a.IsActive,
                                   Order = a.Order } )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        //List All Answers To A Set Of Questions By QuestionIds
        public HashSet<AnswerDef> ListAnswersForQuestions( List<int> ids ) {
            return _quizContext.Answers
                               .Where( a => ids.Contains( a.QuestionId ) && a.IsActive )
                               .Select( a => new AnswerDef {
                                   Text = a.Text,
                                   IsActive = a.IsActive,
                                   Order = a.Order } )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        //Get All Answers To A Set Of Questions By QuestionId
        public HashSet<AnswerDef> GetAnswersForQuestions( List<int> ids ) {
            return _quizContext.Answers
                               .Where( a => ids.Contains( a.QuestionId ) && a.IsActive )
                               .ToHashSet();
        }

        //Get All Answers To A Question By QuestionId
        public HashSet<AnswerDef> GetAnswersForQuestion( int id ) {
            return _quizContext.Answers
                               .Where( a => id.Equals( a.Id ) && a.IsActive )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        //Get Single/Several Answers By Ids
        public HashSet<AnswerDef> GetAnswers( List<int> ids ) {
            return _quizContext.Answers
                               .Where( a => ids.Contains( a.Id ) && a.IsActive )
                               .OrderBy( a => a.Order )
                               .ToHashSet();
        }

        //Get Answer By Id
        public AnswerDef GetAnswer( int id ) {
            return _quizContext.Answers.Single( a => id.Equals( a.Id ) && a.IsActive );
        }

        //Get Correct Answers By QuestionId
        public HashSet<AnswerDef> GetCorrectAnswersForQuestion( int id ) {
            return _quizContext.Answers
                               .Where( a => id.Equals( a.Id ) && a.IsActive && a.IsCorrectAnswer )
                               .ToHashSet();
        }

        //Check Correctness of an Answer By AnswerId
        public bool CheckAnswer( int id ) {
            return _quizContext.Answers.Single( a => id.Equals( a.Id ) && a.IsActive ).IsCorrectAnswer;
        }

        //List Correct Questions/Answers For A Quiz
        public HashSet<QuestionDef> AnswerSheetForQuiz( int id ) {
            return _quizContext.Quizes.Single( quiz => id.Equals( quiz.Id ) && quiz.IsActive )
                                      .Questions.Select( q => new QuestionDef {
                                          Id = q.Id,
                                          Text = q.Text,
                                          Order = q.Order,
                                          Answers = q.Answers.Where( a => a.IsCorrectAnswer)
                                                             .Select( a => new AnswerDef {
                                                                Id = a.Id,
                                                                Text = a.Text,
                                                                Order = a.Order } )
                                                             .ToHashSet() } )
                                      .OrderBy( a => a.Order )
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
}
