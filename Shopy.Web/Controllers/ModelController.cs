﻿using Microsoft.AspNetCore.Mvc;
using Shopy.Web.Models;
using Shopy.Web.Dtos;
using Shopy.Web.Shared;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebApi.Controllers;

    [Route("api/models")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        // GET: api/<ModelController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ModelController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ModelController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ModelController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ModelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

