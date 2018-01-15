using System.Collections.Generic;
using MIQuizAPI.Database.Models;

namespace MIQuizAPI.Repository {
    #region Users Repo Interface
    public interface IUserRepository {
        // List Users
        HashSet<User> ListUsers();

        //// Get Users
        HashSet<User> GetUsers();

        //// Get User(s) By Id(s)
        HashSet<User> GetUsers( List<int> ids );

        //// Get User By Id
        User GetUser( int id );

        //// Get User By Id -- synonym for GetQuizesByOwnerIds
        User GetAdminUserQuizes( int id );
    }
    #endregion

    #region Quizes Repo Interface
    public interface IQuizRepository {
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
    }
    #endregion

    #region Questions Repo Interface
    public interface IQuestionRepository {
        //List Questions
        HashSet<QuestionDef> ListQuestions();

        //List Questions On A Quiz By QuizId
        HashSet<QuestionDef> ListQuestionsForQuiz( int id );

        //Get Questions By Ids
        HashSet<QuestionDef> GetQuestions( List<int> ids );

        //Get Question By Id
        QuestionDef GetQuestion( int id );

        HashSet<AnswerDef> GetCorrectAnswers( int questionId );
    }
    #endregion

    #region Answers Repo Interface
    public interface IAnswerRepository {
        //List Answers To A Question By QuestionId
        HashSet<AnswerDef> ListAnswersForQuestion( int id );

        //List All Answers To A Set Of Questions By QuestionIds
        //HashSet<AnswerDef> ListAnswersForQuestions( List<int> ids );

        //Get All Answers To A Set Of Questions By QuestionId
        HashSet<AnswerDef> GetAnswersForQuestions( List<int> ids );

        //Get All Answers To A Question By QuestionId
        HashSet<AnswerDef> GetAnswersForQuestion( int id );

        //Get Single/Several Answers By Ids
        HashSet<AnswerDef> GetAnswers( List<int> ids );

        //Get Answer By Id
        AnswerDef GetAnswer( int id );

        //Get Correct Answer By QuestionId
        //HashSet<AnswerDef> GetCorrectAnswersForQuestion( int id );

        //Check Correctness of an Answer By AnswerId
        bool CheckAnswer( int questionId, int answerId );

        //List Correct Questions/Answers For A Quiz
        HashSet<QuestionDef> AnswerSheetForQuiz( int id );
    }
    #endregion
}
