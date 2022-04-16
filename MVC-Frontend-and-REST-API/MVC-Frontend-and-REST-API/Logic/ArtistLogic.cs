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

        public void Create(Artist model)
        {
            _artistRepository.Create(model);
            return;
        }

        public List<Artist> Read()
        {
            return _artistRepository.Read();
        }

        public void Update(Artist model)
        {
            _artistRepository.Update(model);
            return;
        }

        public void Delete(Guid id)
        {
            _artistRepository.Delete(id);
            return;
        }
    }
}
