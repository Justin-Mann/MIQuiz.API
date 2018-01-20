using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MIQuizAPI.Database.Context;
using MIQuizAPI.Database.Models;
using MIQuizAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MIQuizAPI.Controllers {
    [Route( "api/quizes" )]
    public class QuizesController : Controller {
        private readonly IQuizRepository _quizRepo;

        public QuizesController( IQuizRepository quizRepo ) {
            _quizRepo = quizRepo;
        }

        // GET: api/quizes
        [HttpGet]
        public IEnumerable<QuizDef> Get() {
            return _quizRepo.GetQuizes();
        }

        // GET api/quizes/5
        [HttpGet( "{id}" , Name = "GetQuizes")]
        public IActionResult Get( int id ) {
            var it = _quizRepo.GetQuiz( id );
            if( it == null ) { return NotFound(); }
            return new ObjectResult(it);
        }

        // POST api/quizes
        [HttpPost]
        public IActionResult Post( [FromBody] QuizDef quiz ) {
            if( quiz == null ) {
                return BadRequest();
            }
            _quizRepo.AddQuiz( quiz );
            return CreatedAtRoute( "GetContacts", new { Controller = "Contacts", id = quiz.QuizId }, quiz );
        }

        // PUT api/quizes/5
        [HttpPut( "{id}" )]
        public IActionResult Put( int id, [FromBody] QuizDef quiz ) {
            if( quiz == null ) {
                return BadRequest();
            }
            var contactObj = _quizRepo.GetQuiz( id );
            if( contactObj == null ) {
                return NotFound();
            }
//            _quizRepo.UpdateQuiz( quiz );
            //return CreatedAtRoute( "GetContacts", new { Controller = "Contacts", id = item.QuizId }, item );
            return new NoContentResult();
        }

        // DELETE api/quizes/5
        [HttpDelete( "{id}" )]
        public void Delete( int id ) {
//            _quizRepo.RemoveQuiz( id );
        }
    }
}
