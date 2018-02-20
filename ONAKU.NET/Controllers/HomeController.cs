using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ONAKU.NET.Models;

namespace ONAKU.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly LeagueApplicationContext _context;

        public HomeController(LeagueApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Management()
        {
            return View();
        }
        public IActionResult ONAKU_LEAGUE()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }
        public IActionResult JoinOnakuLeague()
        {
            return View();
        }
      
        public IActionResult ProcessForm()
        {
            bool isError = false;
            string[] keys = new string[] { "nickName", "battleTag1", "battleTag2", "battleTag3", "battleTag4", "maxForLastSeason", "maxForThisSeason", "champ1", "champ2", "champ3", "area", "time1", "time2", "time3", "useMic", "discordTag", "myemail" };
            Dictionary<string, object> val = new Dictionary<string, object>();
            try
            {
                foreach (var k in keys)
                {
                    val[k] = HttpContext.Request.Form[k];
                }
                if (ModelState.IsValid)
                {

                }

                using (Models.LeagueApplicationContext ctx= new Models.LeagueApplicationContext())
                {

                }

                //using (SqlConnection con = new SqlConnection("Data Source=db2.djapple.com;Initial Catalog=onaku;User Id=sa;Password=Sun$hine1004;Trusted_Connection=False;"))
                //{
                //    string query1 = "insert into [dbo].[application](";
                //    for (int i = 0; i < keys.Length; i++)
                //    {
                //        query1 += keys;
                //        if (i != keys.Length - 1)
                //            query1 += ",";
                //    }
                //    query1 += ") VALUES ( ";
                //    for (int i = 0; i < keys.Length; i++)
                //    {
                //        query1 += "'" + val[System.Text.RegularExpressions.Regex.Replace(keys[i], "'", "''").Trim()] + "'";
                //        if (i != keys.Length - 1)
                //            query1 += ",";
                //    }
                //    query1 += ")";

                //    SqlCommand cmd = new SqlCommand(query1, con);
                //    int res = cmd.ExecuteNonQuery();

                //    ViewData["Message"] = "리그 입단신청에 문제가 있습니다. 운영진에게 연락해주세요";
                //    isError = true;
                //    if (res > 0)
                //    {
                //        isError = false;
                //        ViewData["Message"] = "리그 입단신청이 요청되었습니다.";
                //    }
                //}
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "리그 입단신청에 문제가 있습니다. 운영진에게 연락해주세요";
                isError = true;
            }

            string tmpFolder = @"c:\tmp";
            if(isError)
            {
                System.IO.File.WriteAllText(System.IO.Path.Combine(tmpFolder, DateTime.Now.ToFileTime().ToString() + ".json"),
                    Newtonsoft.Json.JsonConvert.SerializeObject(val)
                    );
            }            
            return View("joiningLeagueResult");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplication([Bind("uid,nickname,battleTag1,battleTag2,battleTag3,battleTag4,maxForLastSeason,maxForThisSeason,champ1,champ2,champ3,area,time1,time2,time3,useMic,discordTag,email,OnCreated")] LeagueApplication leagueApplication)
        {
            if (ModelState.IsValid)
            {
                leagueApplication.uid = Guid.NewGuid();
                _context.Add(leagueApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("joiningLeagueResult");
        }

    }
}
