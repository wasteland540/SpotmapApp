using System.Collections.Generic;
using System.Linq;
using Spotmap.Core.Services.Events;
using Spotmap.Webservice;
using SpotmapModel.Models;

namespace Spotmap.Core.Services
{
    public class WebserviceService : IWebserviceService
    {
        private readonly SpotShareClient _webserviceProxy;

        public WebserviceService()
        {
            //_webserviceProxy = new SpotShareClient();
            //TODO: configurierbar machen...
            _webserviceProxy = new SpotShareClient(new SpotShareClient.EndpointConfiguration(), "http://senseless-skate.de:8080/SpotmapWS/SpotmapWS");

            _webserviceProxy.shareSpotCompleted += webserviceProxy_shareSpotCompleted;
            _webserviceProxy.getSharedSpotCompleted += webserviceProxy_getSharedSpotCompleted;

            _webserviceProxy.shareSpotsCompleted += webserviceProxy_shareSpotsCompleted;
            _webserviceProxy.getSharedSpotsCompleted += webserviceProxy_getSharedSpotsCompleted;
        }
        
        public event ShareSpotResponseCompleted ShareSpotResponseCompleted;
        public event ShareSpotsResponseCompleted ShareSpotsResponseCompleted;
        public event GetShareSpotResponseCompleted GetShareSpotResponseCompleted;
        public event GetShareSpotsResponseCompleted GetShareSpotsResponseCompleted;

        public void ShareSpotAsync(Spot spot)
        {
            spot wsSpot = MapSpotToWsSpot(spot);

            //TODO: if the webservice isn't running, it will throw a TimeoutException after 60 seconds 
            _webserviceProxy.shareSpotAsync(wsSpot);
        }

        public void ShareSpotsAsync(List<Spot> spots)
        {
            var wsSpots = new spot[spots.Count()];

            for (int i = 0; i < spots.Count(); i++)
            {
                wsSpots[i] = MapSpotToWsSpot(spots[i]);
            }

            //TODO: if the webservice isn't running, it will throw a TimeoutException after 60 seconds 
            _webserviceProxy.shareSpotsAsync(wsSpots);
        }

        public void GetSharedSpotAsync(string key)
        {
            _webserviceProxy.getSharedSpotAsync(key);
        }

        public void GetSharedSpotsAsync(string key)
        {
            _webserviceProxy.getSharedSpotsAsync(key);
        }

        public void Dispose()
        {
            _webserviceProxy.shareSpotCompleted -= webserviceProxy_shareSpotCompleted;
            _webserviceProxy.getSharedSpotCompleted -= webserviceProxy_getSharedSpotCompleted;
        }

        protected virtual void OnShareSpotResponseCompleted(ShareSpotResponseCompletedArgs args)
        {
            if (ShareSpotResponseCompleted != null) ShareSpotResponseCompleted(this, args);
        }

        protected virtual void OnShareSpotsResponseCompleted(ShareSpotsResponseCompletedArgs args)
        {
            if (ShareSpotsResponseCompleted != null) ShareSpotsResponseCompleted(this, args);
        }

        protected virtual void OnGetShareSpotResponseCompleted(GetShareSpotResponseCompletedArgs args)
        {
            if (GetShareSpotResponseCompleted != null) GetShareSpotResponseCompleted(this, args);
        }

        protected virtual void OnGetShareSpotsResponseCompleted(GetShareSpotsResponseCompletedArgs args)
        {
            if (GetShareSpotsResponseCompleted != null) GetShareSpotsResponseCompleted(this, args);
        }

        private void webserviceProxy_shareSpotCompleted(object sender, shareSpotCompletedEventArgs e)
        {
            string spotKey = e.Result;

            OnShareSpotResponseCompleted(new ShareSpotResponseCompletedArgs
            {
                SpotKey = spotKey
            });
        }

        private void webserviceProxy_getSharedSpotCompleted(object sender, getSharedSpotCompletedEventArgs e)
        {
            spot wsSpot = e.Result;

            Spot spot = MapWsSpotToSpot(wsSpot);

            OnGetShareSpotResponseCompleted(new GetShareSpotResponseCompletedArgs
            {
                Spot = spot
            });
        }

        private void webserviceProxy_shareSpotsCompleted(object sender, shareSpotsCompletedEventArgs e)
        {
            string spotKey = e.Result;

            OnShareSpotsResponseCompleted(new ShareSpotsResponseCompletedArgs
            {
                SpotsKey = spotKey
            });
        }

        private void webserviceProxy_getSharedSpotsCompleted(object sender, getSharedSpotsCompletedEventArgs e)
        {
            spot[] wsSpots = e.Result;

            var spots = new List<Spot>();
            foreach (spot wsSpot in wsSpots)
            {
                var spot = MapWsSpotToSpot(wsSpot);
                spots.Add(spot);
            }

            OnGetShareSpotsResponseCompleted(new GetShareSpotsResponseCompletedArgs
            {
                Spots = spots
            });
        }

        private spot MapSpotToWsSpot(Spot spot)
        {
            var wsSpot = new spot
            {
                description = spot.Description,
                lat = spot.Lat,
                lng = spot.Lng,
                locationKown = spot.LocationKown,
                name = spot.Name,
                pictureBytes = spot.PictureBytes
            };

            return wsSpot;
        }

        private Spot MapWsSpotToSpot(spot wsSpot)
        {
            var spot = new Spot
            {
                Description = wsSpot.description,
                Lat = wsSpot.lat,
                Lng = wsSpot.lng,
                LocationKown = wsSpot.locationKown,
                Name = wsSpot.name,
                PictureBytes = wsSpot.pictureBytes
            };

            return spot;
        }
    }
}