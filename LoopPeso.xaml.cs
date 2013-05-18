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
    public partial class LoopPeso : PhoneApplicationPage
    {
        List<Peso> pesos = new List<Peso>();
		List<GramosCentenas> gramosCentenas = new List<GramosCentenas>();
		List<GramosDecenas> gramosDecenas = new List<GramosDecenas>();
		List<GramosUnidades> gramosUnidades = new List<GramosUnidades>();
        public string data;
		public string datag;
        public string dataAltura;
        public string dataEdad;
        public string dataPeso;
        public string dataIndice;
        public string dataIndiceDesc;
        public string dataGenero;
		public string dataGramos;
        public LoopPeso()
        {
            InitializeComponent();
            if (App.IsMetric)
            {
			
				(ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = true;
				
                selectorIntPesoLibras.Visibility = Visibility.Collapsed;
                selectorIntPeso.Visibility = Visibility.Visible;
				selectorIntPesoGramosCentanas.Visibility = Visibility.Visible;
				selectorIntPesoGramosDecenas.Visibility = Visibility.Visible;
				selectorIntPesoGramosUnidades.Visibility = Visibility.Visible;
				//cl1.MinWidth = 480;
				//cl2.MaxWidth = 0;
            }
            else
            {
				(ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = false;
				
                
                selectorIntPesoLibras.Visibility = Visibility.Visible;
                selectorIntPeso.Visibility = Visibility.Collapsed;
				selectorIntPesoGramosCentanas.Visibility = Visibility.Visible;
				selectorIntPesoGramosDecenas.Visibility = Visibility.Visible;
				selectorIntPesoGramosUnidades.Visibility = Visibility.Visible;
				cl1.MinWidth = 480;
				cl2.MaxWidth = 0;
				cl3.MaxWidth =0;
				cl4.MaxWidth =0;
            }
                

        }
        void DataSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            data = e.AddedItems[0].ToString();
        }

		void DataSource_SelectionChangedGramos(object sender, SelectionChangedEventArgs e)
        {
            datag = e.AddedItems[0].ToString();
        }
        

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("dataAltura",		out dataAltura);
            NavigationContext.QueryString.TryGetValue("dataPeso",		out dataPeso);
            NavigationContext.QueryString.TryGetValue("dataEdad",		out dataEdad);
            NavigationContext.QueryString.TryGetValue("dataIndice",		out dataIndice);
            NavigationContext.QueryString.TryGetValue("dataIndiceDesc", out dataIndiceDesc);
            NavigationContext.QueryString.TryGetValue("dataGenero",		out dataGenero);
			NavigationContext.QueryString.TryGetValue("dataGramos",		out dataGramos);

            Initializa(dataPeso,dataGramos);
        }

        public void Initializa(string data,string datag)
        {

            //Edades

          
			 var cultura = CultureInfo.CurrentCulture;
            if (App.IsMetric)
            {
                for (int i = 10; i < 240; i++)
                    pesos.Add(new Peso() { peso = i });

				

                if (String.IsNullOrEmpty(data))
                    data = "80";
                
                this.selectorIntPeso.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>() { Items = pesos.Select(o => o.peso), SelectedItem = Convert.ToInt32(data.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray())[0]) };
                this.selectorIntPeso.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);

				
				CreaGramos(datag);
				//for(int a= 0; a< 10;a++)
				//{	
				//    gramosCentenas.Add(new GramosCentenas(){ Centenas=a});
				//    gramosDecenas.Add(new GramosDecenas(){ Decenas=a});
				//    gramosUnidades.Add(new GramosUnidades(){ Unidades=a});
				//}
					

				//if(String.IsNullOrEmpty(datag))
				//    datag =  "000";

				//this.selectorIntPesoGramosCentanas.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>{Items = gramosCentenas.Select(o=>o.Centenas),SelectedItem = Convert.ToInt32(datag.Substring(0,1))};
				//this.selectorIntPesoGramosDecenas.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>{Items = gramosDecenas.Select(o=>o.Decenas),SelectedItem = Convert.ToInt32(datag.Substring(1,1)) };
				//this.selectorIntPesoGramosUnidades.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>{Items = gramosUnidades.Select(o=>o.Unidades),SelectedItem = Convert.ToInt32(datag.Substring(2,1)) };


				//this.selectorIntPesoGramosCentanas.DataSource.SelectionChanged +=new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);
				//this.selectorIntPesoGramosDecenas.DataSource.SelectionChanged +=new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);
				//this.selectorIntPesoGramosUnidades.DataSource.SelectionChanged +=new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);

            }

            else
            {

                for (int i = 90; i < 400; i++)
                    pesos.Add(new Peso() { peso = i });

                if (String.IsNullOrEmpty(data))
                    data = "190";

				data = data.Split('.').First();

                this.selectorIntPesoLibras.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>() { Items = pesos.Select(o => o.peso), SelectedItem = Convert.ToInt32(data) };
                this.selectorIntPesoLibras.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);
            }
             
            
           
        }
		private void button1_Click(object sender, RoutedEventArgs e)
        {
            string peso  = (App.IsMetric == true) ? this.selectorIntPeso.DataSource.SelectedItem.ToString()  : this.selectorIntPesoLibras.DataSource.SelectedItem.ToString();
			if(App.IsMetric)
			{
				string gramosCentenas = (App.IsMetric == true) ? this.selectorIntPesoGramosCentanas.DataSource.SelectedItem.ToString():null;
				string gramosDecenas  =	(App.IsMetric == true) ? this.selectorIntPesoGramosDecenas.DataSource.SelectedItem.ToString():null;
				string gramosUnidades  =(App.IsMetric == true) ? this.selectorIntPesoGramosUnidades.DataSource.SelectedItem.ToString():null;
				string gramos = gramosCentenas + gramosDecenas + gramosUnidades;
				NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + peso +"&dataGramos=" + gramos, UriKind.Relative));
			}
			else
			{
				NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + peso , UriKind.Relative));
      	
			}
			
        }
        private void appbarSelect_Click(object sender, EventArgs e)
        {
            string peso  = (App.IsMetric == true) ? this.selectorIntPeso.DataSource.SelectedItem.ToString()  : this.selectorIntPesoLibras.DataSource.SelectedItem.ToString();
			if(App.IsMetric)
			{
				string gramosCentenas = (App.IsMetric == true) ? this.selectorIntPesoGramosCentanas.DataSource.SelectedItem.ToString():null;
				string gramosDecenas  =	(App.IsMetric == true) ? this.selectorIntPesoGramosDecenas.DataSource.SelectedItem.ToString():null;
				string gramosUnidades  =(App.IsMetric == true) ? this.selectorIntPesoGramosUnidades.DataSource.SelectedItem.ToString():null;
				string gramos = gramosCentenas + gramosDecenas + gramosUnidades;
				NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + peso +"&dataGramos=" + gramos, UriKind.Relative));
			}
			else
			{
				NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + peso , UriKind.Relative));
      	
			}
			
			//NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + peso +"&dataGramos=" + gramos, UriKind.Relative));
      
        }

		private void CreaGramos(string datag)
		{
				for(int a= 0; a< 10;a++)
				{	
				    gramosCentenas.Add(new GramosCentenas(){ Centenas=a});
					gramosDecenas.Add(new GramosDecenas(){ Decenas=a});
					gramosUnidades.Add(new GramosUnidades(){ Unidades=a});
				}
					

				if(String.IsNullOrEmpty(datag))
				    datag =  "000";

				this.selectorIntPesoGramosCentanas.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>{Items = gramosCentenas.Select(o=>o.Centenas),SelectedItem = Convert.ToInt32(datag.Substring(0,1))};
				this.selectorIntPesoGramosDecenas.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>{Items = gramosDecenas.Select(o=>o.Decenas),SelectedItem = Convert.ToInt32(datag.Substring(1,1)) };
				this.selectorIntPesoGramosUnidades.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>{Items = gramosUnidades.Select(o=>o.Unidades),SelectedItem = Convert.ToInt32(datag.Substring(2,1)) };


				this.selectorIntPesoGramosCentanas.DataSource.SelectionChanged +=new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);
				this.selectorIntPesoGramosDecenas.DataSource.SelectionChanged +=new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);
				this.selectorIntPesoGramosUnidades.DataSource.SelectionChanged +=new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);

		}
		private void appbarSelectZero_Click(object sender, EventArgs e)
		{
			CreaGramos(string.Empty);
		}
		
    }
}