using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;
using ClassLibrary1.Dao;
using MyBlog.Class;

namespace MyBlog.Controllers
{
    public class FormController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            BookDao bookDao = new BookDao();
            IList<Book> books = bookDao.GetAll();
            return View(books);
        }

        public ActionResult CreatePost()
        {
            return View();
        }
        public ActionResult Create()
        {
            BookCategoryDao bookCategoryDao = new BookCategoryDao();
            IList<BookCategory> categories = bookCategoryDao.GetAll();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        public ActionResult AddPost(Post post, HttpPostedFileBase picture)
        {
            
            
            post.Date = DateTime.Now.ToString();
            if (ModelState.IsValid) // kontroluje, zda jsou všechny pole vyplněné a validní, pokud ano, provede
            {
                if (picture != null)
                {
                    // picture.SaveAs(Server.MapPath("~/Uploads/Book/")+picture.FileName); jedná se o obecnou přílohu, bez kontroly, zda vyhovuje parametrům (rozměry, scale...)
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 400, 400);
                            Bitmap b = new Bitmap(smallImage);
                            Guid guid = Guid.NewGuid();
                            string imageName = guid.ToString() + ".jpg";
                            b.Save(Server.MapPath("~/Uploads/Post/" + imageName), ImageFormat.Jpeg);
                            smallImage.Dispose();
                            // vycisteni objektu
                            b.Dispose();

                            post.ImageName = imageName;
                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/Uploads/Book/" + picture.FileName));
                        }
                    }
                }
                    PostDao postDao = new PostDao();
                
                postDao.Create(post);

                TempData["message-success"] = "Záznam byl úspěšně přidán!";
            }
            else
            {
                TempData["message-error"] = "Záznam nebyl úspěšně přidán!";
                return View("CreatePost", post);

            }
            return RedirectToAction("Posts");

            }
        // public ActionResult Add(int? id, string Name, string Author, int Published, int Pages)
        [HttpPost]
        public ActionResult UpdatePost(Post post, HttpPostedFileBase picture)
        {


            post.Date = DateTime.Now.ToString();
            if (ModelState.IsValid) // kontroluje, zda jsou všechny pole vyplněné a validní, pokud ano, provede
            {
                if (picture != null)
                {
                    // picture.SaveAs(Server.MapPath("~/Uploads/Book/")+picture.FileName); jedná se o obecnou přílohu, bez kontroly, zda vyhovuje parametrům (rozměry, scale...)
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 400, 400);
                            Bitmap b = new Bitmap(smallImage);
                            Guid guid = Guid.NewGuid();
                            string imageName = guid.ToString() + ".jpg";
                            b.Save(Server.MapPath("~/Uploads/Post/" + imageName), ImageFormat.Jpeg);
                            smallImage.Dispose();
                            // vycisteni objektu
                            b.Dispose();

                            post.ImageName = imageName;
                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/Uploads/Book/" + picture.FileName));
                        }
                    }
                }
                PostDao postDao = new PostDao();

                postDao.Update(post);

                TempData["message-success"] = "Záznam byl úspěšně přidán!";
            }
            else
            {
                TempData["message-error"] = "Záznam nebyl úspěšně přidán!";
                return View("CreatePost", post);

            }
            return RedirectToAction("Posts");

        }
        public ActionResult Add(Book book, HttpPostedFileBase picture, int categoryId)
        {
            if (ModelState.IsValid) // kontroluje, zda jsou všechny pole vyplněné a validní, pokud ano, provede
            {

                if (picture != null)
                {
                    // picture.SaveAs(Server.MapPath("~/Uploads/Book/")+picture.FileName); jedná se o obecnou přílohu, bez kontroly, zda vyhovuje parametrům (rozměry, scale...)
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap b = new Bitmap(smallImage);
                            Guid guid = Guid.NewGuid();
                            string imageName = guid.ToString() + ".jpg";
                            b.Save(Server.MapPath("~/Uploads/Book/" + imageName), ImageFormat.Jpeg);
                            smallImage.Dispose();
                            // vycisteni objektu
                            b.Dispose();

                            book.ImageName = imageName;
                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/Uploads/Book/" + picture.FileName));
                        }
                    }

                }
                BookCategoryDao bookCategoryDao = new BookCategoryDao();
                BookCategory bookCategory = bookCategoryDao.GetById(categoryId);
                book.Category = bookCategory;
                BookDao bookDao = new BookDao();
                bookDao.Create(book);

                TempData["message-success"] = "Kniha byla úspěšně přidána!";
            }
            else
            {
                TempData["message-error"] = "Kniha nebyla úspěšně přidána!";
                return View("Create", book);

            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            BookDao bookDao = new BookDao();
            BookCategoryDao bookCategoryDao = new BookCategoryDao();
            Book b = bookDao.GetById(id);
            ViewBag.Categories = bookCategoryDao.GetAll();
            return View(b);

        }
        public ActionResult EditPost(int id)
        {
            PostDao postDao = new PostDao();
            Post p = postDao.GetById(id);
           
            return View(p);

        }
        public ActionResult Update(Book book, HttpPostedFileBase picture, int categoryId)
        {
            try
            {
                BookDao bookDao = new BookDao();
                BookCategoryDao bookCategoryDao = new BookCategoryDao();
                BookCategory bookCategory = bookCategoryDao.GetById(categoryId);
                book.Category = bookCategory;

                if (picture != null)
                {
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);
                        Guid guid = Guid.NewGuid();
                        string imageName = guid.ToString() + ".jpg";

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap b = new Bitmap(smallImage);

                            b.Save(Server.MapPath("~/Uploads/Book/" + imageName), ImageFormat.Jpeg);
                            smallImage.Dispose();
                            // vycisteni objektu
                            b.Dispose();

                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/Uploads/Book/" + picture.FileName));
                        }
                        System.IO.File.Delete(Server.MapPath("~/Uploads/Book" + imageName));
                        book.ImageName = imageName;
                    }

                }
                bookDao.Update(book);

                TempData["message-success"] = "Kniha " + book.Name + " byla upravena.";
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index", "Form");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                BookDao bookDao = new BookDao();
                Book book = bookDao.GetById(id);
                if (book.ImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/Book" + book.ImageName));
                }
                 
                    bookDao.Delete(book);

                TempData["message-success"] = "Kniha " + book.Name + " byla smazána.";
            }
            catch (Exception exception)
            {

                throw;
            }
            return RedirectToAction("Index");

        }
        public ActionResult DeletePost(int id)
        {
            try
            {
                PostDao postDao = new PostDao();
                Post post = postDao.GetById(id);
                if (post.ImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/Post" + post.ImageName));
                }

                postDao.Delete(post);

                TempData["message-success"] = "Článek " + post.PostName + " byla smazán.";
            }
            catch (Exception exception)
            {

                throw;
            }
            return RedirectToAction("Posts");

        }
        public ActionResult Posts()
        {
            int id = 1;
            ViewBag.a = id;
            PostDao postDao = new PostDao();
            IList<Post> posts=postDao.GetAll();
            return View(posts);
        }
        public ActionResult Detail(int id)
        {
            PostDao postDao = new PostDao();
            Post p = postDao.GetById(id);

            
            return View(p);
        }

    }
}
