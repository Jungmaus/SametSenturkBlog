using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SametSenturkBlog.Business.Library;
using SametSenturkBlog.Business.Services;
using SametSenturkBlog.Data.Models.ORM.Entities;
using SametSenturkBlog.UI.Models.Types;
using SametSenturkBlog.UI.Models.VM.Panel;

namespace SametSenturkBlog.UI.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private ServiceProvider service;
        private IHttpContextAccessor accessor;
        private IHostingEnvironment environment;

        private int GetCurrentUserID()
        {
            var name = User.Claims.Where(c => c.Type == ClaimTypes.Name)
               .Select(c => c.Value).SingleOrDefault();
            return service.userService.FirstOrDefault(x => x.Username == name).ID;
        }

        public PanelController(IHttpContextAccessor _accessor, IHostingEnvironment _environment)
        {
            service = new ServiceProvider();
            accessor = _accessor;
            environment = _environment;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel lm, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = service.userService.FirstOrDefault(x => x.Username == lm.Username && x.Password == lm.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,lm.Username)
                    };
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    user.LastLogin = DateTime.Now;
                    user.IpAdress = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    service.userService.Update(user);
                    service.logService.Insert(new Data.Models.ORM.Entities.Log { Subject = "User Login", Description = "UserID: " + user.ID + " logined at admin panel.", IpAdress = accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Type = (int)EnumLogType.UserLoginSuccess });
                    return Redirect(ReturnUrl ?? "/Panel/Index");
                }
                else
                {
                    service.logService.Insert(new Data.Models.ORM.Entities.Log { Subject = "Login Try", Description = "Tryed login to admin panel.", IpAdress = accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Type = (int)EnumLogType.UserLoginFailure });
                    ViewBag.OperationStatus = EnumOperationType.Error;
                    ModelState.AddModelError("", "Wrong username/email or password.");
                }
            }
            else
                ViewBag.OperationStatus = EnumOperationType.Error;
            return View(lm);
        }


        public IActionResult Index()
        {
            User user = service.userService.FirstOrDefault(x => x.ID == GetCurrentUserID());
            List<ArticleModel> model = service.articleService.GetAllWithQuery(x => user.Role != 2 ? x.UserID == GetCurrentUserID() : true).OrderByDescending(x => x.AddDate).Select(s => new ArticleModel
            {
                ID = s.ID,
                Title = s.Subject,
                ImgPath = service.imageService.FirstOrDefault(x => x.ArticleID == s.ID && x.Type == (int)EnumImageType.TitleImage)?.Path,
                Language = Enum.GetName(typeof(EnumLanguageType), s.LanguageType),
                SeeCount = s.ReadCount,
                LikeCount = s.LikeCount,
                DontLikeCount = s.DontLikeCount,
                DescriptionCount = s.Content.Length,
                CategoryName = service.categoryService.FirstOrDefault(x => x.ID == s.CategoryID
                )?.Name
            }).ToList();

            return View(model);
        }


        public IActionResult AddArticle()
        {
            AddArticleModel am = new AddArticleModel();
            am.CategoryList = service.categoryService.GetAll().Select(s => new SelectListItem { Text = s.Name + " " + "(" + Enum.GetName(typeof(EnumLanguageType), s.LanguageType) + ")", Value = s.ID.ToString() }).ToList();
            int sayac = 0;
            foreach (var item in Enum.GetNames(typeof(EnumLanguageType)))
            {
                am.LanguageTypes.Add(new SelectListItem { Text = item, Value = sayac.ToString() });
                sayac++;
            }
            return View(am);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleModel am)
        {
            try
            {
                Article article = new Article();
                article.Subject = am.Subject;
                article.SeoSubject = SeoLib.GetSeoLink(am.Subject);
                article.CategoryID = am.CategoryId;
                article.LanguageType = am.LanguageType;
                article.UserID = GetCurrentUserID();
                article.Content = am.Content;
                service.articleService.Insert(article);

                if (am.file != null && am.file.Length > 0)
                {
                    Image image = new Image();
                    image.LanguageType = am.LanguageType;
                    image.Type = (int)EnumImageType.TitleImage;
                    image.ArticleID = service.articleService.GetAll().LastOrDefault(x => x.Subject == am.Subject && x.CategoryID == am.CategoryId && x.LanguageType == am.LanguageType && x.UserID == GetCurrentUserID()).ID;

                    string pathName = Guid.NewGuid() + am.file.FileName;
                    var path = Path.Combine(
                 Directory.GetCurrentDirectory(), "wwwroot/Images/TitleImages",
                 pathName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await am.file.CopyToAsync(stream);
                    }

                    image.Name = pathName;
                    image.Path = "/Images/TitleImages/" + pathName;
                    service.imageService.Insert(image);
                }
                ViewBag.OperationStatus = EnumOperationType.Success;
            }
            catch (Exception ex)
            {
                ViewBag.OperationStatus = EnumOperationType.Error;
                ModelState.AddModelError("", "Something went wrong (" + ex.ToString() + ")");
            }
            am.CategoryList = service.categoryService.GetAll().Select(s => new SelectListItem { Text = s.Name + " " + "(" + Enum.GetName(typeof(EnumLanguageType), s.LanguageType) + ")", Value = s.ID.ToString() }).ToList();
            int sayac = 0;
            foreach (var item in Enum.GetNames(typeof(EnumLanguageType)))
            {
                am.LanguageTypes.Add(new SelectListItem { Text = item, Value = sayac.ToString() });
                sayac++;
            }
            return View(am);
        }


        public IActionResult EditArticle(int id)
        {
            Article article = service.articleService.FirstOrDefault(x => x.ID == id);
            if ((article != null) && (article.UserID == GetCurrentUserID()) || (service.userService.FirstOrDefault(x=>x.ID == GetCurrentUserID()).Role == 2))
            {
                AddArticleModel model = new AddArticleModel();
                model.ID = article.ID;
                model.CategoryId = article.CategoryID;
                model.Content = article.Content;
                model.LanguageType = (int)article.LanguageType;
                model.Subject = article.Subject;
                model.PicPath = service.imageService.FirstOrDefault(x => x.ArticleID == article.ID && x.Type == (int)EnumImageType.TitleImage)?.Path;
                model.CategoryList = service.categoryService.GetAll().Select(s => new SelectListItem { Text = s.Name + " " + "(" + Enum.GetName(typeof(EnumLanguageType), s.LanguageType) + ")", Value = s.ID.ToString(), Selected = model.CategoryId == s.ID ? true : false }).ToList();
                int sayac = 0;
                foreach (var item in Enum.GetNames(typeof(EnumLanguageType)))
                {
                    model.LanguageTypes.Add(new SelectListItem { Text = item, Value = sayac.ToString(), Selected = Enum.GetName(typeof(EnumLanguageType),model.LanguageType) == item ? true : false });
                    sayac++;
                }
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(AddArticleModel am)
        {
            try
            {
                Article article = service.articleService.FirstOrDefault(x => x.ID == am.ID);
                if (article != null && (article.UserID == GetCurrentUserID()) || service.userService.FirstOrDefault(x=>x.ID == GetCurrentUserID()).Role == 2)
                {
                    article.CategoryID = am.CategoryId;
                    article.Content = am.Content;
                    article.LanguageType = am.LanguageType;
                    article.Subject = am.Subject;
                    article.SeoSubject = SeoLib.GetSeoLink(am.Subject);
                    service.articleService.Update(article);

                    if (am.file != null && am.file.Length > 0)
                    {
                        Image image = service.imageService.FirstOrDefault(x => x.ArticleID == article.ID && x.Type == (int)EnumImageType.TitleImage);
                        if (image != null)
                        {
                            var pathh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/TitleImages", image.Name);
                            if (System.IO.File.Exists(pathh))
                                System.IO.File.Delete(pathh);
                        }

                        string pathName = Guid.NewGuid() + am.file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/TitleImages", pathName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await am.file.CopyToAsync(stream);
                        }

                        image.Name = pathName;
                        image.Path = "/Images/TitleImages/" + pathName;
                        am.PicPath = image.Path;
                        service.imageService.Update(image);
                    }
                    ViewBag.OperationStatus = EnumOperationType.Success;


                    am.CategoryList = service.categoryService.GetAll().Select(s => new SelectListItem { Text = s.Name + " " + "(" + Enum.GetName(typeof(EnumLanguageType), s.LanguageType) + ")", Value = s.ID.ToString() }).ToList();
                    int sayac = 0;
                    foreach (var item in Enum.GetNames(typeof(EnumLanguageType)))
                    {
                        am.LanguageTypes.Add(new SelectListItem { Text = item, Value = sayac.ToString() });
                        sayac++;
                    }

                    return View(am);
                }
                else
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {

                am.CategoryList = service.categoryService.GetAll().Select(s => new SelectListItem { Text = s.Name + " " + "(" + Enum.GetName(typeof(EnumLanguageType), s.LanguageType) + ")", Value = s.ID.ToString() }).ToList();
                int sayac = 0;
                foreach (var item in Enum.GetNames(typeof(EnumLanguageType)))
                {
                    am.LanguageTypes.Add(new SelectListItem { Text = item, Value = sayac.ToString() });
                    sayac++;
                }
                ViewBag.OperationStatus = EnumOperationType.Error;
                ModelState.AddModelError("", "Something went wrong. (" + ex.ToString() + ")");
                return View(am);
            }
        }

        [HttpPost]
        public int DeleteArticle(int articleId)
        {
            Article article = service.articleService.FirstOrDefault(x => x.ID == articleId && (x.UserID == GetCurrentUserID()) || service.userService.FirstOrDefault(y => y.ID == GetCurrentUserID()).Role == 2);
            User user = service.userService.FirstOrDefault(x => x.ID == GetCurrentUserID());
            if (article != null && user.Role == 2)
            {
                foreach (var item in service.imageService.GetAllWithQuery(x => x.ArticleID == article.ID))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/TitleImages", item.Name);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    service.imageService.Delete(item.ID);
                }
                service.articleService.Delete(article.ID);
                return 1;
            }
            else
                return 2;
        }

        public IActionResult CategoryList()
        {
            List<CategoryModel> model = service.categoryService.GetAll().Select(s => new CategoryModel {
                AddDate = s.AddDate,
                ID = s.ID,
                ArticleCount = service.articleService.GetAllWithQuery(x=>x.CategoryID == s.ID).Count,
                isDeletable = service.articleService.GetAllWithQuery(x=>x.CategoryID == s.ID).Count > 0 ? false : true,
                Name = s.Name,
                 Language = Enum.GetName(typeof(EnumLanguageType),s.LanguageType)
            }).OrderByDescending(x=>x.AddDate).ToList();


            int sayac = 0;
            List<SelectListItem> languageTypes = new List<SelectListItem>();
            foreach (var item in Enum.GetNames(typeof(EnumLanguageType))) {
                languageTypes.Add(new SelectListItem { Value = sayac.ToString(), Text = item });
                sayac++;
            }

            ViewBag.languageTypes = languageTypes;

            return View(model);
        }

        [HttpPost]
        public int AddCategory(string name,int languageType)
        {
            Category category = service.categoryService.FirstOrDefault(x => x.Name == name && x.LanguageType == languageType);
            if(category == null)
            {
                service.categoryService.Insert(new Category { Name = name, LanguageType = languageType, SeoName = SeoLib.GetSeoName(name) });
                return 1;
            }
            return 2;
        }

        [HttpGet]
        public JsonResult GetCategory(int id)
        {
            return Json(service.categoryService.FirstOrDefault(x => x.ID == id));
        }

        [HttpPost]
        public int EditCategory(int id,string name,int languageType)
        {
            Category category = service.categoryService.FirstOrDefault(x => x.ID == id);
            if(category != null)
            {
                category.Name = name;
                category.SeoName = SeoLib.GetSeoName(name);
                category.LanguageType = languageType;
                service.categoryService.Update(category);
                return 1;
            }
            return 2;
        }

        [HttpPost]
        public int DeleteCategory(int id)
        {
            Category category = service.categoryService.FirstOrDefault(x => x.ID == id);
            if(category != null && !service.articleService.AnyWithQuery(x=>x.CategoryID == category.ID))
            {
                service.categoryService.Delete(category.ID);
                return 1;
            }
            return 0;
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult ContactDetail(int id)
        {
            return View();
        }

        public int DeleteContact(int id)
        {
            return 1;
        }

        public IActionResult Documents()
        {
            return View();
        }

        public IActionResult AddDocument()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDocument(int a)
        {
            return View();
        }

        public IActionResult EditDocument(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditDocument(int a, int b)
        {
            return View();
        }

        public IActionResult UploadedImages()
        {
            List<UploadedImages> model = service.imageService.GetAllWithQuery(x=>x.ArticleID == new int?()).Select(s => new UploadedImages {
                AddDate = s.AddDate,
                FileName = s.Name,
                FullPath = Request.Host.Value + s.Path,
                Path = s.Path
            }).OrderByDescending(x=>x.AddDate).ToList();

            return View(model);
        }

        public IActionResult UploadImage() => View();

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                Image image = new Image();

                string pathName = Guid.NewGuid() + file.FileName;
                var path = Path.Combine(
             Directory.GetCurrentDirectory(), "wwwroot/Images/TitleImages",
             pathName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                image.Name = pathName;
                image.Path = "/Images/TitleImages/" + pathName;
                ViewBag.path = image.Path;
                service.imageService.Insert(image);
            }
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(int a)
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}