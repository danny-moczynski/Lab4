using System.Collections.ObjectModel;
using Lab2;

namespace UnitTest
{
    public class UnitTest1
    {
        BusinessLogic businessLogic = new BusinessLogic();

        [Fact]
        public void AddAirportTest()
        {
            // Adding an airport to the database
            ObservableCollection<Airport> airports = businessLogic.GetAirports();
            businessLogic.AddAirport("KATW", "Appleton", DateTime.Now, 4);
            Assert.NotNull(businessLogic.FindAirport("KATW"));
        }

        [Fact]
        public void UpdateAirportTest()
        {
            businessLogic.AddAirport("KFDL", "Fond U Lac", new DateTime(2023, 2, 12), 5);
            bool updatedAirport = businessLogic.EditAirport("KFDL", "Fondy", new DateTime(2023, 3, 12), 4);
            Assert.True(updatedAirport);
            Airport airport = businessLogic.FindAirport("KFDL");
            Assert.NotNull(airport);
        }

        [Fact]
        public void UpdateNonExistantAirportTest()
        {
            businessLogic.AddAirport("KAAS", "Lomira", DateTime.Now, 3);
            bool updatedAirport = businessLogic.EditAirport("KAAA", "Lomira", DateTime.Now, 3);
            Assert.False(updatedAirport);
            Airport airport = businessLogic.FindAirport("KAAA");
            Assert.Null(airport);
            
        }


        [Fact]
        public void DeleteAirportTest()
        {
            // Check the id of the airport and delete it
            bool result = businessLogic.AddAirport("KLMR", "Lomira", new DateTime(2023, 4, 20), 3);
            bool deletedAirport = businessLogic.DeleteAirport("KFDL");
            Assert.True(deletedAirport);
            Airport deleteNewAirport = businessLogic.FindAirport("KFDL");
            Assert.Null(deleteNewAirport);
        }

        [Fact]
        public void GetAllAirports()
        {
            ObservableCollection<Airport> airports = businessLogic.GetAirports();
            Assert.NotNull(airports);
        }

        // Create an airport edge test case that passes in the airport parameters
        private void AirportEdgeTestCase(string airportId, string airportCity, DateTime dateVisited, int ratingCount)
        {
            businessLogic.AddAirport(airportId, airportCity, dateVisited, ratingCount);
            Assert.Null(businessLogic.FindAirport(airportId));
        }

        [Fact]
        public void AddAirportEdgeTestCase1()
        {
            // Invalid rating
            AirportEdgeTestCase("KATT", "Montucky", new DateTime(2023, 7, 24), 7);
        }

        [Fact]
        public void AddAirportEdgeTestCase2()
        {
            // The Id does not start with a K
            AirportEdgeTestCase("LMRM", "Lomira", new DateTime(2023, 2, 10), 4);
        }

        [Fact]
        public void AddAirportEdgeTestCase3()
        {
            // Duplicate Id
            Assert.False(businessLogic.AddAirport("KATW", "Appleton", DateTime.Now, 4));
        }

        [Fact]
        public void AddAirportEdgeTestCase4()
        {
            // This airport does not exist
            Assert.False(businessLogic.DeleteAirport("KYYE"));
        }

        [Fact]
        public void CheckAirportDateTimeCase()
        {
            // Date is not valid
            AirportEdgeTestCase("KTTW", "Kaukauna", new DateTime(2024, 2, 12), 5);
        }

        [Fact]
        public void CheckAirportCityCase()
        {
            // City is too long
            AirportEdgeTestCase("KUIU", "OshkoshAppletonCombinedAsOneCity ", new DateTime(2023, 9, 12), 1);
        }
    }
}
