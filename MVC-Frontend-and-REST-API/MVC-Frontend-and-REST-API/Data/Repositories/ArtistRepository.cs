using MVC_Frontend_and_REST_API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Data.Repositories
{
    public class ArtistRepository
    {
        private CrudRepository<Artist> _crudRepository; 
        private DataContext _context;

        public ArtistRepository(DataContext dataContext)
        {
            _context = dataContext;
            _crudRepository = new CrudRepository<Artist>(_context);
        }

        public bool Create(Artist model)
        {
            return _crudRepository.Create(model);
        }

        public List<Artist> Read()
        {
            return _crudRepository.Read().OrderBy(x => x.Name).ToList();
        }

        public bool Update(Artist model)
        {
            return _crudRepository.Update(model);
        }

        public bool Delete(Guid id)
        {
            return _crudRepository.Delete(_crudRepository.Read().Where(x => x.Id == id).FirstOrDefault());
        }

        public bool Search(string name)
        {
            var response = _context.Artists.Where(x => x.Name == name).FirstOrDefault();

            if (response != null) return true;
            return false;
        }

    }
}
