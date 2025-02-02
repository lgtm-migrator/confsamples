using System;
using System.Collections.Generic;
using System.Linq;

﻿using Microsoft.AspNetCore.Mvc;

using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ITodoRepository _repository;

        public TestController(ITodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public string Init()
        {
            string ret = "OK";
            
            try
            {
                _repository.Init();
            }
            catch (Exception e) 
            {
                ret = e.Message;
            }                          
            
            return ret;
        }
        
        [HttpGet("{id:int}")]
        public string TestConnection(int id)
        {
            string ret = "OK";
            
            try
            {
                var x = _repository.AllItems;
            }
            catch (Exception e) 
            {
                ret = e.Message;
            }                          
            
            return ret;
        }
    }
}
