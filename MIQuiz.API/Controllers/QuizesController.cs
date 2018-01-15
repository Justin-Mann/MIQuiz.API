﻿using System;
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
        [HttpGet( "{id}" )]
        public QuizDef Get( int id ) {
            return _quizRepo.GetQuiz( id );
        }

        // POST api/quizes
        [HttpPost]
        public void Post( [FromBody]string value ) {
        }

        // PUT api/quizes/5
        [HttpPut( "{id}" )]
        public void Put( int id, [FromBody]string value ) {
        }

        // DELETE api/quizes/5
        [HttpDelete( "{id}" )]
        public void Delete( int id ) {
        }
    }
}
