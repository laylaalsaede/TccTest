using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TccTest.Models;
using TccTest.Models.ViewModel;

namespace TccTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
       public IActionResult PostFile(UploadViewModel model)
        {
            // 1- [Done] read the uploaded file from (FORM)  
            // 2- [Done] save the first line in varible uploadedline
            // 3- [Done] open the master text
            // 4- [Done] add (append) the uploadedline in master text

            if (model.NewFile == null)
            {
                return RedirectToAction("Index");
            }

            string uploadedline = null;
            var newFile = model.NewFile.OpenReadStream();

            using (StreamReader file = new(newFile))
            {
                if (file.Peek() >= 0)
                {
                    uploadedline =  file.ReadLine();
                }

                //for( var i=0; i<= file.Peek(); i++)
                //{
                //    if(i == 1)
                //    {
                //        uploadedline = file.ReadLine();
                //    }
                //}
            }

            using (StreamWriter master = new(Path.Combine("wwwroot/Master.txt"), true))
            {
                    master.WriteLine(uploadedline);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CleareFile(UploadViewModel model)
        {
            System.IO.File.WriteAllBytes(Path.Combine("wwwroot/Master.txt"), new byte[0]);
            
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
