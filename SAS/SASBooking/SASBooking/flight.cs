using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SASBooking
{
	class flight
	{
		// Assumes that a flight has 12 seats and it can't change (could create a flighttype subclass etc - but this will do)
		private List<string> _availableSeats = new List<string>(){"1a", "1b", "1c", "2a", "2b", "2c", "3a", "3b", "3c","4a", "4b", "4c" };
		// Initializing two concurrentDictionary with an initial capacity of 12 and a concurreny Level of 6 (number of threads that can acces at the same time)
		// First is for reserved tickets - the other is for boughtTickets
		private ConcurrentDictionary<string,customer> _reservedSeats = new ConcurrentDictionary<string,customer>(6,12);
		private Dictionary<string,customer> _boughtTickets = new Dictionary<string, customer>();
		private readonly DateTime _date;
		private string _travelFrom;
		private string _destination;
		private int reserveTime = 50000;

		// Creating a lock to make sure only one thread can acces the elements inside the {} of the lock
		private static object seatLock = new object();
		
		public flight(string flightFrom, string toDestination, DateTime date)
		{
			this._travelFrom = flightFrom;
			this._destination = toDestination;
			this._date = date;
		}

		// Method to buy a ticket 
		// This one is made with lock as _available seats are not threadsafe and a block is needed.
		public void BuyingAFlight(int numberOfSeats, customer customer)
		{
			// _availableSeats are not threadsafe, so in order to make sure no 2 threads book same seat
			// a lock is placed before accessing the array 
			lock (seatLock)
			{
				if (_availableSeats.Count < numberOfSeats)
				{
					Console.WriteLine("Not all seats was available - Bought: " + _availableSeats.Count);
					for (int i = _availableSeats.Count-1; i > 0; i--)
					{
						string seat = _availableSeats[i];
						_availableSeats.Remove(seat);
						_boughtTickets.Add(seat,customer);
						Console.WriteLine("The following seats was bought: " + seat);
					}
				}
				else
				{
					for (int i = numberOfSeats; i > 0; i--)
					{
						string seat = _availableSeats[_availableSeats.Count-1];
						_availableSeats.Remove(seat);
						_boughtTickets.Add(seat, customer);
						Console.WriteLine("The following seats was bought: " + seat);
					}
				}

				
			}

		}

		// Method to reserve a ticket 
		public void ReservingAFlight(int numberOfSeats, customer customer)
		{
			lock (seatLock)
			{
				List<string> seatsReserved = new List<string>();
				for (int i = 0; i < numberOfSeats; i++)
				{
					var reservedSeat = _availableSeats.TakeLast(1);
					string seat = reservedSeat.ToString();
					seatsReserved.Add(seat);
					_reservedSeats.TryAdd(seat, customer);

				}
				Console.WriteLine("The following seats was bought:" + seatsReserved.ToString());

				// Above is same as in bought above - but here i wait for a set wait time
				Thread.Sleep(reserveTime);

				// When wait is over bought tickets is viewed to see if it have the reserved seat
				// if not the seat was not bought and it is returned to the available list.
				foreach (var seat in seatsReserved)
				{
					if (!_boughtTickets.ContainsKey(seat))
					{
						_reservedSeats.TryRemove(seat, out customer);
						_availableSeats.Add(seat);
					}
				}
			}
		}
		
		// Method to buy a reserved ticket 
		// This one is made with async keyword as this is threadsafe and there is no need
		// to block or similar (and to test it) 
		// In order to create some thing that takes a while i set printing tickets takes 5 sec.
		public async void BuyingReservedTickets(int numberOfSeats, customer customer)
		{
			// Creates a counter to make sure it is known how many seat was bought
			int count = 0;

			// Create stream to write to file
			using (StreamWriter streamWriter = new StreamWriter("Tickets.txt"))
			{
				// Iterate over the reserved seats dictionary 
				foreach (KeyValuePair<string, customer> seat in _reservedSeats)
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
							_boughtTickets.Add(seat.Key, customer);
							count++;
							await PrintingTicket(streamWriter, seat);
						}
					}
				}

				Console.WriteLine("A total of " + count + " tickets was bought.");
			}


		}

		private static Task PrintingTicket(StreamWriter streamWriter, KeyValuePair<string, customer> seat)
		{
			Thread.Sleep(5000);
			return streamWriter.WriteAsync("-------Printing ticket for seat " + seat.Key);
		}


		// Override ToString to create a more meaningfull info if .ToString is called on it so get info on a flight.
		public override string ToString()
		{
			return string.Format("\n - Flight from {0} to {1} at {2}",_travelFrom, _destination,_date);
		}
	}
}
