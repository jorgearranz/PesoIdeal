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
    public partial class LoopIndice : PhoneApplicationPage
    {
        List<Peso> pesos = new List<Peso>();
        public string data;
        public string dataAltura;
        public string dataEdad;
        public string dataPeso;
		public string dataGramos;
        public string dataIndice;
        public string dataIndiceDesc;
        public string dataGenero;
        List<IndiceActividad> indiceactivida = new List<IndiceActividad>();
        public LoopIndice()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("dataAltura"      , out dataAltura    );
            NavigationContext.QueryString.TryGetValue("dataPeso"        , out dataPeso      );
            NavigationContext.QueryString.TryGetValue("dataEdad"        , out dataEdad      );
            NavigationContext.QueryString.TryGetValue("dataIndice"      , out dataIndice    );
            NavigationContext.QueryString.TryGetValue("dataIndiceDesc"  , out dataIndiceDesc);
            NavigationContext.QueryString.TryGetValue("dataGenero"      , out dataGenero    );
			NavigationContext.QueryString.TryGetValue("dataGramos"		, out dataGramos	);

            Initializa(dataIndiceDesc);
        }
        public void Initializa(string data)
        {
            String p1 = Resource.Indice_PersonaSedentaria;
            String p2=  Resource.Indice_Actividadfisicaligera.ToString() ;
            String p3 = Resource.Indice_ActividadfisicaMedia.ToString();
            String p4 = Resource.Indice_Personamuyactiva.ToString()  ;
            String p5 = Resource.Indice_Personaextremadamenteactiva.ToString();
            
            indiceactivida.Add(new IndiceActividad() { indice = 1.0, descripcion = p1 });
            indiceactivida.Add(new IndiceActividad() { indice = 1.2, descripcion = p2 });
            indiceactivida.Add(new IndiceActividad() { indice = 1.4, descripcion = p3 });
            indiceactivida.Add(new IndiceActividad() { indice = 1.6, descripcion = p4 });
            indiceactivida.Add(new IndiceActividad() { indice = 1.8, descripcion = p5 });


            if (String.IsNullOrEmpty(data))
            { 
                data = Resource.Indice_PersonaSedentaria;
                dataIndiceDesc = Resource.Indice_PersonaSedentaria; ;

                dataIndice = indiceactivida.Where(o => o.descripcion.Contains(dataIndiceDesc)).Select(o => o.indice).First().ToString();
            }
                


            this.selectorActividad.DataSource = new PesoIdeal.MainPage.ListLoopingDataSource<string>() { Items = indiceactivida.Select(o=>o.descripcion), SelectedItem = data };
            this.selectorActividad.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(DataSource_SelectionChanged);

        }
        void DataSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataIndiceDesc = e.AddedItems[0].ToString();
            dataIndice = indiceactivida.Where(o => o.descripcion.Contains(e.AddedItems[0].ToString())).Select(o => o.indice).First().ToString();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso, UriKind.Relative));
             NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso + "&dataGramos=" + dataGramos, UriKind.Relative));
        }

        private void appbarSelect_Click(object sender, EventArgs e)
        {
           // NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso , UriKind.Relative));
      
          NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso + "&dataGramos=" + dataGramos, UriKind.Relative));
      
        }

		
    }
}