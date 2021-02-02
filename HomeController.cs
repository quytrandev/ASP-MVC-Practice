using DatabaseIO;
using DBProvider;
using MyASPMVCCSHARP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyASPMVCCSHARP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //query
            DBIO db = new DBIO();
            //DB object
            //Users user = db.GetUsers("quytrandev","123456");

            List<Users> list = db.GetUsersList();
            return View(list);
        }

        [HttpPost]
        public JsonResult AddUser(FormCollection data)
        {
            string username = data["username"];
            string password = data["password"];

            JsonResult json = new JsonResult();

            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                json.Data = new
                {
                    status = "ERROR",
                    message = "Data cannot be empty"
                };
            }
            else
            {
                DBIO db = new DBIO();
                Users user = new Users();

                user.ID = db.GetLatestID() + 1;
                user.username = username;
                user.password = password;
                db.AddObject(user);
                db.Save();
                json.Data = new
                {
                    status = "OK"
                };
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUser(FormCollection data)
        {
            string id = data["id"];
     
            JsonResult json = new JsonResult();

            if (String.IsNullOrEmpty(id))
            {
                json.Data = new
                {
                    status = "ERROR",
                    message = "Data cannot be empty"
                };
            }
            else
            {
                DBIO db = new DBIO();
                Users user = db.GetObject_Users(id);
                
                if(user == null)
                {
                    json.Data = new
                    {
                        status = "ERROR",
                        message = "Data does not exist"
                    };
                }
                else
                {
                    db.DeleteObject(user);
                    db.Save();
                    json.Data = new
                    {
                        status = "OK"
                    };
                }
                
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditUser(FormCollection data)
        {
            string id = data["id"];
            string username = data["username"].Trim();
            string password = data["password"].Trim();

            JsonResult json = new JsonResult();

            if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(username))
            {
                json.Data = new
                {
                    status = "ERROR",
                    message = "Data cannot be empty"
                };
            }
            else
            {
                DBIO db = new DBIO();
                Users user = db.GetObject_Users(id);

                if (user == null)
                {
                    json.Data = new
                    {
                        status = "ERROR",
                        message = "Data does not exist"
                    };
                }
                else
                {
                    user.username = username;
                    if(!String.IsNullOrEmpty(password))
                    {
                        user.password = password;
                    }
                    db.Save();
                    json.Data = new
                    {
                        status = "OK"
                    };
                }

            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About(string id)
        {
            MyModel myModel = new MyModel();
            myModel.name = "Yumi";
            myModel.phone = "0385918111";
            ViewBag.Message = "Your application description page." + id;

            return View(myModel);
        }

        public ActionResult Contact(string name)
        {
            ViewBag.Message = "Your contact page." + name;

            return View();
        }
    }
}