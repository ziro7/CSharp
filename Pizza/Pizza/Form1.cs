using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza
{
	enum PizzaType
	{
		ShrimpTuna,
		Pepperoni
	}

	public partial class Pizza : Form
	{
		//declaring af variabler/constanter
		const double SHRIMPTUNAPRICE = 64;
		const double PEPPERONIPRICE = 59;

		IList<double> pizzaPrice = new List<double>() { SHRIMPTUNAPRICE, PEPPERONIPRICE };

		int normalShrimpTunaCount = 0;
		int familyShrimpTunaCount = 0;

		double Onion = 5;
		double Shrimp = 10;
		double Tuna = 7;

		Boolean addingOnion = false;
		Boolean addingShrimp = false;
		Boolean addingTuna = false;

		double ekstraIngrediensePriceTuna = 0;

		int normalPepperoniCount = 0;
		int familyPepperoniCount = 0;

		double pepperoni = 8;
		double champion = 11;
		double cheese = 6;

		Boolean addingPepperoni = false;
		Boolean addingChampion = false;
		Boolean addingCheese = false;

		double ekstraIngrediensePricePepperoni = 0;

		double shrimpTunaPriceTotal;
		double pepperoniPriceTotal;

		double subTotalTuna;
		double subTotalPepperoni;

		double total;

		//Calorie beregning
		Dictionary<PizzaType, int> calcCount = new Dictionary<PizzaType, int>() { { PizzaType.ShrimpTuna, 231 }, { PizzaType.Pepperoni, 253 } };
		int slicesTuna = 1;
		int slicesPepperoni = 1;

		private double CalcPrSlice(int slices, Boolean isFamilySize, PizzaType pizza)
		{
			var CalcNormalPrSlice = calcCount[pizza] / slices;
			var result = NormalOrFamilyCalc(isFamilySize, CalcNormalPrSlice);
			return result;
		}

		private double NormalOrFamilyCalc(Boolean isFamilySize, double CalcNormalPrSlice)
		{
			var result = isFamilySize ? CalcNormalPrSlice * 1.5 : CalcNormalPrSlice;
			return result;
		}

		//Bestillingstid + madlavningstid
		double cookingTime = 4;
		
		
		public DateTime GetFinishTime()
		{
			DateTime CurrentTime = DateTime.Now;
			DateTime FinistTime = CurrentTime.AddMinutes(cookingTime);
			return FinistTime;
		}

		public Pizza()
		{
			InitializeComponent();
			textBox1.Enabled = false;
			textBox2.Enabled = false;
			textBox3.Enabled = false;
			textBox4.Enabled = false;
		}

		private void button3_Click(object sender, EventArgs e) //Beregn knap
		{
			try
			{
				//Giver boolean værdi true hvis den er checked.
				addingOnion = (checkBox1.Checked == true) ? true : false;
				addingShrimp = (checkBox2.Checked == true) ? true : false;
				addingTuna = (checkBox3.Checked == true) ? true : false;
				addingPepperoni = (checkBox4.Checked == true) ? true : false;
				addingChampion = (checkBox5.Checked == true) ? true : false;
				addingCheese = (checkBox6.Checked == true) ? true : false;

				//Finder tilvalgsprisen
				if (addingOnion) ekstraIngrediensePriceTuna += Onion;
				if (addingShrimp) ekstraIngrediensePriceTuna += Shrimp;
				if (addingTuna) ekstraIngrediensePriceTuna += Tuna;
				if (addingPepperoni) ekstraIngrediensePricePepperoni += pepperoni;
				if (addingChampion) ekstraIngrediensePricePepperoni += champion;
				if (addingCheese) ekstraIngrediensePricePepperoni += cheese;

				//Giver Antal Tuna pizzaer værdi
				if (textBox1.Enabled)
				{
					normalShrimpTunaCount = Convert.ToInt32(textBox1.Text);
				}
				if (textBox2.Enabled)
				{
					familyShrimpTunaCount = Convert.ToInt32(textBox2.Text);
				}

				//Giver Antal Pepperoni pizzaer værdi
				if(textBox3.Enabled)
				{
					normalPepperoniCount = Convert.ToInt32(textBox3.Text);
				}
				if(textBox4.Enabled)
				{
					familyPepperoniCount = Convert.ToInt32(textBox4.Text);
				}
				
				//Beregning af subtotaler
				shrimpTunaPriceTotal = pizzaPrice[0] + ekstraIngrediensePriceTuna;
				pepperoniPriceTotal = pizzaPrice[1] + ekstraIngrediensePricePepperoni;
				ekstraIngrediensePriceTuna = 0;
				ekstraIngrediensePricePepperoni = 0;

				subTotalTuna = normalShrimpTunaCount * shrimpTunaPriceTotal + familyShrimpTunaCount * shrimpTunaPriceTotal * 1.5;
				label12.Text = "" + subTotalTuna + " kr.";
				subTotalPepperoni = normalPepperoniCount * pepperoniPriceTotal + familyPepperoniCount * pepperoniPriceTotal * 1.5;
				label11.Text = "" + subTotalPepperoni + " kr.";

				//Skriv totalen til skærmen
				total = subTotalTuna + subTotalPepperoni;
				label10.Text = "" + total + " kr.";

				//Antal slices får værdi
				slicesTuna = Convert.ToInt32(textBox5.Text);
				slicesPepperoni = Convert.ToInt32(textBox6.Text);

				// Kalorie forbrug
				if (normalShrimpTunaCount > 0)
				{
					label15.Text = "" + CalcPrSlice(slicesTuna, false, PizzaType.ShrimpTuna);
				}
				if(familyShrimpTunaCount > 0)
				{
					label28.Text = "" + CalcPrSlice(slicesTuna, true, PizzaType.ShrimpTuna);
				}
				if(normalPepperoniCount > 0)
				{
					label16.Text = "" + CalcPrSlice(slicesPepperoni, false, PizzaType.Pepperoni);
				}
				if(familyPepperoniCount> 0)
				{
					label31.Text = "" + CalcPrSlice(slicesPepperoni, true, PizzaType.Pepperoni);
				}

			}
			catch (DivideByZeroException)
			{
				MessageBox.Show("En pizza kan ikke deles i 0 stykker", "Fejl i antal stykker", MessageBoxButtons.OK);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Formularen er ikke udfyldt korrekt", "Fejl formular", MessageBoxButtons.OK);
			}


		}


		private void checkBox7_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox7.Checked)
			{
				textBox1.Enabled = true;
			} else
			{
				textBox1.Enabled = false;
				textBox1.Text = "";
				normalShrimpTunaCount = 0;
				label15.Text = "";
			}
		}

		private void checkBox8_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox8.Checked)
			{
				textBox2.Enabled = true;
			}
			else
			{
				textBox2.Enabled = false;
				textBox2.Text = "";
				familyShrimpTunaCount = 0;
				label28.Text = "";
			}
		}

		private void checkBox9_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox9.Checked)
			{
				textBox3.Enabled = true;
			}
			else
			{
				textBox3.Enabled = false;
				textBox3.Text = "";
				normalPepperoniCount = 0;
				label16.Text = "";
			}
		}

		private void checkBox10_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox10.Checked)
			{
				textBox4.Enabled = true;
			}
			else
			{
				textBox4.Enabled = false;
				textBox4.Text = "";
				familyPepperoniCount = 0;
				label31.Text = "";
			}
		}

		public void Reset()
		{
			textBox1.Text = "";
			textBox1.Enabled = false;
			textBox2.Text = "";
			textBox2.Enabled = false;
			textBox3.Text = "";
			textBox3.Enabled = false;
			textBox4.Text = "";
			textBox4.Enabled = false;
			checkBox1.Checked = false;
			checkBox2.Checked = false;
			checkBox3.Checked = false;
			checkBox4.Checked = false;
			checkBox5.Checked = false;
			checkBox6.Checked = false;
			checkBox7.Checked = false;
			checkBox8.Checked = false;
			checkBox9.Checked = false;
			checkBox10.Checked = false;

			normalShrimpTunaCount = 0;
			familyShrimpTunaCount = 0;
						
			addingOnion = false;
			addingShrimp = false;
			addingTuna = false;

			ekstraIngrediensePriceTuna = 0;

			normalPepperoniCount = 0;
			familyPepperoniCount = 0;
						
			addingPepperoni = false;
			addingChampion = false;
			addingCheese = false;

			ekstraIngrediensePricePepperoni = 0;

			shrimpTunaPriceTotal=0;
			pepperoniPriceTotal=0;

			subTotalTuna = 0;
			subTotalPepperoni = 0;

			total = 0;
			label19.Text = ""; 
			label21.Text = "";
			label15.Text = "";
			label28.Text = "";
			label16.Text = "";
			label31.Text = "";
			label10.Text = "";
			label11.Text = "";
			label12.Text = "";
		}

		private void button2_Click(object sender, EventArgs e) //Cancel knap.
		{
			Reset();

		}

		int sendOrder = 1;

		private void button1_Click(object sender, EventArgs e) //Bestil Knap
		{
			if (total > 0)
			{
				sendOrder++;

				Reset();

				MessageBox.Show("Din bestilling er gennemført og forventes klar " + GetFinishTime() + 
								"\nDit bestillingsnummer er: " + sendOrder, "Bestilling Gennemført", MessageBoxButtons.OK);
			}
		}
	}
}

