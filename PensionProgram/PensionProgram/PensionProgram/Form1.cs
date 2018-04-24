using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PensionProgram
{
	public partial class PensionCreation : Form
	{
		private string customerName;
		private string birthdate;
		private double premiumPrMaturity;
		private Maturity maturity;
		private int expirationYear;
		private int showingPensionScheme;
		private Customer customer;
		private PensionScheme pensionScheme;
		private DateTime startDate;
		private bool isthereRate;
		private int rateYear = 10;
		private int maxRateYear = 0;

		public PensionCreation()
		{
			InitializeComponent();
			maturityComboBox.DataSource = Enum.GetValues(typeof(Maturity));
		}

		private void button1_Click(object sender, EventArgs e) //opret ordning
		{
			try
			{
				customerName = Convert.ToString(customerNameTextBox.Text);
				birthdate = Convert.ToString(FødselsDatoTextBox.Text);
				premiumPrMaturity = Convert.ToInt32(premiumTextBox.Text);
				maturity = (Maturity)maturityComboBox.SelectedItem;
				expirationYear = Convert.ToInt32(expiryTextBox.Text);
				startDate = startDateSelector.Value;
				isthereRate = (isThereRatePensionCheckBox.Checked == true) ? true : false;
				if (isthereRate)
				{
					rateYear = Convert.ToInt32(rateYearTextBox.Text);
					maxRateYear = Convert.ToInt32(maxRateTextBox.Text);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Der er fejl i indtastningen", "Fejl i indtastning", MessageBoxButtons.OK);
			}


			//Opret Kunde
			try
			{
				customer = new Customer(customerName, birthdate);
			}
			catch (Exception)
			{
				MessageBox.Show("Der er fejl i navn eller fødselsdag", "Fejl i navn eller fødselsdag", MessageBoxButtons.OK);
				throw;
			}
			
			//Opret ordning
			try
			{
				pensionScheme = new PensionScheme(customer,
					new DepositAccount(),
					new Premium(premiumPrMaturity, maturity),
					expirationYear,
					startDate,
					PensionsSchemeNumber.Instance);
				CreationLabel.Text = "Ordning med nummer: " + pensionScheme.PensionsSchemeNumber + " Er oprettet.";

				if (isthereRate == true)
				{
					pensionScheme.DepositAccount.AddRatePension(maxRateYear, rateYear);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Der er fejl i pensionsinfo", "Fejl i pensionsinfo", MessageBoxButtons.OK);
				throw;
			}
			

		}

		private void SeekInfoButton_Click(object sender, EventArgs e)
		{
			//Fremsøg ordning
			ShowSchemeNumberLabel.Text = pensionScheme.PensionsSchemeNumber.ToString();
			ShowYearlyPremiumLabel.Text = pensionScheme.ShowYearlyPremium().ToString(); 
			ShowExpiryDateLabel.Text = pensionScheme.ShowExpirationDate().ToString();  
			ShowCurrentDepositLabel.Text = pensionScheme.ShowDepositAccount().ToString(); 
			ShowMaturityLabel.Text = pensionScheme.Premium.Maturity.ToString(); 
		}

		private void PremiumPaymentButton_Click(object sender, EventArgs e)
		{
			double paidOnTax2 = pensionScheme.Premium.PaymentsOnTaxCode(Taxcode.Ratepension);
			bool RateOnScheme = pensionScheme.DepositAccount.ThereIsRatePension;
			int max2 = pensionScheme.DepositAccount.Max2Amount();

			if (paidOnTax2 >= max2)
			{
				pensionScheme.DepositAccount.AddPaymentToDepot(premiumPrMaturity, Taxcode.Livrente);
				pensionScheme.Premium.AddPayment(new Payment(DateTime.Now, premiumPrMaturity, Taxcode.Livrente));
			}
			else if (paidOnTax2<max2)
			{
				if (premiumPrMaturity < (max2 - paidOnTax2))
				{
					pensionScheme.DepositAccount.AddPaymentToDepot(premiumPrMaturity, Taxcode.Ratepension);
					pensionScheme.Premium.AddPayment(new Payment(DateTime.Now, premiumPrMaturity, Taxcode.Ratepension));
				}
				else
				{
					pensionScheme.DepositAccount.AddPaymentToDepot((max2-paidOnTax2), Taxcode.Ratepension);
					pensionScheme.Premium.AddPayment(new Payment(DateTime.Now, (max2 - paidOnTax2), Taxcode.Ratepension));
					pensionScheme.DepositAccount.AddPaymentToDepot(premiumPrMaturity - (max2 - paidOnTax2), Taxcode.Ratepension);
					pensionScheme.Premium.AddPayment(new Payment(DateTime.Now, premiumPrMaturity - (max2 - paidOnTax2), Taxcode.Ratepension));
				}

			}
		}

		private void isThereRatePensionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (isThereRatePensionCheckBox.Checked)
				{
					maxRateTextBox.Enabled = true;
					rateYearTextBox.Enabled = true;
				}
				else
				{
					maxRateTextBox.Enabled = false;
					rateYearTextBox.Enabled = false;
					maxRateTextBox.Text = "";
					rateYearTextBox.Text = "";
				}
		}

		private void prognoseButton_Click(object sender, EventArgs e)
		{
			//Prognose
			double tax1Deposit = pensionScheme.DepositAccount.GetCurrentLife();
			double tax2Deposit = pensionScheme.DepositAccount.GetCurrentRate();

			int yearsToRetirement = expirationYear - (int)customer.Age();

			for (var i = 0; i < yearsToRetirement; i++)
			{
				if (pensionScheme.Premium.ShowYearlyPremium()>pensionScheme.DepositAccount.Max2Amount())
				{
					tax1Deposit += pensionScheme.Premium.ShowYearlyPremium()-pensionScheme.DepositAccount.Max2Amount() + (tax1Deposit * Prognose.prognoseRate);
					tax2Deposit += pensionScheme.DepositAccount.Max2Amount() + (tax2Deposit * Prognose.prognoseRate);
				}
				else
				{
					tax2Deposit += pensionScheme.Premium.ShowYearlyPremium() + (tax2Deposit * Prognose.prognoseRate);
				}
			}

			if (tax2Deposit > 0)
			{
				string tax2Prognosis = string.Format("{0:N2} kr.", (tax2Deposit / rateYear));
				ShowRatePrognosisLabel.Text = "" + tax2Prognosis + " Pr. år. i " + rateYear + " år.";
			}
			else
			{
				ShowRatePrognosisLabel.Text = "-";
			}

			if (tax1Deposit > 0)
			{
				string tax1Prognosis = string.Format("{0:N2} kr.", (tax1Deposit / LifePension.Omregningsfaktor));
				ShowLifePrognosisLabel.Text = "" + tax1Prognosis + " Årligt i resten af dit liv.";
			}
			else
			{
				ShowLifePrognosisLabel.Text = " ";
			}
		}
	}
}
