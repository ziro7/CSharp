using System;

namespace Cuboid
{
    class Program
    {
        static void Main(string[] args)
        {
			Box One = new Box();
			Box Two = new Box("asadsda", "2", "2");

			Two.BoxCalc();
		
		}
		
	}

	public class Box
	{

		double boxLength;
		double boxHeight;
		double boxWidth;

		public Box()
		{

		}

		public Box(string Length, string Height, string Width)
		{
			try
			{
				this.boxLength = Convert.ToDouble(Length);
				this.boxHeight = Convert.ToDouble(Height);
				this.boxWidth = Convert.ToDouble(Width);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}			

		}

		public void BoxCalc()
		{
			double rumfang = boxLength * boxHeight * boxWidth;
			double overflade = 2 * (boxWidth * boxHeight + boxLength * boxWidth + boxLength * boxHeight);

			Console.WriteLine("rumfang i cm: " + rumfang);
			Console.WriteLine("rumfang i m: " + rumfang / 100);
			Console.WriteLine("overfalde i cm: " + overflade);
			Console.WriteLine("overfalde i m: " + overflade / 100);
			Console.ReadLine();
		}

	}
}
