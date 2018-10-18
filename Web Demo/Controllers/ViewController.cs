﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System.IO;

namespace Show_Dashboard_in_the_Viewer.Controllers
{
    public class ViewController : Controller
    {
        private IHostingEnvironment environment;

        static ViewController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }
        
        public ViewController(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        
        public IActionResult Dashboards()
        {
            var dashboardFiles = Directory.GetFiles($"{environment.ContentRootPath}\\Dashboards", "*.mrt");
            var fileNames = new string[dashboardFiles.Length];
            var index = 0;
            foreach (var filePath in dashboardFiles)
            {
                fileNames[index++] = Path.GetFileNameWithoutExtension(filePath);
            }

            ViewBag.FileNames = fileNames;

            return View();
        }

        public IActionResult GetReport(string id)
        {
            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, $"Dashboards/{id}.mrt"));

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        public IActionResult Design(string id)
        {
            return RedirectToAction("Dashboards", "Design", new { id });
        }
    }
}