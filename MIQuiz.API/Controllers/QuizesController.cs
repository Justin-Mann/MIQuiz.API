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
        [HttpGet( Name = "GetQuizes" )]
        public async Task<IActionResult> GetAll() {
            var quizes = await _quizRepo.GetAll();
            return Ok( quizes );
        }

        // GET api/quizes/5
        [HttpGet( "{id}", Name = "GetQuiz" )]
        public async Task<IActionResult> Get( int id ) {
            var quiz = await _quizRepo.GetById( id );
            if( quiz == null ) { return NotFound(); }
            return Ok( quiz );
        }

        [HttpPost]
        public async Task<IActionResult> Create( [FromBody] QuizDef Quiz ) {
            if( Quiz == null ) {
                return BadRequest();
            }
            await _quizRepo.Add( Quiz );
            return CreatedAtRoute( "GetQuiz", new { Controller = "Quizes", id = Quiz.QuizId }, Quiz );
        }

        // PUT api/quizes/5
        [HttpPut( "{id}" )]
        public async Task<IActionResult> Put( int id, [FromBody] QuizDef Quiz ) {
            if( Quiz == null ) {
                return NotFound();
            }
            Quiz.QuizId = id;
            await _quizRepo.Update( Quiz );
            return RedirectToRoute( "GetQuiz", new { Controller = "Quizes", id = Quiz.QuizId } );
        }

        // DELETE api/quizes/5
        [HttpDelete( "{id}" )]
        public async Task<IActionResult> Delete( int id ) {
            var updateTarget = _quizRepo.GetById( id );
            if( updateTarget == null ) {
                return NotFound();
            }
            await _quizRepo.Remove( id );
            return RedirectToRoute( "GetQuizes", new { Controller = "Quizes" } );
        }
    }
}
