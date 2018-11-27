using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SametSenturkBlog.Business.Services;
using SametSenturkBlog.Data.Models.ORM.Entities;
using SametSenturkBlog.UI.Models.Types;
using SametSenturkBlog.UI.Models.VM.Home;

namespace SametSenturkBlog.UI.Controllers
{
    public class HomeController : Controller
    {
        private ServiceProvider Services;

        public HomeController()
        {
            Services = new ServiceProvider();
        }

        public IActionResult Home(string language)
        {
            if (language == string.Empty || (Enum.GetNames(typeof(EnumLanguageType)).FirstOrDefault(x => x == language) == null && Enum.GetNames(typeof(EnumLanguageType)).FirstOrDefault(x => x.ToLower() == language) == null))
                return RedirectToAction("Home", "Home", new { language = "tr" });
            else
            {
                ViewBag.language = language.ToLower();
                //List<ArticleListVM> model = Services.articleService.GetAllWithQuery(x=>x.LanguageType ==)
                return View();
            }
        }

        public IActionResult Contact(string language)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(int a, string language)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
       
        public IActionResult Documents(string language)
        {
            return View();
        }

        public IActionResult Images(string language)
        {
            return View();
        }

        public IActionResult PostsTagorCategoryBased(string language,string tagOrCategory)
        {
            List<Article> model = new List<Article>();

            if(Services.categoryService.FirstOrDefault(x=>x.SeoName == tagOrCategory && Enum.GetName(typeof(EnumLanguageType),x.LanguageType).Contains(language)) != null)
                model = Services.articleService.GetAllWithQuery(x => x.CategoryID == Services.categoryService.FirstOrDefault(y => y.SeoName == tagOrCategory && Enum.GetName(typeof(EnumLanguageType), x.LanguageType).Contains(language)).ID);

            else if(Services.tagService.FirstOrDefault(x=>x.SeoName == tagOrCategory && Enum.GetName(typeof(EnumLanguageType), x.LanguageType).Contains(language)) != null)
                model = Services.articleService.GetAllWithQuery(x => x.Tags.Any(y => y.ID == Services.tagService.FirstOrDefault(z => z.SeoName == tagOrCategory && Enum.GetName(typeof(EnumLanguageType), x.LanguageType).Contains(language)).ID));

            return View(model);
        }

        public IActionResult Post(string language,string category,string seoLink)
        {

            return View();
        }

        #region Json

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetTags(string language)
        {
            return Json(Services.tagService.GetAllWithQuery(x=> Enum.GetName(typeof(EnumLanguageType), x.LanguageType).Contains(language)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetCategories(string language)
        {
            return Json(Services.categoryService.GetAllWithQuery(x => Enum.GetName(typeof(EnumLanguageType), x.LanguageType).Contains(language.ToUpper())));
        }

        #endregion


    }
}