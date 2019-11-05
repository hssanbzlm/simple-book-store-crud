﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookstorerepository<Author> authorRepository;

        // GET: Author 


        public AuthorController(IBookstorerepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public ActionResult Index() 
        {

            var authors = authorRepository.List();
            return View(authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepository.Find(id);
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            var author = new Author
            {
                FullName = ""
                
            };

            return View(author);
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    authorRepository.Add(author);


                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                } 
 
            }

            var authorv = new Author
            {
                Id = authorRepository.List().Max(a => a.Id) + 1,
                FullName = ""

            };
            return View(authorv);
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id) 

        {
            var author = authorRepository.Find(id);
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {

            if (ModelState.IsValid)
            {

                try
                {

                    authorRepository.Update(id, author);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            var authorv = authorRepository.Find(id);
            return View(authorv);
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepository.Find(id);
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {

                authorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}