using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace SpotmapModel.Models
{
    public class Spot
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool LocationKown { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        //public string ImagePath { get; set; }
        public byte[] PictureBytes { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool Selected { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}