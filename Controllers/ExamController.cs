using CloudDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudDemo.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Generate()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("http://158.101.231.162:8080/api/rest/iam/user/getByRole/Student").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            ViewBag.Users = new SelectList(JsonSerializer.Deserialize<List<UsersViewModel>>(content), "externalId", "firstName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(AddQuestionViewModel model)
        {
            HttpClient client = new HttpClient();
            var done = false;
            var type = "";
            if (model.type == "1")
            {
                type = "CS";
            }
            else if (model.type == "2")
            {
                type = "SOFTWARE_ENGINEER";
            }
            else
            {
                type = "MATH";
            }
            foreach (var item in model.Questions)
            {
                var questionModel = new QuestionModel()
                {
                    question = item.QuestionDesc,
                    type = type
                };
                var entity = JsonSerializer.Serialize(questionModel);
                var requestContent = new StringContent(entity, Encoding.UTF8, "application/json");
                var questionResponse = client.PostAsync("http://158.101.230.122:8080/questions", requestContent).Result;
                var content = questionResponse.Content.ReadAsStringAsync().Result;
                if ((int)questionResponse.StatusCode == 201 || (int)questionResponse.StatusCode == 200)
                {
                    done = true;
                }
                else
                {
                    done = false;
                }
            }
            if (!done)
            {
                var response = client.GetAsync("http://158.101.231.162:8080/api/rest/iam/user/getByRole/Student").Result;
                var content = response.Content.ReadAsStringAsync().Result;
                ViewBag.Users = new SelectList(JsonSerializer.Deserialize<List<UsersViewModel>>(content), "externalId", "firstName");
                return View();
            }
            else
            {
                return RedirectToAction("Generate");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GenerateAsync(int NoOfQues, int type, string externalId)
        {
            HttpClient client = new HttpClient();
            var selectedType = "";
            if (type == 1)
            {
                selectedType = "CS";
            }
            else if (type == 2)
            {
                selectedType = "SOFTWARE_ENGINEER";
            }
            else
            {
                selectedType = "MATH";
            }
            string cookieValueFromReq = Request.Cookies["ID"];
            var examResponse = await client.PostAsync("http://158.101.230.122:8080/exams/generateByType?examinerId=" + cookieValueFromReq + "&examineId=" + externalId + "&type="+ selectedType + "&numberOfQuestions=" + NoOfQues, null);
            if ((int)examResponse.StatusCode == 200 || (int)examResponse.StatusCode == 201)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var response = client.GetAsync("http://158.101.231.162:8080/api/rest/iam/user/getByRole/Student").Result;
                var content = response.Content.ReadAsStringAsync().Result;
                ViewBag.Users = new SelectList(JsonSerializer.Deserialize<List<UsersViewModel>>(content), "externalId", "firstName");
                return RedirectToAction("Index");
            }
        }
    }
}
