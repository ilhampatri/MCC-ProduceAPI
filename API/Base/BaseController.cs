using API.Repository.Data;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{

    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{key}")]
        public ActionResult<Entity> Get(Key key)
        {
            var result = repository.Get(key);
            return result;
        }
        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            var hitung = repository.Get().Count();
            //return Ok( result);
            if (hitung != 0)
            {
                //employeeRepository.Get();
                //return Ok(new { status = HttpStatusCode.OK, result, massage = "successfully display data" });
                return Ok(result);
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, massage = "Empty data" });
            }
        }
        [HttpPost]
        public ActionResult<Entity> Insert(Entity entity)
        {
            var insert = repository.Insert(entity);
            return Ok(new { status = HttpStatusCode.OK, insert, massage = "successfully Insert data" });
        }
        [HttpDelete("{key}")]
        public ActionResult hapus(Key key)
        {
            try
            {
                var result = repository.Get();
                if (result != null)
                {
                    repository.Delete(key);
                    return Ok(new { status = HttpStatusCode.OK, result, message = "Successfully delete data" });
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "" });
                }
            }
            catch
            {
                throw;
            }
        }
        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                repository.Update(entity);
                return Ok(new { status = HttpStatusCode.OK, message = "data successfully updated" });
            }
            catch
            {
                throw;
            }
        }
    }
}



