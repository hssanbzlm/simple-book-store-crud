using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class AuthorRepository : IBookstorerepository<Author>
    {

        IList<Author> authors;
        public AuthorRepository()
        {

            authors = new List<Author>()
            {

                new Author{Id=1,FullName="Hssan Bouzlima"},
                new Author{Id=2,FullName="sirine trabelsi"},
                new Author{Id=3,FullName="nour kharbechi"}


            };

        }

         
        public void Add(Author entity)
        {
            entity.Id = authors.Max(a => a.Id) + 1;
            authors.Add(entity);


        }

        public void Delete(int Id)
        {
            var author = Find(Id);
            authors.Remove(author);
        }

        public Author Find(int Id)
        {
            return authors.FirstOrDefault(a => a.Id == Id);
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int Id, Author entity)
        {
            var author = Find(Id);
            author.FullName = entity.FullName;

        }

        public List<Author> Search(string term)
        {
            return authors.Where(a => a.FullName.Contains(term)).ToList();
        }

    }
}
