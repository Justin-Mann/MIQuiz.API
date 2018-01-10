using MIQuizAPI.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Repository
{
    interface IQuizRepository
    {
        #region Users
        HashSet<User> ListUsers();

        // Get Users
        HashSet<User> GetUsers();

        // Get User(s) By Id(s)
        HashSet<User> GetUsers( List<int> ids );

        // Get User By Id
        User GetUser( int id );

        // Get User By Id -- synonym for GetQuizesByOwnerIds
        HashSet<QuizDef> GetAdminUserQuizes( int id );
        #endregion

        #region Quizes
        // List All Quizes
        HashSet<QuizDef> ListQuizes();

        // Get All Quizes
        HashSet<QuizDef> GetQuizes();

        //Get Quiz(es) By Id(s)
        HashSet<QuizDef> GetQuizes( List<int> ids );

        //Get Single Quiz By Id
        QuizDef GetQuiz( int id );

        //Get Quizes By OwnerId
        HashSet<QuizDef> GetQuizesByOwnerIds( List<int> ids );
        #endregion

        #region Questions
        //List Questions
        HashSet<QuestionDef> ListQuestions();

        //List Questions On A Quiz By QuizId
        HashSet<QuestionDef> ListQuestionsForQuiz( int id );

        //Get Questions By Ids
        HashSet<QuestionDef> GetQuestions( List<int> ids );

        //Get Question By Id
        QuestionDef GetQuestion( int id );
        #endregion

        #region Answers
        //List Answers To A Question By QuestionId
        HashSet<AnswerDef> ListAnswersForQuestion( int id );

        //List All Answers To A Set Of Questions By QuestionIds
        HashSet<AnswerDef> ListAnswersForQuestions( List<int> ids );

        //Get All Answers To A Set Of Questions By QuestionId
        HashSet<AnswerDef> GetAnswersForQuestions( List<int> ids );

        //Get All Answers To A Question By QuestionId
        HashSet<AnswerDef> GetAnswersForQuestion( int id );

        //Get Single/Several Answers By Ids
        HashSet<AnswerDef> GetAnswers( List<int> ids );

        //Get Answer By Id
        AnswerDef GetAnswer( int id );

        //Get Correct Answer By QuestionId
        AnswerDef GetCorrectAnswerForQuestion( int id );

        //Check Correctness of an Answer By AnswerId
        bool CheckAnswer( int id );

        //List Correct Questions/Answers For A Quiz
        HashSet<QuestionDef> AnswerSheetForQuiz( int id );
        #endregion
    }
}
