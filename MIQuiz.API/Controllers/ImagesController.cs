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
    [Route( "api/images" )]
    public class ImagesController : Controller {
        private readonly IImageRepository _imageRepo;

        public ImagesController( IImageRepository imageRepo ) {
            _imageRepo = imageRepo;
        }

        //TODO : write image controller actions

        //// GET: api/users
        //[HttpGet]
        //public IEnumerable<User> Get() {
        //    return _userRepo.GetUsers();
        //}

        //// GET api/users/5
        //[HttpGet( "{id}" )]
        //public User Get( int id ) {
        //    return _userRepo.GetUser( id );
        //}

        //// POST api/users
        //[HttpPost]
        //public void Post( [FromBody]string value ) {
        //}

        //// PUT api/users/5
        //[HttpPut( "{id}" )]
        //public void Put( int id, [FromBody]string value ) {
        //}

        //// DELETE api/users/5
        //[HttpDelete( "{id}" )]
        //public void Delete( int id ) {
        //}
    }
}
