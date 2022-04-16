using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Data
{
    interface ICrud<TEntity> where TEntity : class
    {
        bool Create(TEntity entity);
        List<TEntity> Read();
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }

}
