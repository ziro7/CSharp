using System;
using System.Collections.Generic;
using System.Linq;

namespace SASBooking
{
	class Sas
	{
		private List<travel> _travels = new List<travel>();
		private List<customer> _customers = new List<customer>();

		public Sas()
		{

		}

		public void AddingTravelToCatalog(travel travelToAdd)
		{
			_travels.Add(travelToAdd);
		}

		public void CreateCustomer (params customer[] customers)
		{
			foreach (var customer in customers)
			{
				_customers.Add(customer);
			}
		}

		public void BuyATicket(travel travelToBuy, int numberOfSeats, customer customer)
		{
			travelToBuy.BuyingTravel(numberOfSeats,customer);
			Console.WriteLine("BuyingATicket To " + travelToBuy.NameOfTravel.ToString());
			Console.WriteLine(travelToBuy.ToString());
		}

		public void ReserveATicket()
		{

		}

		public void BuyAReservedTicket()
		{

		}


	}



}
