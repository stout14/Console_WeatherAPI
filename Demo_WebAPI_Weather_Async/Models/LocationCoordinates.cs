using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_WebAPI_Weather
{
    public struct LocationCoordinates
    {
        //public double Latitude { get; set; }
        //public double Longitude { get; set; }
        public int Zip{get; set;}

        public LocationCoordinates(int zip) //double latitude, double longitude)
        {
            this.Zip = zip;
            //this.Latitude = latitude;
            //this.Longitude = longitude;
        }
    }
}
