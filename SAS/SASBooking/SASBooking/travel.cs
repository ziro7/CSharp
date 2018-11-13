using System.Collections.Generic;
using System.Text;

namespace SASBooking
{
	class travel
	{
		private string _nameOfTravel;
		private List<flight> flights = new List<flight>();
		private decimal price;

		public string NameOfTravel { get => _nameOfTravel; set => _nameOfTravel = value; }

		//Denne metode skal sammensætte de fly der skal bruges til den samlede rejse (men pr 1 person).

		// Creating the construktur with params keyword so a travel can have multiple flights like a return trip
		public travel(string nameOfTravel, decimal price, params flight[] flightsInTravel)
		{
			
			foreach (var flight in flightsInTravel)
			{
				flight.ToString();
				flights.Add(flight);
			}
			this.price = price;
			this.NameOfTravel = nameOfTravel;
		}

		public void BuyingTravel(int numberOfSeats, customer customer)
		{
			foreach (var flight in flights)
			{
				flight.ToString();
				flight.BuyingAFlight(numberOfSeats, customer);
			}
		}

		public void ReservingATravel(int numberOfSeats, customer customer)
		{
			foreach (var flight in flights)
			{
				flight.ToString();
				flight.ReservingAFlight(numberOfSeats, customer);
			}
		}

		public void BuyingAReservedTravel(int numberOfSeats, customer customer)
		{
			foreach (var flight in flights)
			{
				flight.ToString();
				flight.BuyingReservedTickets(numberOfSeats, customer);
			}
		}

		// Override ToString to create a more meaningfull info if .ToString is called on it so get info on a flight.
		public override string ToString()
		{
			StringBuilder InfoOnTravel = new StringBuilder("Travel have the following flights: ");
			foreach (var flight in flights)
			{
				InfoOnTravel.Append(flight.ToString());
			}

			return InfoOnTravel.ToString();
		}
	}
}
