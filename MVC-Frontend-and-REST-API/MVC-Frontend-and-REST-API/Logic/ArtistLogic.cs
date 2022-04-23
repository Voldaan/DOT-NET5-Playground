using MVC_Frontend_and_REST_API.Data;
using MVC_Frontend_and_REST_API.Data.Repositories;
using MVC_Frontend_and_REST_API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Logic
{
    public class ArtistLogic
    {
        private ArtistRepository _artistRepository;
        private DataContext _context;

        public ArtistLogic(DataContext dataContext)
        {
            _context = dataContext;
            _artistRepository = new ArtistRepository(_context);
        }

        public bool Create(Artist model)
        {
            return _artistRepository.Create(model);
        }

        public List<Artist> Read()
        {
            return _artistRepository.Read();
        }

        public bool Update(Artist model)
        {
            return _artistRepository.Update(model);
        }

        public bool Delete(Guid id)
        {
            return _artistRepository.Delete(id);
        }

        public bool SearchByName(Artist model)
        {
            return _artistRepository.Search(model.Name);
        }
    }
}
