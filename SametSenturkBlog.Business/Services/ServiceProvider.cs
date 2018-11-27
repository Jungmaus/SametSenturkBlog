using SametSenturkBlog.Business.Services.EntityService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Business.Services
{
    public class ServiceProvider : IDisposable
    {
        private ArticleService _articleService;
        private CategoryService _categoryService;
        private ContactService _contactService;
        private DocumentService _documentService;
        private ImageService _imageService;
        private LogService _logService;
        private UserService _userService;
        private TagService _tagService;

        public ArticleService articleService { get { return _articleService ?? new ArticleService(); }}
        public CategoryService categoryService { get { return _categoryService ?? new CategoryService(); }}
        public ContactService contactService { get { return _contactService ?? new ContactService(); }}
        public DocumentService documentService { get { return _documentService ?? new DocumentService(); }}
        public ImageService imageService { get { return _imageService ?? new ImageService(); }}
        public LogService logService { get { return _logService ?? new LogService(); }  }
        public UserService userService { get { return _userService ?? new UserService(); }  }
        public TagService tagService{ get { return _tagService ?? new TagService(); }}


        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
