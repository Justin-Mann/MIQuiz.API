using MIQuizAPI.Database.Context;
using MIQuizAPI.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly MIQuizContext _quizContext;

        public QuizRepository(MIQuizContext quizContext)
        {
            _quizContext = quizContext;
        }

        // Get All Quizes
        public HashSet<QuizDef> GetAllQuizes()
        {
            return _quizContext.Quizes.Where(q => q.IsActive).ToHashSet();
        }

        //Get Several Quizes By OwnerId
        public HashSet<QuizDef> GetQuiz(List<int> ids)
        {
            return _quizContext.Quizes.Where(q => ids.Contains(q.OwnerId) && q.IsActive).ToHashSet();
        }
        
        //Get Several/Single Quiz(es) By Id(s)
        public HashSet<QuizDef> GetQuizesById(List<int> ids)
        {
            return _quizContext.Quizes.Where(q => ids.Contains(q.Id) && q.IsActive).ToHashSet();
        }

        /* Get All High Scores On All Quizes
         * Get High Scores On A Quiz
         * Get A List Of Saved Quizes By User ID
         * Get A List Of Saved Quizes By Quiz ID
         * Get Average Score based on some parameters
         * 
         * Get All Questions On A Quiz By QuizId
         * Get Several Questions By Ids
         * Get Single Question By Id
         * Get Correct Answer By QuestionId
         * 
         * Get List Of All Answers To A Question By QuestionId
         * Get All Answers To A Question By QuestionId
         * Get Answer By AnswerId
         * Check Correctness of an Answer By AnswerId
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
