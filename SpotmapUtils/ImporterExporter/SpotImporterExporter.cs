using System.Collections.Generic;
using System.Xml.Linq;
using SpotmapModel.Models;

namespace SpotmapUtils.ImporterExporter
{
    public static class SpotImporterExporter
    {
        private const string RootElement = "Spots";
        private const string SpotElement = "Spot";
        private const string NameAttribute = "Name";
        private const string DescriptionAttribute = "Description";
        private const string LocationKownAttribute = "LocationKown";
        private const string LatAttribute = "Lat";
        private const string LngAttribute = "Lng";
        private const string PictureBytesAttribute = "PictureBytes";
        private const string CreatedAtAttribute = "CreatedAt";

        public static XDocument WriteXmlDocument(List<Spot> spotlist)
        {
            var rootElement = new XElement(RootElement);

            if (spotlist != null && spotlist.Count > 0)
            {
                foreach (Spot spot in spotlist)
                {
                    rootElement.Add(CreateSpot(spot));
                }
            }

            var document = new XDocument(rootElement);
            var declaration = new XDeclaration("1.0", "utf-8", "yes");
            document.Declaration = declaration;

            return document;
        }

        private static XElement CreateSpot(Spot spot)
        {
            var spotElement = new XElement(SpotElement);

            spotElement.Add(new XAttribute(NameAttribute, spot.Name));
            spotElement.Add(new XAttribute(DescriptionAttribute, spot.Description));
            spotElement.Add(new XAttribute(LocationKownAttribute, spot.LocationKown));
            spotElement.Add(new XAttribute(LatAttribute, spot.Lat));
            spotElement.Add(new XAttribute(LngAttribute, spot.Lng));
            spotElement.Add(new XAttribute(PictureBytesAttribute, spot.PictureBytes));
            spotElement.Add(new XAttribute(CreatedAtAttribute, spot.CreatedAt));

            return spotElement;
        }

        public static List<Spot> ReadXmlDocument(XDocument document)
        {
            List<Spot> spotlist = null;

            return spotlist;
        }
    }
}