using System;
using System.Runtime.Serialization;

namespace SASBooking
{
	[Serializable()]
	public class Customer : ISerializable
	{
		public string Name { get; set; }

		public Customer(){}

		public Customer(string name)
		{
			Name = name;
		}

		// Serialization function (Stores Object Data in File)
		// SerializationInfo holds the key value pairs
		// StreamingContext can hold additional info
		// but we aren't using it here
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// Assign key value pair for your data
			info.AddValue("Name", Name);
		}

		// The deserialize function (Removes Object Data from File)
		public Customer(SerializationInfo info, StreamingContext ctxt)
		{
			//Get the values from info and assign them to the properties
			Name = (string)info.GetValue("Name", typeof(string));
		}
	}
}

