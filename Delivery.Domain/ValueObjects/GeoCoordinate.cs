using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Common;
using Delivery.Domain.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace Delivery.Domain.ValueObjects
{
    public class GeoCoordinate : ValueObject
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoCoordinate(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new InvalidPosition();
            if (longitude < -180 || longitude > 180)
                throw new InvalidPosition();

            Latitude = latitude;
            Longitude = longitude;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }

        public double DistanceTo(GeoCoordinate other)
        {
            const double EarthRadiusKm = 6371;
            double dLat = DegreesToRadians(other.Latitude - Latitude);
            double dLon = DegreesToRadians(other.Longitude - Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(Latitude)) * Math.Cos(DegreesToRadians(other.Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        private static double DegreesToRadians(double deg) => deg * (Math.PI / 180);
    }
}
