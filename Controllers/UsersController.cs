using CloudDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudDemo.Controllers
{

    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> IndexAsync()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("http://158.101.231.162:8080/api/rest/iam/user/getByRole/Student").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var responseModel = JsonSerializer.Deserialize<List<UsersViewModel>>(content);
            foreach (var item in responseModel)
            {
                if (item.role == "Student")
                {
                    item.roleID = 2;
                }
                else
                {
                    item.roleID = 2;
                }
            }
            return View(responseModel);
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }
        public async Task<IActionResult> Edit(string id)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("http://158.101.231.162:8080/api/rest/iam/user/uuid/"+id).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var responseModel = JsonSerializer.Deserialize<UsersViewModel>(content);
            return View(responseModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var d = new UsersViewModel() { firstName = "Hamaza 2", lastName = "Alnesser" };
            return View(d);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UsersViewModel model)
        {
            HttpClient client = new HttpClient();
            if (model.roleID == 1)
            {
                model.role="Student";
            }
            else
            {
                model.role = "Admin";
            }
            var entity = JsonSerializer.Serialize(model);
            var requestContent = new StringContent(entity, Encoding.UTF8, "application/json");
            var response = client.PostAsync("http://158.101.231.162:8080/api/rest/iam/user/", requestContent).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UsersViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UsersViewModel model)
        {
            return View(model);
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
