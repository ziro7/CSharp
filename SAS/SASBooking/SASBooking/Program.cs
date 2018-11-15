using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SASBooking
{
	class Program
	{
		static void Main(string[] args)
		{
			// Sas Main class
			Sas sas = new Sas();

			// Flight to and from Oslo
			Flight copenhagenToOslo = new Flight(Destination.Copenhagen,Destination.Oslo,new DateTime(2018,12,10,8,30,00));
			Flight osloToCopenhagen = new Flight(Destination.Oslo, Destination.Copenhagen, new DateTime(2018, 12, 12, 8, 30, 00));
			// Travel combining the two flights
			Travel copenhagenOsloRoundTrip = new Travel("copenhagenOsloRoundTrip", 1000, copenhagenToOslo, osloToCopenhagen);
			// adding to Sas
			sas.AddingTravelToCatalog(copenhagenOsloRoundTrip);

			// Flight to and from Aarhus
			Flight copenhagenToAarhus = new Flight(Destination.Copenhagen, Destination.Aarhus, new DateTime(2018, 12, 16, 8, 30, 00));
			Flight aarhusToCopenhagen = new Flight(Destination.Aarhus, Destination.Copenhagen, new DateTime(2018, 12, 18, 8, 30, 00));
			// Travel combining the two flights
			Travel copenhagenAarhusRoundTrip = new Travel("copenhagenAarhusRoundTrip", 500, copenhagenToAarhus, aarhusToCopenhagen);
			// adding to Sas
			sas.AddingTravelToCatalog(copenhagenAarhusRoundTrip);

			// Flight to and from Hong Kong with transit in Beijing
			Flight copenhagenToBeijing = new Flight(Destination.Copenhagen, Destination.Beijing, new DateTime(2018, 11, 15, 8, 30, 00));
			Flight beijingToHongKong = new Flight(Destination.Beijing, Destination.HongKong, new DateTime(2018, 11, 15, 23, 30, 00));
			Flight hongKongToBeijing = new Flight(Destination.HongKong, Destination.Beijing, new DateTime(2018, 12, 01, 8, 30, 00));
			Flight beijingToCopenhagen = new Flight(Destination.Beijing, Destination.Copenhagen, new DateTime(2018, 12, 01, 12, 30, 00));
			// Travel combining the four flights
			Travel copenhagenHongKongRoundTrip = new Travel("copenhagenHongKongRoundTrip", 4995, copenhagenToBeijing,beijingToHongKong,hongKongToBeijing,beijingToCopenhagen);
			// adding to Sas
			sas.AddingTravelToCatalog(copenhagenHongKongRoundTrip);

			// Creating a customer and add it to SAS customers
			Customer familienJensen = new Customer("familienJensen");
			sas.CreateCustomer(familienJensen);

			#region Testing XML

			// I create a Xml file in which I write a custom object (only one I have so far) 
			// the Serializer writes the data as XML 
			// here a customer that was created in the program.cs
			//XmlSerializer serializer = new XmlSerializer(typeof(Customer));
			//using (TextWriter customerFromProgram = new StreamWriter(@"D:\customer.xml"))
			//{
			//	serializer.Serialize(customerFromProgram, familienJensen);
			//}

			//  To get the customer from the XML file – the following deserialization is used.
			//XmlSerializer deserializer = new XmlSerializer(typeof(Customer));
			//TextReader reader = new StreamReader(@"D:\customer.xml");
			//object obj = deserializer.Deserialize(reader);
			//Customer familienJensen2 = (Customer)obj;
			//reader.Close();

			//Console.WriteLine(familienJensen2.ToString());

			//List<Customer> customers = new List<Customer>()
			//{
			//	new Customer("familienHansen"),
			//	new Customer("familienFlemmingsen"),
			//	new Customer("familienGunnarsen")
			//};

			//Stream fStream = new FileStream(@"D:\customersFromProgram.xml",FileMode.Create,FileAccess.Write,FileShare.None);
			//using (fStream)
			//{
			//	XmlSerializer serializer2 = new XmlSerializer(typeof(List<Customer>));
			//	serializer2.Serialize(fStream,customers);
			//}

			//customers = null;


			#endregion

			// I have created an XML file that holds more customers, which I here 
			// read to the system and store it in my list of customers.

			// First I read from the file and store it in a list.
			XmlSerializer serializer3 = new XmlSerializer(typeof(List<Customer>));
			List<Customer> customersFromXml;

			using (FileStream fs2 = File.OpenRead(@"D:\customersFromXml.xml"))
			{
				customersFromXml = (List<Customer>)serializer3.Deserialize(fs2);
			}

			// As my createCustomer can accept an array of customers i add it to the method (converting it to array in the process)
			sas.CreateCustomer(customersFromXml.ToArray());

			sas.ViewCustomers();

			// familienJensen books a Flight for 4 to Oslo
			sas.BuyATicket(copenhagenOsloRoundTrip, 4, familienJensen);
			Console.WriteLine();

			// familienHansen reserve a flight to Oslo
			Customer familienHansen = sas.Customers.Find(x => x.Name == "familienHansen");
			sas.ReserveATicket(copenhagenOsloRoundTrip,9,familienHansen);

			sas.BuyAReservedTicket(copenhagenOsloRoundTrip,9,familienHansen);
			Console.ReadLine();
		}
	}
}
