using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SametSenturkBlog.Data.Models.ORM.Context;

namespace SametSenturkBlog.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
             {
                 options.LoginPath = "/Panel/Login/";
             });

            services.AddDbContext<SimgeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlServerOption => sqlServerOption.MigrationsAssembly("SametSenturkBlog.Data")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                name: "PanelContactDetail",
                template: "/panel/iletisim-mesaji/{id}",
                defaults: new { controller = "Panel", action = "ContactDetail" });

                routes.MapRoute(
                name: "PanelContacts",
                template: "/panel/iletisim-mesajlari",
                defaults: new { controller = "Panel", action = "Contacts" });

                routes.MapRoute(
                name: "PanelDeleteCategory",
                template: "/panel/kategori-sil/{id}",
                defaults: new { controller = "Panel", action = "DeleteCategory" });

                routes.MapRoute(
                name: "PanelEditCategory",
                template: "/panel/kategori-duzenle/{id}/{name}/{languageType}",
                defaults: new { controller = "Panel", action = "EditCategory" });

                routes.MapRoute(
                name: "PanelDeleteArticle",
                template: "/panel/makale-sil/{articleId}",
                defaults: new { controller = "Panel", action = "DeleteArticle" });

                routes.MapRoute(
                name: "PanelGetCategory",
                template: "/panel/json/kategori-listesi/{id}",
                defaults: new { controller = "Panel", action = "GetCategory" });

                routes.MapRoute(
                name: "PanelAddCategory",
                template: "/panel/kategori-ekle/{name}/{languageType}",
                defaults: new { controller = "Panel", action = "AddCategory" });

                routes.MapRoute(
                name: "PanelCategoryList",
                template: "/panel/kategori-listesi",
                defaults: new { controller = "Panel", action = "CategoryList" });

                routes.MapRoute(
                name: "PanelEditArticle",
                template: "/panel/makale-duzenle/{id}",
                defaults: new { controller = "Panel", action = "EditArticle" });

                routes.MapRoute(
                name: "PanelAddArticle",
                template: "/panel/makale-ekle",
                defaults: new { controller = "Panel", action = "AddArticle" });

                routes.MapRoute(
                name: "PanelArticleList",
                template: "/panel/makale-listesi",
                defaults: new { controller = "Panel", action = "Index" });

                routes.MapRoute(
                name: "PanelLogin",
                template: "/panel/giris-yap",
                defaults: new { controller = "Panel", action = "Login" });

                routes.MapRoute(
                name: "gettags",
                template: "/json/getTags",
                defaults: new { controller = "Home", action = "GetTags" });

                routes.MapRoute(
                name: "getcategories",
                template: "/json/getCategories",
                defaults: new { controller = "Home", action = "GetCategories" });

                routes.MapRoute(
                name: "home",
                template: "/{language}",
                defaults: new { controller = "Home", action = "Home" });

                routes.MapRoute(
                name: "post",
                template: "/{language}/{category}/{seoLink}",
                defaults: new { controller = "Home", action = "Post" });

                routes.MapRoute(
                name: "contact",
                template: "en/contact",
                defaults: new { controller = "Home", action = "Contact" });

                routes.MapRoute(
                name: "iletisim",
                template: "tr/iletisim",
                defaults: new { controller = "Home", action = "Contact" });

                routes.MapRoute(
                name: "kommunikation",
                template: "de/kommunikation",
                defaults: new { controller = "Home", action = "Contact" });

                routes.MapRoute(
                name: "documents",
                template: "en/documents",
                defaults: new { controller = "Home", action = "Documents" });

                routes.MapRoute(
                name: "dokumanlar",
                template: "tr/dokumanlar",
                defaults: new { controller = "Home", action = "Documents" });

                routes.MapRoute(
                name: "unterlagen",
                template: "de/unterlagen",
                defaults: new { controller = "Home", action = "Documents" });

                routes.MapRoute(
                name: "images",
                template: "en/images",
                defaults: new { controller = "Home", action = "Images" });

                routes.MapRoute(
                name: "resimler",
                template: "tr/resimler",
                defaults: new { controller = "Home", action = "Images" });

                routes.MapRoute(
                name: "bilder",
                template: "de/bilder",
                defaults: new { controller = "Home", action = "Images" });

                routes.MapRoute(
                name: "PostsTagorCategoryBased",
                template: "post/{language}/{tagOrCategory}",
                defaults: new { controller = "Home", action = "PostsTagorCategoryBased" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Home}/{id?}");
            });
        }
    }
}
