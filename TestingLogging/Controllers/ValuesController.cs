using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestingLogging.Services;

namespace TestingLogging.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            this._courseService = courseService;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_courseService.Courses);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_courseService.Courses.FirstOrDefault(c => c.Id == id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Course course)
        {
            _courseService.Courses.Add(course);

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
