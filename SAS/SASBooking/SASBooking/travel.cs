using System.Collections.Generic;
using System.Text;

namespace SASBooking
{
	public class Travel
	{
		private List<Flight> flights = new List<Flight>();
		private decimal price;

		public string NameOfTravel { get; set; }

		//Denne metode skal sammensætte de fly der skal bruges til den samlede rejse (men pr 1 person).

		// Creating the construktur with params keyword so a Travel can have multiple flights like a return trip
		public Travel(string nameOfTravel, decimal price, params Flight[] flightsInTravel)
		{
			
			foreach (var flight in flightsInTravel)
			{
				flights.Add(flight);
			}
			this.price = price;
			this.NameOfTravel = nameOfTravel;
		}

		public void BuyingTravel(int numberOfSeats, Customer customer)
		{
			foreach (var flight in flights)
			{
				flight.ToString();
				flight.BuyingAFlight(numberOfSeats, customer);
			}
		}

		public void ReservingATravel(int numberOfSeats, Customer customer)
		{
			foreach (var flight in flights)
			{
				flight.ToString();
				flight.ReservingAFlight(numberOfSeats, customer);
			}
		}

		public async void BuyingAReservedTravel(int numberOfSeats, Customer customer)
		{
			foreach (var flight in flights)
			{
				flight.ToString();
				await flight.BuyingReservedTicketsAsync(numberOfSeats, customer);
			}
		}

		// Override ToString to create a more meaningfull info if .ToString is called on it so get info on a Flight.
		public override string ToString()
		{
			StringBuilder InfoOnTravel = new StringBuilder("Travel have the following flights: ");
			foreach (var flight in flights)
			{
				InfoOnTravel.Append(flight);
			}

			return InfoOnTravel.ToString();
		}
	}
}
