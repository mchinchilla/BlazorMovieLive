using BlazorMovieLive.Models;
using System.Net.Http.Json;

namespace BlazorMovieLive.Services
{
    public class TMDBClient
    {
        private readonly HttpClient _httpClient;

        public TMDBClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

            string apiKey = config["TMDBKey"] ?? throw new Exception("TMDBKey not found!");
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        }

        public async Task<PopularMoviePagedResponse?> GetPopularMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return await Task.Run(() => _httpClient.GetFromJsonAsync<PopularMoviePagedResponse>($"movie/popular?language=en-US&page={page}"));
        }        
        
        
        public async Task<PopularMoviePagedResponse?> GetNowPlayingMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return await Task.Run(() => _httpClient.GetFromJsonAsync<PopularMoviePagedResponse>($"movie/now_playing?language=en-US&page={page}"));
        }


        public async Task<PopularMoviePagedResponse?> GetUpcomingMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return await Task.Run(() => _httpClient.GetFromJsonAsync<PopularMoviePagedResponse>($"movie/upcoming?language=en-US&page={page}"));
        }


        public async Task<PopularMoviePagedResponse?> GetTopRatedMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return await Task.Run(() => _httpClient.GetFromJsonAsync<PopularMoviePagedResponse>($"movie/top_rated?language=en-US&page={page}"));
        }

        public async Task<PopularMoviePagedResponse?> GetSearchedMoviesAsync(string? query, int page = 1)
        {
            if (string.IsNullOrEmpty(query)) return new PopularMoviePagedResponse();

            if (query.Contains(" ")) query = query.Replace(" ", "%20");

            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return await Task.Run(() => _httpClient.GetFromJsonAsync<PopularMoviePagedResponse>($"search/movie?query={query}&include_adult=false&language=en-US&page={page}"));
        }


        public async Task<MovieDetails?> GetMovieDetailsAsync(int id)
        {
            return await Task.Run(() => _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{id}"));
        }
    }
}
