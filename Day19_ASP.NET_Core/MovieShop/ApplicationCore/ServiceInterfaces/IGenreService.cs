using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IGenreService
    {
        Task<List<GenreResponseModel>> GetAllGenres();
    }
}
