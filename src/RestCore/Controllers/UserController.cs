using System;
using Microsoft.AspNetCore.Mvc;
using RestCore.Models.Logger;
using RestCore.Models.Users;
using Microsoft.Extensions.Caching.Memory;

namespace RestCore.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private UserManager userManager = null;

        public UserController(IMemoryCache memoryCache)
        {
            userManager = new UserManager(memoryCache);
        }

        // GET: api/user 
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(userManager.GetAll());

        }

        // GET api/user/5 
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id < 0)
                    return BadRequest();

                if (!userManager.Exist(id))
                    return NotFound();
                else
                    return Ok(userManager.GetFirstId(id));
            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.ControlException(ex);
                if (ExceptionManager.Instance.IsThrowable(ex))
                    throw ex;
                else
                    return NoContent();
            }
        }
        // POST api/user 
        [HttpPost]
        public IActionResult Create([FromBody]UserModel value)
        {
            try
            {
                if (value == null || string.IsNullOrEmpty(value.Name) || string.IsNullOrEmpty(value.BirthDate))
                    return BadRequest();

                int id = userManager.Add(value.Name, value.BirthDate);
                return Ok(userManager.GetLastId(id));
            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.ControlException(ex);
                if (ExceptionManager.Instance.IsThrowable(ex))
                    throw ex;
                else
                    return NoContent();
            }
        }

        // PUT api/user
        [HttpPut]
        public IActionResult Update([FromBody]UserModel value)
        {
            try
            {
                if (value == null || value.Id==0)
                    return BadRequest();

                if (!userManager.Exist(value.Id))
                    return NotFound();
                else
                {
                    userManager.Update(value);
                    //Only return the first record with this Id
                    return Ok(userManager.GetFirstId(value.Id));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.ControlException(ex);
                if (ExceptionManager.Instance.IsThrowable(ex))
                    throw ex;
                else
                    return NoContent();
            }
        }

        // DELETE api/user/5 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 0)
                    return BadRequest();

                if (!userManager.Exist(id))
                    return NotFound();
                else
                {
                    userManager.Delete(id);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.ControlException(ex);
                if (ExceptionManager.Instance.IsThrowable(ex))
                    throw ex;
                else
                    return NoContent();
            }

        }

    }
}
