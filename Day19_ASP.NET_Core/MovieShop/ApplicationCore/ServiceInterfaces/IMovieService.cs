﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        //method for getting top 30 highest movies
        List<MovieCardResponseModel> GetTopRevenueMovies();
        MovieDetailsReposonseModel GetMovieDetailsById(int id);
    }
}
