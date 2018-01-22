using System.Collections.Generic;
using MIQuizAPI.Database.Models;
using System.Threading.Tasks;

namespace MIQuizAPI.Repository {
    #region Users Repo Interface
    public interface IUserRepository {
        // Add A New User
        Task Add( User user );

        // Remove A User
        Task Remove( int id );

        // Update A User
        Task Update( User user);

        // List Users
        Task<IEnumerable<User>> ListAll();

        // Get Users
        Task<IEnumerable<User>> GetAll();

        // Get User(s) By Id(s)
        Task<IEnumerable<User>> GetByIds( List<int> ids );

        // Get User By Id
        Task<User> GetById( int id );

        // Get User By Id -- synonym for GetQuizesByOwnerIds
        Task<User> GetByUserId( int id );
    }
    #endregion

    #region Quizes Repo Interface
    public interface IQuizRepository {
        // Add A New Quiz
        Task Add( QuizDef quiz );

        // Remove A Quiz
        Task Remove( int id );

        // Update A Quiz
        Task Update( QuizDef quiz );

        // List All Quizes
        Task<IEnumerable<QuizDef>> ListAll();

        // Get All Quizes
        Task<IEnumerable<QuizDef>> GetAll();

        //Get Quiz(es) By Id(s)
        Task<IEnumerable<QuizDef>> GetByIds( List<int> ids );

        //Get Single Quiz By Id
        Task<QuizDef> GetById( int id );

        //Get Quizes By OwnerId
        Task<IEnumerable<QuizDef>> GetByOwnerIds( List<int> ids );
    }
    #endregion

    #region Questions Repo Interface
    public interface IQuestionRepository {
        // Add A New Question
        Task Add( QuestionDef question );

        // Remove A Question
        Task Remove( int id );

        // Update A Question
        Task Update( QuestionDef question );

        //List Questions
        Task<IEnumerable<QuestionDef>> ListAll();

        //Get All Questions
        Task<IEnumerable<QuestionDef>> GetAll();

        //List Questions On A Quiz By QuizId
        Task<IEnumerable<QuestionDef>> ListByQuizId( int id );

        //Get Questions By Ids
        Task<IEnumerable<QuestionDef>> GetByIds( List<int> ids );

        //Get Question By Id
        Task<QuestionDef> GetById( int id );

        Task<IEnumerable<AnswerDef>> GetCorrectAnswersByQuestionId( int questionId );
    }
    #endregion

    #region Answers Repo Interface
    public interface IAnswerRepository {
        // Add A New Answer
        Task Add( AnswerDef answer );

        // Remove A Answer
        Task Remove( int id );

        // Update A Answer
        Task Update( AnswerDef answer );

        //List Answers To A Question By QuestionId
        Task<IEnumerable<AnswerDef>> ListByQuestionId( int questionId );

        //Get All Answers To A Set Of Questions By QuestionId
        Task<IEnumerable<AnswerDef>> GetByQuestionIds( List<int> questionIds );

        //Get All Answers To A Question By QuestionId
        Task<IEnumerable<AnswerDef>> GetByQuestionId( int questionId );

        //Get Correct Answer By QuestionId
        Task<IEnumerable<AnswerDef>> GetCorrectAnswersByQuestionId( int id );

        //Get Single/Several Answers By Ids
        Task<IEnumerable<AnswerDef>> GetByIds( List<int> ids );

        //Get Answer By Id
        Task<AnswerDef> GetById( int id );

        //List Correct Questions/Answers For A Quiz
        Task<IEnumerable<QuestionDef>> GetAnswersByQuizId( int id );
    }
    #endregion

    #region Images Repo Interface
    public interface IImageRepository {
        // Add A New Image
        Task Add( Image image );

        // Remove A Image
        Task Remove( int id );

        // Update A Image
        Task Update( Image image );

        //Get Images By Ids
        Task<IEnumerable<Image>> GetByIds( List<int> ids );

        //Get Image By Id
        Task<Image> GetById( int id );
    }
    #endregion

    #region Videos Repo Interface
    public interface IVideoRepository {
        // Add A New Video
        Task Add( Video video );

        // Remove A Video
        Task Remove( int id );

        // Update A Video
        Task Update( Video video );

        //Get Videos By Ids
        Task<IEnumerable<Video>> GetByIds( List<int> ids );

        //Get Video By Id
        Task<Video> GetById( int id );
    }
    #endregion
}
