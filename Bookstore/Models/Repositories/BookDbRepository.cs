using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class BookDbRepository : IBookstorerepository<Book>
    {

        BookstoreDbContext db;
        public BookDbRepository(BookstoreDbContext _db)
        {
            db = _db;
            
        }

        public void Add(Book entity)
        {
            db.Books.Add(entity);
            saveChanges();
        }

        public void Delete(int Id)
        {
            var book = Find(Id);
            db.Books.Remove(book);
            saveChanges();

        }

        public Book Find(int Id)
        {
            var book = db.Books.Include(a=>a.Author).SingleOrDefault(b => b.Id == Id);
            return book;


        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList();

        }

        public void Update(int Id, Book newbook)
        {
            db.Books.Update(newbook);
            saveChanges();


        } 

        public void saveChanges()
        {
            db.SaveChanges();
        } 


        public List<Book> Search(string term)
        {
            var result = db.Books.Include(a=>a.Author)
                .Where(b => b.Title.Contains(term) 
                       ||b.Description.Contains(term) 
                       ||b.Title.Contains(term)).ToList();
            return result;

            

        }


    }
}


