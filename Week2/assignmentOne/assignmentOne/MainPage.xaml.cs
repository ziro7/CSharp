using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace assignmentOne
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		const double SALG_FRA_BUTIK_BESOEG_PCT = 0.2;
		const double BILLET_PRIS_GENNEMSNIT = 175.00;
		const double SALG_DRIKKEVARE_GENNEMSNIT = 70.00;
		const double SALG_SPORTS_VARE_GENNEMSNIT = 245.00;
		const double ABONNEMENT_PRIS_6_MAANEDER = 999.00;
		const double TV_RETTIGHEDER_PR_KANAL = 1000000.00;
		const double TOTAL_OMK_I_PCT = 0.64;
		
		public MainPage()
        {
			this.InitializeComponent();
        }

		private void Beregn_Click(object sender, RoutedEventArgs e)
		{

			Boolean formIsValid = true;
			if (billetterSolgt.Text == "") 
			{
				Warning1.Text = "Error in input - need value";
				formIsValid = false;
			} 
			
			if (TVkanaler.Text == "")
			{
				Warning2.Text = "Error in input - need value";
				formIsValid = false;
			} 

			if (VisitorToStore.Text == "")
			{
				Warning3.Text = "Error in input - need value";
				formIsValid = false;
			}
			
			if (MotionscenterAbonnement.Text == "")
			{
				Warning4.Text = "Error in input - need value";
				formIsValid = false;
			}
			

			if (Tilskuer.Text == "")
			{
				Warning5.Text = "Error in input - need value";
				formIsValid = false;
			}
								

			if (formIsValid)
			{
				Warning1.Text = "";
				Warning2.Text = "";
				Warning3.Text = "";
				Warning4.Text = "";
				Warning5.Text = "";
				Boolean calcOK = true;
				if (Convert.ToDouble(billetterSolgt.Text) < 0)
				{
					Warning1.Text = "Error in input - need value above or equal to zero";
					calcOK = false;
				}

				if (Convert.ToDouble(TVkanaler.Text) < 0)
				{
					Warning2.Text = "Error in input - need value above or equal to zero";
					calcOK = false;
				}

				if (Convert.ToDouble(VisitorToStore.Text) < 0)
				{
					Warning3.Text = "Error in input - need value above or equal to zero";
					calcOK = false;
				}

				if (Convert.ToDouble(MotionscenterAbonnement.Text) < 0)
				{
					Warning4.Text = "Error in input - need value above or equal to zero";
					calcOK= false;
				}

				if (Convert.ToDouble(Tilskuer.Text) < 0)
				{
					Warning5.Text = "Error in input - need value above or equal to zero";
					calcOK = false;
				}


				if (calcOK)
				{
					Warning1.Text = "";
					Warning2.Text = "";
					Warning3.Text = "";
					Warning4.Text = "";
					Warning5.Text = "";
					double ticketsSold = Convert.ToDouble(billetterSolgt.Text);
					double tVKanaler = Convert.ToDouble(TVkanaler.Text);
					double visitorsToStore = Convert.ToDouble(VisitorToStore.Text);
					double motionsCenter = Convert.ToDouble(MotionscenterAbonnement.Text);
					double visitorPercentage = Convert.ToDouble(Tilskuer.Text);

					double totalSale;
					double revenue;

					double saleFromDrinks = ticketsSold * visitorPercentage * SALG_DRIKKEVARE_GENNEMSNIT;
					double saleFromTV = tVKanaler * TV_RETTIGHEDER_PR_KANAL;
					double salesFromTickets = ticketsSold * BILLET_PRIS_GENNEMSNIT;
					double salesFromStore = visitorsToStore * SALG_FRA_BUTIK_BESOEG_PCT * SALG_SPORTS_VARE_GENNEMSNIT;
					double salesFromAbonnement = motionsCenter * ABONNEMENT_PRIS_6_MAANEDER;

					totalSale = salesFromTickets + saleFromDrinks + saleFromTV + salesFromStore + salesFromAbonnement;
					revenue = totalSale * TOTAL_OMK_I_PCT;

					TotalSalgResult.Text = string.Format("{0:N2}", totalSale);
					IndtjeningResult.Text = string.Format("{0:N2}", revenue);

				}

			}
		}
	}
}
