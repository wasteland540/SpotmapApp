using System.Collections.Generic;
using SpotmapModel.Models;

namespace Spotmap.Core.Services
{
    public interface IDataService
    {
        void AddSpot(Spot spot);
        List<Spot> GetAllSpotsWithLocation();
        List<Spot> GetAllSpotsWithoutLocation();
        Spot GetSpotById(int id);
        void DeleteSpot(int id);
    }
}