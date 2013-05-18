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

namespace PesoIdeal
{
    public partial class LoopEdad : PhoneApplicationPage
    {
        List<Edad> edades = new List<Edad>();
        public string data;
        public string dataAltura;
        public string dataEdad;
        public string dataPeso;
		public string dataGramos;
        public string eda;
        public string dataIndice;
        public string dataIndiceDesc;
        public string dataGenero;
        public LoopEdad()
        {
            InitializeComponent();
            
           
        }

        void DataSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            data = e.AddedItems[0].ToString();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + selectorIntEdad.DataSource.SelectedItem.ToString() + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso, UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("dataAltura", out dataAltura);
            NavigationContext.QueryString.TryGetValue("dataPeso", out dataPeso);
            NavigationContext.QueryString.TryGetValue("dataEdad", out dataEdad);
            NavigationContext.QueryString.TryGetValue("dataIndice", out dataIndice);
            NavigationContext.QueryString.TryGetValue("dataIndiceDesc", out dataIndiceDesc);
            NavigationContext.QueryString.TryGetValue("dataGenero", out dataGenero);
			NavigationContext.QueryString.TryGetValue("dataGramos"		, out dataGramos	);
            Initiliza(dataEdad);
        }
        public void Initiliza(string data)
        {
            //Edades
            for (int i = 18; i < 120; i++)
            {
                edades.Add(new Edad() { edad = i });
            }

            if (String.IsNullOrEmpty(data))
                data = "25";
           


            this.selectorIntEdad.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<int>() { Items = edades.Select(o => o.edad), SelectedItem = Convert.ToInt32(data) };

            this.selectorIntEdad.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);
        }

        private void selectorIntEdad_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void appbarSelect_Click(object sender, EventArgs e)
        {
            //NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + selectorIntEdad.DataSource.SelectedItem.ToString() + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso , UriKind.Relative));
			 NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + selectorIntEdad.DataSource.SelectedItem.ToString() + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso + "&dataGramos=" + dataGramos, UriKind.Relative));
      
        }
    }
}