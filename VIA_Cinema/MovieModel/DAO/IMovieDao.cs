﻿using System;
using System.Collections.Generic;

namespace DNP1.ViaCinema.Model.MovieModel.DAO
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMovieDao : IDisposable
    {
        /// <summary>
        ///     Creates a new movie entry in the movies entity
        /// </summary>
        /// <param name="movieName"> the name of hte movie </param>
        /// <param name="durationMinutes"> the duration of the movie </param>
        /// <param name="genre"> the genre of the movie </param>
        /// <returns> a movie object </returns>
        Movie Create(string movieName, int durationMinutes, string genre);

        /// <summary>
        ///     Reads a movie from the movies entity, that has the movie name passed as a parameter
        /// </summary>
        /// <param name="movieName"> the movie name </param>
        /// <returns> a movie object </returns>
        Movie Read(string movieName);

        /// <summary>
        ///     Reads all movie entries from the movie entity and returns a collection of them
        /// </summary>
        /// <returns> a collection of all movie objects </returns>
        ICollection<Movie> ReadAll();

        /// <summary>
        ///     Updates a movie entry in the database, with the data from the movie passed as a parameter
        /// </summary>
        /// <param name="updatedMovie"> the updated movie object </param>
        /// <returns> true, if the update operation has affected at least 1 database row. Otherwise, false </returns>
        bool Update(Movie updatedMovie);

        /// <summary>
        ///     Deletes a movie entry from the database that matches the properties of the movie object passed as a parameter
        /// </summary>
        /// <param name="movie"> the movie to be deleted </param>
        /// <returns> true, if the delete operation has affected at least 1 database row. Otherwise, false </returns>
        bool Delete(Movie movie);
    }
}