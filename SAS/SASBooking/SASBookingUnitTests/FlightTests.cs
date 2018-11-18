using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SASBooking;

namespace SASBookingUnitTests
{
	[TestClass]
	public class FlightTests
	{
		// setup
		Sas Sas = new Sas();
		Flight copenhagenToOslo = new Flight(Destination.Copenhagen, Destination.Oslo, new DateTime(2018, 12, 10, 8, 30, 00));
		Flight osloToCopenhagen = new Flight(Destination.Oslo, Destination.Copenhagen, new DateTime(2018, 12, 12, 8, 30, 00));

		[TestInitialize]
		public void Setup()
		{
			Travel copenhagenOsloRoundTrip = new Travel("copenhagenOsloRoundTrip", 1000, copenhagenToOslo, osloToCopenhagen);
			Sas.AddingTravelToCatalog(copenhagenOsloRoundTrip);

		}
		
		[TestMethod]
		public void BuyingAFlight_CustomerBuysAvaliableTickets_TicketsAreBought()
		{
			// Arrange 
			int numberOfTickets = 8;
			Customer familienJensen = new Customer("familienJensen");
			Sas.CreateCustomer(familienJensen);

			// Act
			copenhagenToOslo.BuyingAFlight(numberOfTickets,familienJensen);

			// Assert
			int boughtTickets = copenhagenToOslo.BoughtTickets.Count;
			Assert.AreEqual(numberOfTickets,boughtTickets);
		}

		[TestMethod]
		public void BuyingAFlight_NotEnoughTickets_OnlyAvailableTicketsAreBought()
		{
			// Arrange 
			int numberOfTickets = 13;
			Customer familienJensen = new Customer("familienJensen");
			Sas.CreateCustomer(familienJensen);

			// Act
			copenhagenToOslo.BuyingAFlight(numberOfTickets, familienJensen);

			// Assert
			int boughtTickets = copenhagenToOslo.BoughtTickets.Count;
			Assert.AreEqual(12, boughtTickets);
		}

		[TestMethod]
		public void ReservingAFlight_CustomerReserveSeats_SeatsAreReserved()
		{
			// Arrange 
			int numberOfTickets = 4;
			Customer familienJensen = new Customer("familienJensen");
			Sas.CreateCustomer(familienJensen);

			// Act
			copenhagenToOslo.ReservingAFlight(numberOfTickets,familienJensen);

			// Assert
			int reserevedTickets = copenhagenToOslo.ReservedSeats.Count;
			Assert.AreEqual(numberOfTickets, reserevedTickets);
		}

		[TestMethod]
		public void ReservingAFlight_CustomerTryToReserveMoreThanAvailable_OnlyAvaliableSeatsAreReserved()
		{
			// Arrange 
			int numberOfTickets = 16;
			Customer familienJensen = new Customer("familienJensen");
			Sas.CreateCustomer(familienJensen);

			// Act
			copenhagenToOslo.ReservingAFlight(numberOfTickets, familienJensen);

			// Assert
			int reserevedTickets = copenhagenToOslo.ReservedSeats.Count;
			Assert.AreEqual(12, reserevedTickets);
		}

		[TestMethod]
		public async Task BuyingReservedTicketsAsync_CustomerBuysReservedTickets_TicketsAreBought()
		{
			// Arrange 
			int numberOfTickets = 3;
			Customer familienJensen = new Customer("familienJensen");
			Sas.CreateCustomer(familienJensen);

			// Arrange reservation
			copenhagenToOslo.ReservingAFlight(numberOfTickets, familienJensen);

			// Act
			Thread.Sleep(1000); // reservations are good for 500000
			await copenhagenToOslo.BuyingReservedTicketsAsync(numberOfTickets,familienJensen);

			// Assert
			int boughtTickets = copenhagenToOslo.BoughtTickets.Count;
			Assert.AreEqual(numberOfTickets, boughtTickets);
		}

		[TestMethod]
		public async Task BuyingReservedTicketsAsync_CustomerBuysReservedTicketsTOOLate_TicketsAreNOTBought()
		{
			// Arrange 
			int numberOfTickets = 3;
			Customer familienJensen = new Customer("familienJensen");
			Sas.CreateCustomer(familienJensen);

			// Arrange reservation
			copenhagenToOslo.ReservingAFlight(numberOfTickets, familienJensen);

			// Act
			Thread.Sleep(600000); // reservations are good for 500000
			await copenhagenToOslo.BuyingReservedTicketsAsync(numberOfTickets, familienJensen);


			// Assert
			int boughtTickets = copenhagenToOslo.BoughtTickets.Count;
			Assert.AreEqual(0, boughtTickets);
		}

	}
}
