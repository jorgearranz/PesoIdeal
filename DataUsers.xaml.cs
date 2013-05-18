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
using System.Data.Common;

namespace PesoIdeal
{
    public partial class DatasUsers : PhoneApplicationPage
    {
        public string Id;
        public DatasUsers()
        {
            InitializeComponent();

			(ApplicationBar.Buttons[0] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Users_Text;
            (ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.EliminarCaptionText;
			(ApplicationBar.Buttons[2] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Detalles_Text;
			(ApplicationBar.Buttons[3] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Add_Text;

           
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("Id", out Id);
            ShowUser();
            loadData();
            
        }
        void ShowUser()
        {
            using (ContextoDatos ctx = new ContextoDatos())
            {
                var user = ctx.Users.Where(x => x.Id == Convert.ToInt32(Id)).SingleOrDefault();
                PageTitle.Text = user.Nombre;
            }
        }

        void loadData()
        {
            listBox1.ItemsSource = null;
            List<Resultado> resultados = new List<Resultado>();
            ContextoDatos ctx = new ContextoDatos();
            var data = ctx.Datas.Where(o=>o.IdUsuario == Convert.ToInt32(Id)).OrderBy(o => o.Fecha ).ThenBy(o=> o.Id);

			var cultura = CultureInfo.CurrentCulture;
            
          //  CultureInfo culture = new CultureInfo("en-GB");
            RegionInfo regionInfo = new RegionInfo(cultura.Name);
            bool isMetric = regionInfo.IsMetric;
            //listaUsuarios.Add(new Usuario { Id = user.Id, Nombre = user.Nombre });
		
		    double pesoAnterior=0;
			bool entra = false;
            foreach (DataUser dato in data)
            {
				
			
                Datos datos = new Datos(dato.Genero,dato.Altura,dato.Edad,dato.Peso,dato.Indice);
				


                DateTime Fecha = new DateTime();

                
                resultados.Add	(
								new Resultado()
								{ 
                                            IdUsuario       = dato.IdUsuario.ToString(),
                                            IdDato          = dato.Id,
											FechaOrder		= dato.Fecha,
											Fecha			= dato.Fecha.ToShortDateString(), 
                                            FechaDia        = dato.Fecha.Day.ToString(),
											NomDia			= CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(dato.Fecha.DayOfWeek),
                                            FechaMes        = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dato.Fecha.Month),
											imc				= string.Format("{0:#,#0.00}",datos.imc),
											Peso			= App.IsMetric?string.Format("{0:#,#0.000}kg",dato.Peso):string.Format("{0:#,#0}lb",Conversion.ToLibras(dato.Peso)),
											//Peso			= String.Format(cultura,"{0}{1}" ,Math.Round(App.IsMetric?datos.peso:Conversion.ToLibras(datos.peso) ,2), App.IsMetric ? "Kg" : "lb"),
											PorcentajeGrasa = String.Format(cultura,"{0}%"   ,Math.Round(datos.porcentajeGrasa	,2)),
											upVisible		= !entra?Visibility.Collapsed:dato.Peso > pesoAnterior?Visibility.Visible:Visibility.Collapsed,
											downVisible		= !entra?Visibility.Collapsed:dato.Peso < pesoAnterior?Visibility.Visible:Visibility.Collapsed,
											PesoDif			= !entra?"":resulDif(dato.Peso,pesoAnterior),
											ImcColor		= datos.imc < 18.5 || datos.imc > 25 ? new SolidColorBrush(Colors.Red): new SolidColorBrush(Colors.White)
											
								}
								);

				if(!entra)
				{
					entra=true;
				}
				
				pesoAnterior =  dato.Peso;

            }
			
            listBox1.ItemsSource = resultados.OrderByDescending(o => o.FechaOrder ).ThenByDescending(o=> o.IdDato);
        }
		public String resulDif(double peso, double pesoanterior)
		{
			string resul="";
			if(App.IsMetric)
			{
				resul = peso-pesoanterior>0?string.Format("+{0:#,#0.000}kg", peso - pesoanterior):string.Format("{0:#,#0.000}kg", peso - pesoanterior);
			}
			else
			{
				resul = peso-pesoanterior>0? string.Format("+{0:#,#0}lb",Conversion.ToLibras(peso) - Conversion.ToLibras(pesoanterior)):string.Format("{0:#,#0}lb",Conversion.ToLibras(peso) - Conversion.ToLibras(pesoanterior));
			}
			return resul;
		}
        public Resultado resultadoSelected;
        public Canvas canvasSelected;

      
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resultadoSelected = (sender as ListBox).SelectedItem as Resultado;
        }

        private void appbarDelete_Click(object sender, EventArgs e)
        {
            if (resultadoSelected == null)
            {
                MessageBox.Show(Resource.EliminarNoSelected.ToString());
            }
            else
            {
                if (MessageBox.Show(Resource.EliminarMessageBoxText , Resource.EliminarCaptionText, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    try
                    {
                        using (ContextoDatos ctx = new ContextoDatos())
                        {

                           // var user = ctx.Users.Where(x => x.Id == userSelected.Id).FirstOrDefault();
                            var userData = ctx.Datas.Where(x => x.Id == Convert.ToInt32(resultadoSelected.IdDato));

                            ctx.GetTable<DataUser>().DeleteAllOnSubmit(userData);
                            //ctx.GetTable<User>().DeleteOnSubmit(user);

                            ctx.SubmitChanges();
                           
                        }
                    }
                    catch (DbException x)
                    {
                        MessageBox.Show("An error has occurred!, we worked to resolved this problem");
                    }
                    loadData();
                }
            }
        }

        private void appbarVer_Click(object sender, EventArgs e)
        {
            if (resultadoSelected != null)
            {
                double Pulgadas;
                double Pies;
                string altura="";
                Boolean  entra = false;
                DataUser datosUsuario = new DataUser();
                using (ContextoDatos ctx = new ContextoDatos())
                {
                     datosUsuario = ctx.Datas.Where(x => x.Id == Convert.ToInt32(resultadoSelected.IdDato)).SingleOrDefault();


                    if (App.IsMetric)
                        NavigationService.Navigate(new Uri("/Resultadoss.xaml?dataIndiceDesc=" + String.Empty + "&dataIndice=" + datosUsuario.Indice.ToString() + "&dataGenero=" + datosUsuario.Genero + "&dataEdad=" + datosUsuario.Edad.ToString() + "&dataAltura=" + datosUsuario.Altura.ToString() + "&dataPeso=" + datosUsuario.Peso.ToString(), UriKind.Relative));
                    else
                    {
                       
                        Conversion.ToPies(datosUsuario.Altura, out Pies, out Pulgadas);
                        altura = String.Format("{0}.{1}", Pies, Pulgadas);
                        entra = true;
                    }

                    
                }

                if (entra)
                {
                    NavigationService.Navigate(new Uri("/Resultadoss.xaml?dataIndiceDesc=" + String.Empty + "&dataIndice=" + datosUsuario.Indice.ToString() + "&dataGenero=" + datosUsuario.Genero + "&dataEdad=" + datosUsuario.Edad.ToString() + "&dataAltura=" + altura + "&dataPeso=" +string.Format("{0:#,#0}",Conversion.ToLibras(datosUsuario.Peso)), UriKind.Relative));
                }
                
            }
            else 
            {
                MessageBox.Show(Resource.EditarUserNoSelected);
            }
        }

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if ((e.Orientation & PageOrientation.Portrait) == (PageOrientation.Portrait))
            {

               
                 
            }
            else
            {
                NavigationService.Navigate(new Uri("/DataUserGraphics.xaml?Id="+Id, UriKind.Relative));

            }

        }

		private void appbarUser_Click(object sender, EventArgs e)
		{
			 NavigationService.Navigate(new Uri("/ListadoUsuarios.xaml?", UriKind.Relative));
		}
		public string dataIndice;
        public string dataIndiceDesc;
		List<IndiceActividad> indiceactivida = new List<IndiceActividad>();
		private void appbarAdd_Click(object sender, EventArgs e)
		{
			//ContextoDatos ctx = new ContextoDatos();
            
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

				(App.Current as App).CurrentUser = Convert.ToInt32(Id);

                double Pulgadas;
                double Pies;
				string peso="";
                string altura="";
                Boolean  entra = false;
                DataUser datosUsuario = new DataUser();
                using (ContextoDatos ctx = new ContextoDatos())
                {
					datosUsuario = ctx.Datas.Where(o=>o.IdUsuario == Convert.ToInt32(Id)).OrderByDescending(o => o.Fecha ).ThenByDescending(o=> o.Id).First();
					
                    // datosUsuario = ctx.Datas.Where(x => x.Id == Convert.ToInt32(resultadoSelected.IdDato)).SingleOrDefault();
					


                    if (App.IsMetric)
					{
					  altura = string.Format("{0:#,#0.00}",datosUsuario.Altura/100); 
					  peso	 = string.Format("{0:#,#0.000}",datosUsuario.Peso); 

					   NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + indiceactivida.Where(o=> o.indice == datosUsuario.Indice).First().descripcion  + "&dataIndice=" + datosUsuario.Indice.ToString() + "&dataGenero=" + datosUsuario.Genero + "&dataEdad=" + datosUsuario.Edad.ToString() + "&dataAltura=" + altura + "&dataPeso=" + peso, UriKind.Relative));
					}
                     
                    else
                    {
                       
                        Conversion.ToPies(datosUsuario.Altura, out Pies, out Pulgadas);
                        altura = String.Format("{0}.{1}", Pies, Pulgadas);
                        entra = true;
						
                    }
                }

                if (entra)
                {
                    NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + indiceactivida.Where(o=> o.indice == datosUsuario.Indice).First().descripcion + "&dataIndice=" + datosUsuario.Indice.ToString() + "&dataGenero=" + datosUsuario.Genero + "&dataEdad=" + datosUsuario.Edad.ToString() + "&dataAltura=" + altura + "&dataPeso=" + string.Format("{0:#,#0}",Conversion.ToLibras(datosUsuario.Peso)), UriKind.Relative));
                }
                
            }
           
		
    }

    public class Resultado
    {
        public String		IdUsuario			{ get; set; }
		public String		PesoDif				{ get; set; }
        public Int32		IdDato				{ get; set; }
        public String		Fecha				{ get; set; }
		public DateTime		FechaOrder			{ get; set; }
        public String		FechaDia			{ get; set; }
		public String		NomDia				{ get; set; }
        public String		FechaMes			{ get; set; }
        public String		Peso				{ get; set; }
        public String		imc					{ get; set; }
        public String		PorcentajeGrasa		{ get; set; }
		public Visibility	upVisible			{ get; set; }
		public Visibility   downVisible			{ get; set; }
		public SolidColorBrush		ImcColor			{ get; set; }
    }
}