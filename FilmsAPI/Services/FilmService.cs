using System.Collections.Generic;
using FilmsApi.Model;
using MongoDB.Driver;

namespace FilmsApi.Services
{
    public class FilmService
    {
        private readonly IMongoCollection<Film> _Films;
        private readonly IMongoDatabase database;
        public FilmService(IFilmsDatabaseSettings settings) {
             var client = new MongoClient(settings.ConnectionString);
               database = client.GetDatabase(settings.DatabaseName);

             _Films  = database.GetCollection<Film>(settings.FilmsCollectionName[0]);

        }

        public List<Film> Get() => 
            _Films.Find(film => true).ToList();

        public List<Film> GetFilms(string collection) => 
                database.GetCollection<Film>(collection).Find(film => true).ToList();
        public Film Get(string id) => 
            _Films.Find<Film>(film => film.Id == id).FirstOrDefault();

        public List<Film> GetFilmName(string name) => 
            _Films.Find<Film>(film => film.FilmName.ToLower() == name.ToLower()).ToList();

        public Film Create(Film film)
        {
            _Films.InsertOne(film);
            return film;
        }

        public void Update(string id, Film filmIn) =>
            _Films.ReplaceOne(film => film.Id == id, filmIn);

        public void Remove(Film filmIn) =>
            _Films.DeleteOne(film => film.Id == filmIn.Id);


        public void Remove(string id) => 
            _Films.DeleteOne(film => film.Id == id);
    }
}