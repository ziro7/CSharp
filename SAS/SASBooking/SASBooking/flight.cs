using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SASBooking
{
	class Flight
	{
		// Assumes that a Flight has 12 seats and it can't change (could create a flighttype subclass etc - but this will do)
		private List<string> _availableSeats = new List<string>(){"1a", "1b", "1c", "2a", "2b", "2c", "3a", "3b", "3c","4a", "4b", "4c" };
		// Initializing concurrentDictionaries with an initial capacity of 12 and a concurreny Level of 6 (number of threads that can acces at the same time)
		// First is used to hold reserved tickets
		private ConcurrentDictionary<string,Customer> _reservedSeats = new ConcurrentDictionary<string,Customer>(6,12);
		// Second is used to hold the bought tickets
		private ConcurrentDictionary<string,Customer> _boughtTickets = new ConcurrentDictionary<string,Customer>(6,12);
		private readonly DateTime _date;
		private Destination _travelFrom;
		private Destination _destination;
		private int reserveTime = 50000;

		// Creating a lock to make sure only one thread can acces the elements inside the {} of the lock
		private static object seatLock = new object();
		
		public Flight(Destination flightFrom, Destination toDestination, DateTime date)
		{
			this._travelFrom = flightFrom;
			this._destination = toDestination;
			this._date = date;
		}

		// Method to buy a ticket 
		// This one is made with lock as _available seats are not threadsafe and a block is needed.
		public void BuyingAFlight(int numberOfSeats, Customer customer)
		{
			// _availableSeats are not threadsafe, so in order to make sure no 2 threads book same seat
			// a lock is placed before accessing the array 
			lock (seatLock)
			{
				int seatsToReserve = GetNumberOfSeatsThatCanBeBooked(numberOfSeats);
				StringBuilder seatInfoStringBuilder = new StringBuilder("The following seats was bought: ");

				for (int i = 0; i < seatsToReserve; i++)
				{
					string seat = _availableSeats[_availableSeats.Count-1];
					_availableSeats.Remove(seat);
					_boughtTickets.TryAdd(seat, customer);
					seatInfoStringBuilder.Append(seat + " ");
				}
				// Removes a whitespace
				seatInfoStringBuilder.Remove(seatInfoStringBuilder.Length - 1, 1);
				// Prints string builder
				Console.WriteLine(seatInfoStringBuilder);

			}

		}

		// Helper method to get seats as it will be used in reserving tickets aswell
		private int GetNumberOfSeatsThatCanBeBooked(int numberOfSeats)
		{
			Boolean IsSeatsAvailable = (_availableSeats.Count >= numberOfSeats);
			var seatsToReserve = (IsSeatsAvailable) ? numberOfSeats : _availableSeats.Count;
			return seatsToReserve;
		}

		// Method to reserve a ticket 
		public async void ReservingAFlight(int numberOfSeats, Customer customer)
		{
			List<string> _seatsReserved = new List<string>();

			// Using same lock as in buying as both method try to acces _availableSeats
			lock (seatLock)
			{
				int seatsToReserve = GetNumberOfSeatsThatCanBeBooked(numberOfSeats);
				StringBuilder seatInfoStringBuilder = new StringBuilder("The following seats was reserved: ");

				for (int i = 0; i < seatsToReserve; i++)
				{
					string seat = _availableSeats[_availableSeats.Count - 1];
					_availableSeats.Remove(seat);
					_seatsReserved.Add(seat);
					_reservedSeats.TryAdd(seat, customer);
					seatInfoStringBuilder.Append(seat + " ");
				}

				// Removes a whitespace
				seatInfoStringBuilder.Remove(seatInfoStringBuilder.Length - 1, 1);
				// Prints string builder
				Console.WriteLine(seatInfoStringBuilder);
			}

			// Above is same as in bought above - but here i wait for a set wait time
			await Task.Delay(reserveTime); 
			
			// When wait is over bought tickets is viewed to see if it have the reserved seat
			// if not the seat was not bought and it is returned to the available list.
			foreach (var seat in _seatsReserved)
			{
				if (!_boughtTickets.ContainsKey(seat))
				{
					// Not storing the out parameter as i don't have a use for it here.
					_reservedSeats.TryRemove(seat, out customer);
					_availableSeats.Add(seat);
				}
			}
		}
		
		// Method to buy a reserved ticket 
		// This one is made with async keyword as this is threadsafe and there is no need
		// to block or similar (and to test it) 
		// In order to create some thing that takes a while i set printing tickets takes 5 sec.
		public async void BuyingReservedTickets(int numberOfSeats, Customer customer)
		{
			// Creates a counter to make sure it is known how many seat was bought
			int count = 0;
			// Create stream to write to file

			// Iterate over the reserved seats dictionary 
			foreach (KeyValuePair<string, Customer> seat in _reservedSeats)
			{
				// if the count is equal to the number of required seats i stop looking for more
				if (count == numberOfSeats)
				{
					return;
				}

				// If the value in the dictionay equals the customer who reserved the item
				// and now wanna buy i try to remove it.
				// if succesfull i add it to the boughtticket dictionary and increase count.
				if (seat.Value == customer)
				{
					if (_reservedSeats.TryRemove(seat.Key, out customer))
					{
						_boughtTickets.TryAdd(seat.Key, customer);
						count++;
						try
						{
							await PrintingTicketAsync(seat);
						}
						catch(Exception ex)
						{
							Console.WriteLine("Exception Message: " + ex.Message);
						}
					}
				}
			}

			Console.WriteLine("A total of " + count + " tickets was bought.");
		}

		private static async Task PrintingTicketAsync(KeyValuePair<string, Customer> seat)
		{
			int numberOfRetries = 3;
			int DelayOnRetry = 1000;

			for (int i = 0; i < numberOfRetries; i++)
			{
				try
				{
					using (StreamWriter streamWriter = new StreamWriter("Tickets.txt", true))
					{
						await Task.Delay(5000);
						await streamWriter.WriteAsync("-------Printing ticket for seat " + seat.Key);
						return;
					}
				}
				catch (IOException e) when (i <= numberOfRetries)
				{
					Thread.Sleep(DelayOnRetry);
				}
			}

		}


		// Override ToString to create a more meaningfull info if .ToString is called on it so get info on a Flight.
		public override string ToString()
		{
			return string.Format("\n - Flight from {0} to {1} at {2}",_travelFrom, _destination,_date);
		}
	}
}
