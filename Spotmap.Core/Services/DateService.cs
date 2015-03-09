using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Sqlite;
using SpotmapModel.Models;

namespace Spotmap.Core.Services
{
    public class DateService : IDataService
    {
        private readonly ISQLiteConnection _connection;

        public DateService(ISQLiteConnectionFactory factory)
        {
            _connection = factory.Create("spotmap.sql");
            _connection.CreateTable<Spot>();
        }

        public void AddSpot(Spot spot)
        {
            _connection.Insert(spot);
        }

        public List<Spot> GetAllSpotsWithLocation()
        {
            return _connection.Table<Spot>()
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.LocationKown == true)
                .ToList();
        }

        public List<Spot> GetAllSpotsWithoutLocation()
        {
            return _connection.Table<Spot>()
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.LocationKown == false)
                .ToList();
        }

        public Spot GetSpotById(int id)
        {
            return _connection.Get<Spot>(id);
        }

        public void DeleteSpot(int id)
        {
            _connection.Delete<Spot>(id);
        }
    }
}