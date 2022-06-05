  using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CloudDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudDemo.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            string cookieValueFromReq = Request.Cookies["ID"];
            HttpClient client = new HttpClient();
            var response = client.GetAsync("http://158.101.230.122:8080/exams/examineId/"+cookieValueFromReq).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var responseModel = JsonSerializer.Deserialize<List<ExamModel>>(content);
            return View(responseModel);
        }

        public async Task<IActionResult> Solve(int id)
        {
            HttpClient client = new HttpClient();
            string cookieValueFromReq = Request.Cookies["ID"];
            var response = client.GetAsync("http://158.101.230.122:8080/exams/questions/examID/" + id).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var responseModel = JsonSerializer.Deserialize<List<QuestionModel>>(content);
            return View(new ExamModel() {questions = responseModel });
        }

        [HttpPost]
        public IActionResult Solve(List<StudentAnswerModel> answers)
        {
            HttpClient client = new HttpClient();
            var done = false;
            foreach (var item in answers)
            {

                var entity = JsonSerializer.Serialize(item);
                var requestContent = new StringContent(entity, Encoding.UTF8, "application/json");
                var questionResponse = client.PostAsync("http://158.101.230.122:8080/answers", requestContent).Result;
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
                string cookieValueFromReq = Request.Cookies["ID"];
                var response = client.GetAsync("http://158.101.230.122:8080/exams/examineId/" + cookieValueFromReq).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var responseModel = JsonSerializer.Deserialize<List<ExamModel>>(content);
                return View(responseModel.First());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
