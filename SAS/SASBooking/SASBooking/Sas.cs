using System;
using System.Collections.Generic;
using System.Linq;

namespace SASBooking
{
	public class Sas
	{
		private List<Travel> _travels = new List<Travel>();
		private List<Customer> _customers = new List<Customer>();

		public Sas()
		{

		}

		public List<Customer> Customers
		{
			get { return _customers; }
		}

		public void AddingTravelToCatalog(Travel travelToAdd)
		{
			_travels.Add(travelToAdd);
		}

		public void CreateCustomer (params Customer[] customers)
		{
			foreach (var customer in customers)
			{
				_customers.Add(customer);
			}
		}

		public void ViewCustomers()
		{
			foreach (Customer customer in _customers)
			{
				Console.WriteLine(customer.Name);
			}
		}

		public void BuyATicket(Travel travelToBuy, int numberOfSeats, Customer customer)
		{
			Console.WriteLine("Processing purchase of a ticket To " + travelToBuy.NameOfTravel);
			Console.WriteLine(travelToBuy.ToString());
			try
			{
				travelToBuy.BuyingTravel(numberOfSeats, customer);
				Console.WriteLine("Purchase successful");
			} catch (Exception)
			{
				Console.WriteLine("Purchase NOT successful");
			}
		}

		public void ReserveATicket(Travel travelToBuy, int numberOfSeats, Customer customer)
		{
			Console.WriteLine("Processing reservation of a ticket To " + travelToBuy.NameOfTravel.ToString());
			Console.WriteLine(travelToBuy.ToString());
			try
			{
				travelToBuy.ReservingATravel(numberOfSeats, customer);
				Console.WriteLine("Reservation successful");
			} catch (Exception)
			{
				Console.WriteLine("Reservation NOT successful");
			}

		}

		public void BuyAReservedTicket(Travel travelToBuy, int numberOfSeats, Customer customer)
		{
			Console.WriteLine("Processing buying a reserved ticket To " + travelToBuy.NameOfTravel);
			Console.WriteLine(travelToBuy.ToString());
			try
			{
				travelToBuy.BuyingAReservedTravel(numberOfSeats, customer);
				Console.WriteLine("Purchase successful");
			} catch (Exception)
			{
				Console.WriteLine("Purchase NOT successful");
			}

		}


	}



}
