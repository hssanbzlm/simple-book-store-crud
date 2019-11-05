using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstorerepository<Book> bookRepository;
        private readonly IBookstorerepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        // GET: Book  


        public BookController(IBookstorerepository<Book> bookRepository ,IBookstorerepository<Author> authorRepository,IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }

        public ActionResult Index()
        {

            var books = bookRepository.List(); 
           
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {

            var model = new BookAuthorViewModel()
            {
                Authors = FillSelectList(),
                
            }; 


            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel bookauthor)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    string fileName = UploadFile(bookauthor.file) ?? string.Empty;
                    
                    if (bookauthor.AuthorId == -1)
                    {
                        ViewBag.message = "please choose an author";

                        var model = new BookAuthorViewModel()
                        {
                            Authors = FillSelectList()
                        };


                        return View(model);
                    }
                    Book book = new Book
                    {
                        Description = bookauthor.Description,
                        Title = bookauthor.Title,
                        Author = authorRepository.Find(bookauthor.AuthorId),
                    
                        ImageUrl=fileName

                    };
                    bookRepository.Add(book);


                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            var modelv = new BookAuthorViewModel()
            {
                Authors = FillSelectList()
            };


            return View(modelv);
            
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);


            var model = new BookAuthorViewModel()
            {
                BookId = book.Id,
                Title = book.Title,
                AuthorId = book.Author.Id,
                Description = book.Description,
                Authors = FillSelectList(),
                ImageUrl=book.ImageUrl
                

            };

            return View(model);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    string fileName = UploadFile(model.file,model.ImageUrl);




                    if (model.AuthorId == -1)
                    {
                        ViewBag.message = "please choose an author";

                        var modelv = new BookAuthorViewModel()
                        {
                            Authors = FillSelectList()
                        };


                        return View(modelv);
                    }


                    var book = new Book()
                    {
                        Id = model.BookId,
                        Description = model.Description,
                        Title = model.Title,
                        Author = authorRepository.Find(model.AuthorId),
                        ImageUrl=fileName
                    };
                    bookRepository.Update(id, book);

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception e)
                {
                    var modelv = new BookAuthorViewModel()
                    {
                        Authors = FillSelectList()
                    };
                    Console.Write(e.Message);

                    return View(modelv);
                } 





            }

            var bookv = bookRepository.Find(id);

            var authorid = bookv.Author == null ? bookv.Author.Id = 0 : bookv.Author.Id;

            var vmodel = new BookAuthorViewModel()
            {
                BookId = bookv.Id,
                Title = bookv.Title,
                Description = bookv.Description,
                AuthorId = authorid,
                Authors = FillSelectList(),
                ImageUrl=bookv.ImageUrl


            };


            return View(vmodel);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            string name;

            var book =bookRepository.Find(id);

            var model = new BookAuthorViewModel
            {
                Description = book.Description,
                Title = book.Title,
                BookId = book.Id,
                AuthorId = book.Author.Id,




            };
            ViewBag.name = "";
            if (book.Author.FullName != "Not yet assigned")
            {
                name = authorRepository.Find(book.Author.Id).FullName;
                ViewBag.name = name;
            }
            
            
            return View(model);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BookAuthorViewModel model)
        {
            try
            {
                bookRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        } 
        [NonAction]
        List<Author> FillSelectList()
        {

            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author {Id=-1,FullName="--- please select an author" });
            return authors;
             
        } 

        string UploadFile(IFormFile file)
        {


            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
                return file.FileName;

            }
            return null;

        }


        string UploadFile(IFormFile file,string imageUrl)
        {


            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string newPath = Path.Combine(uploads, file.FileName);
                //Delete the old file 
                string oldpath = Path.Combine(uploads, imageUrl);

                if (oldpath != newPath)
                {
                    System.IO.File.Delete(oldpath);

                    using (var fileStream = new FileStream(newPath, FileMode.Create))
                    {

                        fileStream.Position = 0;
                        file.CopyToAsync(fileStream);
                    }
                }
                //Save the new file
                //model.file.CopyTo(new FileStream(fullPath, FileMode.Create)); 
                return file.FileName;


            }
            return imageUrl;

        } 
        public ActionResult Search(string term)
        {
            var result = bookRepository.Search(term);
            return View("Index",result);

        }

    }
}