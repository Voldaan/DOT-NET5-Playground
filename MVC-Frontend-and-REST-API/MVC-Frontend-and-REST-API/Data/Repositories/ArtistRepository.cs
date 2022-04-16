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

        public void Create(Artist model)
        {
            _crudRepository.Create(model);
        }

        public List<Artist> Read()
        {
            //return _crudRepository.Read().Where(x => x.WorldId == InformationStorage.worldId).OrderBy(character => character.Surname).ToList();
            return _crudRepository.Read().OrderBy(x => x.Name).ToList();
        }

        public void Update(Artist model)
        {
            _crudRepository.Update(model);
        }

        public void Delete(Guid id)
        {
            _crudRepository.Delete(_crudRepository.Read().Where(x => x.Id == id).FirstOrDefault());
        }

    }
}
