using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dojodachi.Models;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Dojodachis myDojodachi = new Dojodachis();
            myDojodachi.Fullness = 20;
            myDojodachi.Happiness = 20;
            myDojodachi.Energy = 50;
            myDojodachi.Meals = 3;

            return View(myDojodachi);
        }

        public IActionRestul Feed(){
            if (myDojodachi.Meals > 0){
                
            }
        }
    }
}
