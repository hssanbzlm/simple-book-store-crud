using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class AuthorDbRepository : IBookstorerepository<Author>
    {

        BookstoreDbContext db;
        
        public AuthorDbRepository(BookstoreDbContext _db)
        {
            db = _db;

           

          

        }


        public void Add(Author entity)
        {

            db.Authors.Add(entity);
            savechanges();



        }

        public void Delete(int Id)
        {
            db.Authors.Remove(db.Authors.Find(Id));
            savechanges();

        }

        public Author Find(int Id)
        {
            return db.Authors.FirstOrDefault(a => a.Id == Id);
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public void Update(int Id, Author entity)
        {
            db.Update(entity);
            savechanges();

        } 
        public void savechanges()
        {
            db.SaveChanges();
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();

        }

    }
}

    

