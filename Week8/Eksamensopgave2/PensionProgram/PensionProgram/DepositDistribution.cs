using System;

namespace PensionProgram
{
	public class DepositDistribution
	{
		public bool isThereRate { get; set; }
		public double maxOnRate { get; set; }
		
		public DepositDistribution(bool isThereRate)
		{
			this.isThereRate = isThereRate;
		}

		public DepositDistribution(double maxOnRate)
		{
			this.isThereRate = true;
			this.maxOnRate = maxOnRate;
		}

		public void AddRatePensionToScheme(double maxOnRate)
		{
			this.isThereRate = true;
			this.maxOnRate = maxOnRate;
		}

		public void RemoveRatePensionFromScheme()
		{
			if (isThereRate)
			{
				this.isThereRate = false;
			}
		}

		public double ValueLeftOnRate()
		{
			var valueLeftOnRate = 0;
			//skal have samlet præmie i år - maxOnRate
			return valueLeftOnRate;
		}

		/*
		 * //mangler lidt
			Indbetalingsfordeling
			Andel på liv
			Andel på rate
			Konstrukter (bool isThereRate)
			Overload konstrukter (max på rate) (Antager at der er rate)
			-Metode: Tilføj opsparing til stack(skal også oprette instantiere depot med den skatekode)
			-Metode: Ret indbetalingsfordeling 
		 */
	}
}
