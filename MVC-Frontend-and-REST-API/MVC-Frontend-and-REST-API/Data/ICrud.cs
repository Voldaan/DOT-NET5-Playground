using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Data
{
    interface ICrud<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        List<TEntity> Read();
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

}
