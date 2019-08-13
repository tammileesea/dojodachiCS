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
    // public static class SessionExtensions {
    //     public static void SetObjestAsJson(this ISession session, string key, object value){
    //         session.SetString(key, JsonConvert.SerializeObject(value));
    //     }

    //     public static T GetObjectFromJson<T>(this ISession session, string key){
    //         string value = session.GetString(key);
    //         return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    //     }
    // }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Fullness") == null){
                HttpContext.Session.SetInt32("Fullness", 20);
            }
            if (HttpContext.Session.GetInt32("Happiness") == null){
                HttpContext.Session.SetInt32("Happiness", 20);
            }
            if (HttpContext.Session.GetInt32("Energy") == null){
                HttpContext.Session.SetInt32("Energy", 50);
            }
            if (HttpContext.Session.GetInt32("Meals") == null){
                HttpContext.Session.SetInt32("Meals", 3);
            }
            // TempData["Message"] = "Let's play!!!!!!!!";
            if ((Convert.ToInt32(HttpContext.Session.GetInt32("Fullness")) >= 100) && (Convert.ToInt32(HttpContext.Session.GetInt32("Happiness")) >= 100) && (Convert.ToInt32(HttpContext.Session.GetInt32("Energy")) >= 100)){
                TempData["Message"] = "YOU WON!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
                ViewBag.Partial = "WinLosePartial"; 
            } else if ((Convert.ToInt32(HttpContext.Session.GetInt32("Fullness")) <= 0) || (Convert.ToInt32(HttpContext.Session.GetInt32("Happiness")) <= 0)){
                TempData["Message"] = "YOU LOOOOOOSSSSSEEEEEEEEEEE";
                ViewBag.Partial = "WinLosePartial";
            } else {
                ViewBag.Partial = "ButtonPartial"; 
            }
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Message = TempData["Message"];
            return View();
        }

        public IActionResult Feed(){
            int? dachiFullness = HttpContext.Session.GetInt32("Fullness");
            int? dachiMeals = HttpContext.Session.GetInt32("Meals");
            Random rand = new Random();
            if (dachiMeals <= 0){
                TempData["Message"] = "Can't eat no mo' bc u aint got any meals left dumdum";
                return RedirectToAction("Index");
            } else {
                int fail = rand.Next(1, 5);
                if (fail == 1){
                    dachiMeals--;
                    TempData["Message"] = "Dojodachi don't wanna eat rn but u still lost a meal like a dumdum :'((((( try again!";
                    HttpContext.Session.SetInt32("Meals", Convert.ToInt32(dachiMeals));
                    return RedirectToAction("Index");
                } else {
                    dachiMeals--;
                    int fullPoints = rand.Next(5,11);
                    dachiFullness += fullPoints;
                    TempData["Message"] = $"You gained {fullPoints} fullness points and have {dachiMeals} meals left";
                    HttpContext.Session.SetInt32("Fullness", Convert.ToInt32(dachiFullness));
                    HttpContext.Session.SetInt32("Meals", Convert.ToInt32(dachiMeals));
                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult Play(){
            Random rand = new Random();
            int? dachiEnergy = HttpContext.Session.GetInt32("Energy");
            int? dachiHap = HttpContext.Session.GetInt32("Happiness");
            if (dachiEnergy == 0){
                TempData["Message"] = "You have no energy :'((((( u can't play";
                return RedirectToAction("Index");
            } else {
                int fail = rand.Next(1, 5);
                if (fail == 1){
                    dachiEnergy -= 5;
                    TempData["Message"] = "You lost 5 energy points bc Dojodachi don't wanna play rn :'((((( try again!";
                    HttpContext.Session.SetInt32("Energy", Convert.ToInt32(dachiEnergy));
                    return RedirectToAction("Index");
                } else {
                    dachiEnergy -= 5;
                    int addHap = rand.Next(5,11);
                    dachiHap += addHap;
                    TempData["Message"] = $"You've lost 5 energy points and gained {addHap} happiness points";
                    HttpContext.Session.SetInt32("Energy", Convert.ToInt32(dachiEnergy));
                    HttpContext.Session.SetInt32("Happiness", Convert.ToInt32(dachiHap));
                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult Work(){
            int? dachiEnergy = HttpContext.Session.GetInt32("Energy");
            int? dachiMeals = HttpContext.Session.GetInt32("Meals");
            dachiEnergy -= 5;
            Random rand = new Random();
            int newMeals = rand.Next(1,4);
            dachiMeals += newMeals;
            TempData["Message"] = $"You've lost 5 energy points and gained {newMeals} meals";
            HttpContext.Session.SetInt32("Energy", Convert.ToInt32(dachiEnergy));
            HttpContext.Session.SetInt32("Meals", Convert.ToInt32(dachiMeals));
            return RedirectToAction("Index");
        }

        public IActionResult Sleep(){
            int? dachiEnergy = HttpContext.Session.GetInt32("Energy");
            int? dachiHap = HttpContext.Session.GetInt32("Happiness");
            int? dachiFullness = HttpContext.Session.GetInt32("Fullness");
            dachiEnergy += 15;
            dachiHap -= 5;
            dachiFullness -= 5;
            TempData["Message"] = "You've gained 15 energy points and lost 5 happiness and fullness points ://";
            HttpContext.Session.SetInt32("Energy", Convert.ToInt32(dachiEnergy));
            HttpContext.Session.SetInt32("Happiness", Convert.ToInt32(dachiHap));
            HttpContext.Session.SetInt32("Fullness", Convert.ToInt32(dachiFullness));
            return RedirectToAction("Index");
        }

        public IActionResult Reset(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
