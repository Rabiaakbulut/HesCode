using HesCode.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HesCode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Tüm hes kodları json dosyasında varmış gibi düşündüm
            var webClient = new WebClient() { Encoding = Encoding.UTF8 };
            var json = webClient.DownloadString("C:/Users/LENOVO/source/repos/HesCode/HesCode/App_Data/hes.json");
            List<HesModel> allHesCodes = new JavaScriptSerializer().Deserialize<List<HesModel>>(json);

            //Kontrol edilecek hes kodları
            List<string> givenCodes = new List<string> { "G1B5-6449-15", "G1B5-6449-16" , "G1B5-6449-17", "GGGG-6449-17" };

            //Kontrol edilen kodların sınıflandırılması
            List<string> riskyCodes = new List<string>();
            List<string> risklessCodes = new List<string>();
            List<string> invalidCodes = new List<string>();

            //Verilen heslerin json dosyasından sorgulanıp gruplandırılması
            foreach (var item in givenCodes)
            {
                HesModel hescode = allHesCodes.Find(x => x.hes == item);
                if (hescode != null)
                {
                    if (hescode.status == "risky")
                    {
                        riskyCodes.Add(hescode.hes);
                    }
                    else
                    {
                        risklessCodes.Add(hescode.hes);
                    }
                }
                else
                {
                    invalidCodes.Add(item);
                }
            }

            ViewBag.givenCodes = givenCodes;
            ViewBag.riskyCodes = riskyCodes;
            ViewBag.risklessCodes = risklessCodes;
            ViewBag.invalidCodes = invalidCodes;
            return View();
        }
    }
}