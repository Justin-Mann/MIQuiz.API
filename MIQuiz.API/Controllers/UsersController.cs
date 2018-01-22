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
    [Route( "api/users" )]
    public class UsersController : Controller {
        private readonly IUserRepository _userRepo;

        public UsersController( IUserRepository userRepo ) {
            _userRepo = userRepo;
        }

        // GET: api/users
        [HttpGet( Name = "GetUsers" )]
        public async Task<IActionResult> GetAll() {
            var users = await _userRepo.GetAll();
            return Ok( users );
        }

        // GET api/users/5
        [HttpGet( "{id}", Name = "GetUser" )]
        public async Task<IActionResult> Get( int id ) {
            var user = await _userRepo.GetById( id );
            return Ok( user );
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Create( [FromBody] User User ) {
            if( User == null ) {
                return BadRequest();
            }
            await _userRepo.Add( User );
            return CreatedAtRoute( "GetUser", new { Controller = "Users", id = User.UserId }, User );
        }

        // PUT api/users/5
        [HttpPut( "{id}" )]
        public async Task<IActionResult> Put( int id, [FromBody] User User ) {
            if( User == null ) {
                return BadRequest();
            }
            await _userRepo.Update( User );
            return RedirectToRoute( "GetUser", new { Controller = "Users", id = User.UserId } );
        }

        // DELETE api/users/5
        [HttpDelete( "{id}" )]
        public async Task<IActionResult> Delete( int id ) {
            var removeTarget = _userRepo.GetById( id );
            if( removeTarget == null ) {
                return NotFound();
            }
            await _userRepo.Remove( id );
            return RedirectToRoute( "GetUsers", new { Controller = "Users" } );
        }
    }
}
