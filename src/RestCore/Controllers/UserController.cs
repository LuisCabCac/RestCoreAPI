using System;
using Microsoft.AspNetCore.Mvc;
using RestCore.Models.Logger;
using RestCore.Models.Users;

namespace RestCore.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager UserManager = null;

        public UserController(IUserManager userManager)
        {
            this.UserManager = userManager;
        }

        // GET: api/user 
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(UserManager.GetAll());

        }

        // GET api/user/5 
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id < 0)
                    return BadRequest();

                if (!UserManager.Exist(id))
                    return NotFound();
                else
                    return Ok(UserManager.GetFirstId(id));
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

                int id = UserManager.Add(value.Name, value.BirthDate);
                return Ok(UserManager.GetLastId(id));
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

                if (!UserManager.Exist(value.Id))
                    return NotFound();
                else
                {
                    UserManager.Update(value);
                    //Only return the first record with this Id
                    return Ok(UserManager.GetFirstId(value.Id));
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

                if (!UserManager.Exist(id))
                    return NotFound();
                else
                {
                    UserManager.Delete(id);
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
