using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public interface IBookstorerepository<TEntity>
    {

        IList<TEntity> List();
        TEntity Find(int Id);
        void Add(TEntity entity);
        void Update(int Id,TEntity entity);
        void Delete(int Id);
        List<TEntity> Search(string term);



    }
}
