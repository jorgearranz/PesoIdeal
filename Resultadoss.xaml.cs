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
    public partial class Resultadoss : PhoneApplicationPage
    {
        string dataAltura   = string.Empty;
        string dataPeso     = string.Empty;
		string dataGramos	= string.Empty;
        string dataEdad     = string.Empty;
        string dataGenero   = string.Empty;
        string dataIndice   = string.Empty;
        string dataIndiceDbl = string.Empty;
        string datapageante = string.Empty;

        List<IndiceActividad> indiceactivida = new List<IndiceActividad>();
        public Resultadoss()
        {
            InitializeComponent();
            indiceactivida.Add(new IndiceActividad() { indice = 1.0, descripcion = Resource.Indice_PersonaSedentaria });
            indiceactivida.Add(new IndiceActividad() { indice = 1.2, descripcion = Resource.Indice_Actividadfisicaligera });
            indiceactivida.Add(new IndiceActividad() { indice = 1.4, descripcion = Resource.Indice_ActividadfisicaMedia });
            indiceactivida.Add(new IndiceActividad() { indice = 1.6, descripcion = Resource.Indice_Personamuyactiva });
            indiceactivida.Add(new IndiceActividad() { indice = 1.8, descripcion = Resource.Indice_Personaextremadamenteactiva });

            (ApplicationBar.Buttons[0] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Back_Text;
			 (ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Save_Text;

          
        }
       
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("dataAltura", out dataAltura))
            {
                //txtAltura.Text = dataAltura;
            }
            if (NavigationContext.QueryString.TryGetValue("dataPeso", out dataPeso))
            {
                //txtPeso.Text = dataPeso;
            }
			 if (NavigationContext.QueryString.TryGetValue("dataGramos", out dataGramos))
			{
			    //txtPeso.Text = dataPeso;
			}
            if (NavigationContext.QueryString.TryGetValue("dataEdad", out dataEdad))
            {
                //txtEdad.Text = dataEdad;
            }
            if (NavigationContext.QueryString.TryGetValue("dataGenero", out dataGenero))
            {
                //txtEdad.Text = dataGenero;
            }
            if (NavigationContext.QueryString.TryGetValue("dataIndice", out dataIndice))
            {
                //txtEdad.Text = dataGenero;
            }
            if (NavigationContext.QueryString.TryGetValue("dataIndice", out datapageante))
            {
                //txtEdad.Text = dataGenero;
            }
            var cultura = CultureInfo.CurrentCulture;
            
          //  CultureInfo culture = new CultureInfo("en-GB");
            RegionInfo regionInfo = new RegionInfo(cultura.Name);
            bool isMetric = regionInfo.IsMetric;

            Datos datos     = new Datos();
            datos.edad      = Convert.ToInt32(dataEdad);
            datos.genero    = dataGenero;
            //datos.altura    =  Convert.ToDouble(dataAltura.Replace(",", "."));
            if (App.IsMetric)
                datos.altura = Conversion.ConverDouble(dataAltura);
            else
            {
                double a = Convert.ToDouble(dataAltura);

                int intPies = (int)Math.Floor(a);
                double aPulgadas = a - Convert.ToDouble(intPies);

                double Pulgadas = Convert.ToDouble(dataAltura.Substring(dataAltura.IndexOf('.') + 1)); //Math.Round(aPulgadas, 2) * 100;
                double Pies = Convert.ToDouble(intPies);

                datos.altura = Conversion.ToCentimetros(Pies, Pulgadas);
            }
                

            if(App.IsMetric)
                 datos.peso = Convert.ToDouble(dataPeso.Replace(System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator.ToString(), "."));
				
            else
                datos.peso = Conversion.ToKilogramos(Convert.ToDouble(dataPeso));
            
			datos.indice    = Conversion.ConverDouble(dataIndice);

            if (App.IsMetric)
                txtAltura.Text =datos.altura.ToString() + "cm";
            else
            {
                double Pies;
                double Pulgadas;

                Conversion.ToPies(datos.altura, out Pies, out Pulgadas);
                txtAltura.Text = String.Format(cultura, "{0}' {1}''",Pies,Pulgadas);
            }
                

            if(App.IsMetric)
                txtPeso     .Text =string.Format("{0:#,#0.000}kg",datos.peso);  //Math.Round(datos.peso,3).ToString()+"kg";
            else
                txtPeso     .Text =string.Format("{0:#,#0}lb",dataPeso);

            txtEdad     .Text = datos.edad.ToString();
            if (datos.genero == "HOMBRE")
            {
                imageMen.Visibility = System.Windows.Visibility.Visible;
                imageWomen.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                imageMen.Visibility = System.Windows.Visibility.Collapsed;
                imageWomen.Visibility = System.Windows.Visibility.Visible;
            }
                 
            
            txtActividad.Text = indiceactivida.Where(o=> o.indice == datos.indice).First().descripcion;


            txtICM              .Text   = String.Format(cultura,"{0}"    ,Math.Round(datos.ICM               () ,2));
            if(App.IsMetric)
			{
				txtPesoIdeal        .Text   = datos.pesoideal < 10? Resource.NoDataAviable:string.Format("{0:#,#0.000}kg",datos.PESOIDEAL());
				txtPesoMaximo		.Text	= string.Format("{0:#,#0.000}kg",datos.PesoMaximoIdeal()); 
				txtPesoMinimo		.Text	= string.Format("{0:#,#0.000}kg",datos.PesoMinimoIdeal()); 
			}
            else
			{
				txtPesoIdeal        .Text   = datos.pesoideal < 10? Resource.NoDataAviable:string.Format("{0:#,#0}lb",datos.PESOIDEAL());
				txtPesoMaximo		.Text	= string.Format("{0:#,#0}lb",datos.PesoMaximoIdeal()); 
				txtPesoMinimo		.Text	= string.Format("{0:#,#0}lb",datos.PesoMinimoIdeal()); 
			
			}
             
           
			txtPorcentajeGrasa  .Text   =string.Format("{0:#,#0.00}%",datos.PORCENTAJEGRASA()); // String.Format(cultura,"{0}%"   ,Math.Round(datos.PORCENTAJEGRASA   () ,2));
            txtTasaMetabolica   .Text   =string.Format("{0:#,#0.00}kcal",datos.TASAMETABOLICA()); // String.Format(cultura,"{0}kcal",Math.Round(datos.TASAMETABOLICA    () ,2));
            txtKcalRecomendadas .Text   =string.Format("{0:#,#0.00}kcal",datos.KILOCALORIASDIA()); // String.Format(cultura,"{0}kcal",Math.Round(datos.KILOCALORIASDIA   () ,2));
            txtTipo             .Text   = datos.TIPOPESO();
            
            
            base.OnNavigatedTo(e);

        }

		private void appbarBack_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void appbarSave_Click(object sender, EventArgs e)
        {
            //NavigationService.Navigate(new Uri("/SaveData.xaml?dataIndice=" + dataIndice + "&dataGenero=" + dataGenero + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso , UriKind.Relative));
        
			NavigationService.Navigate(new Uri("/SaveData.xaml?dataIndice=" + dataIndice + "&dataGenero=" + dataGenero + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso + "&dataGramos=" + dataGramos, UriKind.Relative));
        
        }
    }
}