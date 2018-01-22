using MIQuizAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Controllers
{
    public class ListingsController
    {
        private readonly IUserRepository _userRepo;
        private readonly IQuizRepository _quizRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly IAnswerRepository _answerRepo;
        private readonly IImageRepository _imageRepo;
        private readonly IVideoRepository _videoRepo;

        public ListingsController( IUserRepository userRepo, IQuizRepository quizRepo, IQuestionRepository questionRepo, IAnswerRepository answerRepo, IImageRepository imageRepo, IVideoRepository videoRepo ) {
            _userRepo = userRepo;
            _quizRepo = quizRepo;
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
            _imageRepo = imageRepo;
            _videoRepo = videoRepo;
        }

        //TODO : write listings controller actions
    }
}
