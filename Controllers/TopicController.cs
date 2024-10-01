using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FrontEndQuiz.Data;
using FrontEndQuiz.Models.SpecModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FrontEndQuiz.Controllers
{
     [ApiController]
    [Route("[controller]")]
    public class TopicController : Controller
    {

        private readonly DataContext _db;

        public TopicController(DataContext db) {

           _db = db;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Topic> topics = _db.Topics.ToList();
            return View(topics);
        }

  [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);

                // var relativePath = Path.Combine("./uploads", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Json(new { success = true, message = "File uploaded successfully", filepath = file.FileName });

            }
            return Json(new { success = false, message = "No file uploaded" });
        }


        // Endpoint to update Topic ImageFilePath after image is chosen
        // [HttpPost("UpdateTopic")]
        [HttpPost("{id}/{imagepath}")]
        public IActionResult UpdateImagePath(int id, string imagepath)
        {
            var topic = _db.Topics.FirstOrDefault(t => t.TopicId == id);
    
              string newImagePath = Path.Combine("./uploads", imagepath);
              topic.TopicImageFilePath = newImagePath;
    
            _db.SaveChanges();
             return Ok();

        }


        // GET a specific topic for the purposes of updating the topic image filepath (for now). Later this will evolve to update the image itself without needing to reload the page.

        [HttpGet("GetTopic/{id}")]
        public IActionResult GetSpecificTopic(int id)
        {
            var specificTopic = _db.Topics.FirstOrDefault(t => t.TopicId == id);
            return Ok(specificTopic);
        }

        [HttpGet("GetImages")]
        public IActionResult GetImages()
        {
            string[] imageFiles = Directory.GetFiles("wwwroot/uploads");
            string[] myList = new string[imageFiles.Count()];
            for (int i = 0; i < imageFiles.Length; i++) 
            {
                var imagePath = imageFiles[i].Replace("wwwroot", ".");
                myList[i] = imagePath;
              
            }
            return Ok(myList);
        }
    }
}