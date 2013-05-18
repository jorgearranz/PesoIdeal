using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Globalization;

namespace PesoIdeal
{
    public partial class LoopAltura : PhoneApplicationPage
    {
        public string data;
        public string dataCentimetros;
        public string dataMetros;
        public string dataAltura;
        public string dataEdad;
        public string dataPeso;
		public string dataGramos;
        public string dataIndice;
        public string dataIndiceDesc;
        public string dataGenero;
        List<int> centimetros = new List<int>();
        public LoopAltura()
        {
            InitializeComponent();

          
          
          
        }

        void DataSource_SelectionChangedMetros(object sender, SelectionChangedEventArgs e)
        {
            dataMetros = e.AddedItems[0].ToString();

        }
        void DataSource_SelectionChangedCentiMetros(object sender, SelectionChangedEventArgs e)
        {
            dataCentimetros= e.AddedItems[0].ToString();

        }
        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    var cultura = CultureInfo.CurrentCulture;
        //    RegionInfo regionInfo = new RegionInfo(cultura.Name);
           
        //    if(App.IsMetric)
        //        NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataMetros + cultura.NumberFormat.CurrencyDecimalSeparator + dataCentimetros + "&dataPeso=" + dataPeso, UriKind.Relative));
        //    else
        //        NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataMetros + cultura.NumberFormat.CurrencyDecimalSeparator + dataCentimetros + "&dataPeso=" + dataPeso, UriKind.Relative));
        //}


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("dataAltura", out dataAltura);
            NavigationContext.QueryString.TryGetValue("dataPeso", out dataPeso);
			NavigationContext.QueryString.TryGetValue("dataGramos",out dataGramos);
			NavigationContext.QueryString.TryGetValue("dataEdad", out dataEdad);
            NavigationContext.QueryString.TryGetValue("dataIndice", out dataIndice);
            NavigationContext.QueryString.TryGetValue("dataIndiceDesc", out dataIndiceDesc);
            NavigationContext.QueryString.TryGetValue("dataGenero", out dataGenero);

            Initializa(dataAltura);
        }

        public void Initializa(string data)
        {

            var cultura = CultureInfo.CurrentCulture;
            RegionInfo regionInfo = new RegionInfo(cultura.Name);

            List<int> intMetros;

            

            if (App.IsMetric)
            { 
               
                intMetros = new List<int> {  1, 2 };
                for (int i = 0; i < 99; i++)
                {
                    centimetros.Add(i);
                }

                if (String.IsNullOrEmpty(data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray().First()).First()) && String.IsNullOrEmpty(data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray().First()).Last()))
                {
                    dataCentimetros = "80";
                    dataMetros = "1";
                }
                else
                {
                    dataCentimetros = data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray().First()).Last().ToString();
                    dataMetros = data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray().First()).First().ToString();
                }
            }
            else
            {
                //pies
                intMetros = new List<int> { 4,5,6,7 };
               // pulgadas
                for (int i = 0; i < 12; i++)
                {
                    centimetros.Add(i);
                }
                if (String.IsNullOrEmpty(data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray().First()).First()) && String.IsNullOrEmpty(data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray().First()).Last()))
                {
                    dataCentimetros = "0";
                    dataMetros = "6";
                }
                else
                {
                    dataCentimetros = data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray().First()).Last().ToString();
                    dataMetros = data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray().First()).First().ToString();
                }
            }
            this.selectorIntMetros.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>() { Items = intMetros, SelectedItem =Convert.ToInt32( dataMetros) };
            this.selectorInCentimetros.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>() { Items = centimetros, SelectedItem =Convert.ToInt32( dataCentimetros) };


            this.selectorIntMetros.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChangedMetros);
            this.selectorInCentimetros.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChangedCentiMetros);
        }

        private void appbarSelect_Click(object sender, EventArgs e)
        {
            var cultura = CultureInfo.CurrentCulture;
            RegionInfo regionInfo = new RegionInfo(cultura.Name);

            if (App.IsMetric)
			{
				int	dc=0;
				if(Convert.ToInt32(dataCentimetros) < 10)
				{
					dc = Convert.ToInt32(dataCentimetros) * 10;

					dataCentimetros = "0" +dataCentimetros ;
				}
				 

				//NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataMetros + cultura.NumberFormat.CurrencyDecimalSeparator + dataCentimetros + "&dataPeso=" + dataPeso , UriKind.Relative));
				NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataMetros + cultura.NumberFormat.CurrencyDecimalSeparator + dataCentimetros + "&dataPeso=" + dataPeso + "&dataGramos=" + dataGramos, UriKind.Relative));
			}
            else
                NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataMetros + cultura.NumberFormat.CurrencyDecimalSeparator + dataCentimetros + "&dataPeso=" + dataPeso , UriKind.Relative));

        }
    }
}