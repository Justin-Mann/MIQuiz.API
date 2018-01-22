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
    [Route( "api/questions" )]
    public class QuestionsController : Controller {
        private readonly IQuestionRepository _questionRepo;

        public QuestionsController( IQuestionRepository questionRepo ) {
            _questionRepo = questionRepo;
        }

        // GET: api/questions
        [HttpGet( Name = "GetQuestions" )]
        public async Task<IActionResult> GetAll() {
            var questions = await _questionRepo.GetAll();
            if( questions == null ) { return NotFound(); }
            return Ok( questions );
        }

        // GET api/questions/5
        [HttpGet( "{id}", Name = "GetQuestion" ) )]
        public async Task<IActionResult> Get( int id ) {
            var question = await _questionRepo.GetById( id );
            if( question == null ) { return NotFound(); }
            return Ok( question );
        }


        // TODO :: Add more get methods to retrieve by quiz id, get answers, get correct answers


        // POST api/questions
        [HttpPost]
        public async Task<IActionResult> Create( [FromBody] QuestionDef Question ) {
            if( Question == null ) {
                return BadRequest();
            }
            await _questionRepo.Add( Question );
            return CreatedAtRoute( "GetQuestion", new { Controller = "Questions", id = Question.QuestionId }, Question );
        }

        // PUT api/questions/5
        [HttpPut( "{id}" )]
        public async Task<IActionResult> Put( int id, [FromBody] QuestionDef Question ) {
            if( Question == null ) {
                return NotFound();
            }
            Question.QuestionId = id;
            await _questionRepo.Update( Question );
            return RedirectToRoute( "GetQuestion", new { Controller = "Questions", id = Question.QuestionId } );
        }

        // DELETE api/questions/5
        [HttpDelete( "{id}" )]
        public async Task<IActionResult> Delete( int id ) {
            var target = _questionRepo.GetById( id );
            if( target == null ) {
                return NotFound();
            }
            await _questionRepo.Remove( id );
            return RedirectToRoute( "GetQuizes", new { Controller = "Quizes" } );
        }
    }
}


// TODO :: JWT Authentication  --  http://www.blinkingcaret.com/2017/09/06/secure-web-api-in-asp-net-core/  -OR-  https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login