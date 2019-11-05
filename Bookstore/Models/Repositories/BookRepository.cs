using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstorerepository<Book>
    {

       List<Book> books;
        public BookRepository()
        {

            books = new List<Book>()
            {
                new Book{Id=1,Title="c# programming",Description="this is the best book",Author=new Author(),ImageUrl="x.jpg"},
                new Book{Id=2,Title="Angular ",Description="front end development",Author=new Author(),ImageUrl="x.jpg"},
                new Book{Id=3,Title="Asp.net core",Description="Backend development",Author=new Author(),ImageUrl="x.jpg"}
         


            };
        }

        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1; 
            books.Add(entity);
        }

        public void Delete(int Id)
        {
            var book = Find(Id);
            books.Remove(book);
                
        }

        public Book Find(int Id)
        {
            var book = books.SingleOrDefault(b => b.Id == Id);
            return book;


        }

        public IList<Book> List()
        {
            return books;

        }

        public void Update(int Id,Book newbook)
        {
            var book = Find(Id);
            book.Title = newbook.Title;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.ImageUrl = newbook.ImageUrl;
            
               
        }

        public List<Book> Search(string term)
        {
            return books.Where(b => b.Description.Contains(term) || b.Title.Contains(term)).ToList(); 
        }

    }
}
