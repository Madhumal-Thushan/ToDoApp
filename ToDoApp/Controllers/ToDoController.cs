﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        public string Index()
        {
            return "test"; 
        }
    }
}
