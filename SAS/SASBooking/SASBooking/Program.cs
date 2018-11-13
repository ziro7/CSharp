using System;

namespace SASBooking
{

	class Program
	{
		static void Main(string[] args)
		{
			// Sas Main class
			Sas sas = new Sas();

			// Flight to and from Oslo
			flight copenhagenToOslo = new flight("Copenhagen","Osle",new DateTime(2018,12,10,8,30,00));
			flight osloToCopenhagen = new flight("Osle", "Copenhagen", new DateTime(2018, 12, 12, 8, 30, 00));
			// Travel combining the two flights
			travel copenhagenOsloRoundTrip = new travel("copenhagenOsloRoundTrip", 1000, copenhagenToOslo, osloToCopenhagen);
			// adding to Sas
			sas.AddingTravelToCatalog(copenhagenOsloRoundTrip);

			// Flight to and from Aarhus
			flight copenhagenToAarhus = new flight("Copenhagen", "Aarhus", new DateTime(2018, 12, 16, 8, 30, 00));
			flight aarhusToCopenhagen = new flight("Aarhus", "Copenhagen", new DateTime(2018, 12, 18, 8, 30, 00));
			// Travel combining the two flights
			travel copenhagenAarhusRoundTrip = new travel("copenhagenAarhusRoundTrip", 500, copenhagenToAarhus, aarhusToCopenhagen);
			// adding to Sas
			sas.AddingTravelToCatalog(copenhagenAarhusRoundTrip);

			// Flight to and from Hong Kong with transit in Beijing
			flight copenhagenToBeijing = new flight("Copenhagen", "Beijing", new DateTime(2018, 11, 15, 8, 30, 00));
			flight beijingToHongKong = new flight("Beijing", "HongKong", new DateTime(2018, 11, 15, 23, 30, 00));
			flight hongKongToBeijing = new flight("HongKong", "Beijing", new DateTime(2018, 12, 01, 8, 30, 00));
			flight beijingToCopenhagen = new flight("Beijing", "Copenhagen", new DateTime(2018, 12, 01, 12, 30, 00));
			// Travel combining the four flights
			travel copenhagenHongKongRoundTrip = new travel("copenhagenHongKongRoundTrip", 4995, copenhagenToBeijing,beijingToHongKong,hongKongToBeijing,beijingToCopenhagen);
			// adding to Sas
			sas.AddingTravelToCatalog(copenhagenHongKongRoundTrip);

			// Creating a few customers and adding to sas
			customer familienJensen = new customer();
			customer familienHansen = new customer();
			customer familienFlemmingsen = new customer();
			customer familienGunnarsen = new customer();
			sas.CreateCustomer(familienJensen, familienHansen,familienFlemmingsen,familienGunnarsen);

			//familienJensen books a flight for 4 to Oslo
			sas.BuyATicket(copenhagenOsloRoundTrip, 4,familienJensen);


			Console.ReadLine();
		}
	}
}
