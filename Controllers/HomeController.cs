using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;
using ClassLibrary1.Dao;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()

        {
            BookDao bookDao = new BookDao();
            IList<Book> books = bookDao.GetAll();
            return View(books);
        }
        public ActionResult Detail(int id)
        {
            BookDao bookDao = new BookDao();
            Book b = bookDao.GetById(id);
            
            // Book b = (from l in Books.GetBooks where l.Id == id select l).FirstOrDefault();
            //var book = Books.GetBooks.FirstOrDefault(bl => bl.Name == Name);
            // var books = Books.GetBooks.Where(b => b.Name == Name).Average(k=>k.Pages);
            
            return View(b);        
        }
        public ActionResult Home()
        {
            PostDao postDao = new PostDao();
            IList<Post> posts = postDao.GetAll();

            return View(posts.OrderByDescending(d => d.Id).Take(4).ToList());
            
        }
    }
}