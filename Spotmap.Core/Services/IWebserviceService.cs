using System;
using System.Collections.Generic;
using Spotmap.Core.Services.Events;
using SpotmapModel.Models;

namespace Spotmap.Core.Services
{
    public interface IWebserviceService : IDisposable
    {
        void ShareSpotAsync(Spot spot);

        void ShareSpotsAsync(List<Spot> spots);

        void GetSharedSpotAsync(string key);

        void GetSharedSpotsAsync(string key);

        event ShareSpotResponseCompleted ShareSpotResponseCompleted;

        event ShareSpotsResponseCompleted ShareSpotsResponseCompleted;
        
        event GetShareSpotResponseCompleted GetShareSpotResponseCompleted;

        event GetShareSpotsResponseCompleted GetShareSpotsResponseCompleted;
    }
}