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
using System.Collections.ObjectModel;

namespace PesoIdeal
{
	public partial class DataUserGraphics : PhoneApplicationPage
	{
		 string Id;
		public DataUserGraphics()
		{
			InitializeComponent();
		}
		private ObservableCollection<TestDataItem> _data = new ObservableCollection<TestDataItem>();
        private ObservableCollection<IMCS> _dataIMC = new ObservableCollection<IMCS>();
		private ObservableCollection<Grasas> _dataGRASA = new ObservableCollection<Grasas>();

        public ObservableCollection<TestDataItem> Data { get { return _data; } set{ _data = value;} }
		public ObservableCollection<IMCS> DataIMC { get { return _dataIMC; } set{ _dataIMC = value;} }
		public ObservableCollection<Grasas> DataGRASA { get { return _dataGRASA; } set{ _dataGRASA = value;} }

        private void DataUserGraphc_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }

        void loadData()
        {
			try{
           
			ObservableCollection<TestDataItem> d = new ObservableCollection<TestDataItem>();
			ObservableCollection<IMCS> dIMC = new ObservableCollection<IMCS>();
			ObservableCollection<Grasas> dGRASA= new ObservableCollection<Grasas>();
			
         
            ContextoDatos ctx = new ContextoDatos();
            var data = ctx.Datas.Where(o => o.IdUsuario == Convert.ToInt32(Id) && o.Fecha >= DateTime.Now.AddMonths(-1) && o.Fecha <= DateTime.Now).OrderBy(o => o.Fecha).ThenBy(o=> o.Id);

            var cultura = CultureInfo.CurrentCulture;

            //  CultureInfo culture = new CultureInfo("en-GB");
            RegionInfo regionInfo = new RegionInfo(cultura.Name);
            bool isMetric = regionInfo.IsMetric;
            //listaUsuarios.Add(new Usuario { Id = user.Id, Nombre = user.Nombre });


            foreach (DataUser dato in data)
            {

                Datos datos = new Datos(dato.Genero, dato.Altura, dato.Edad, dato.Peso, dato.Indice);
				double peso = Math.Round(dato.Peso,2);
                string stPeso = App.IsMetric ? peso.ToString() : Conversion.ToLibras(datos.peso).ToString(); //Math.Round(Conversion.ToLibras(peso),2).ToString();

                d.Add(new TestDataItem()
				{
					Fecha = String.Format("{0}/{1}", CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dato.Fecha.Month) ,dato.Fecha.Day),
					Peso = stPeso
				});
				dIMC.Add(new IMCS()
				{
					Fecha = String.Format("{0}/{1}", CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dato.Fecha.Month) ,dato.Fecha.Day),
                    IMC = Math.Round(datos.imc,2).ToString(),
				});

				dGRASA.Add( new Grasas()
				{
					Fecha = String.Format("{0}/{1}", CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dato.Fecha.Month) ,dato.Fecha.Day),
					Grasa = Math.Round(datos.porcentajeGrasa,2).ToString()
				});
            }

			Data = d;
			DataIMC = dIMC;
			DataGRASA = dGRASA;

			this.DataContext = this;

		 

            
            }catch(Exception ex)
			{
				string kk = ex.Message;

			}
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("Id", out Id);
            loadData();
        }

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if ((e.Orientation & PageOrientation.Portrait) == (PageOrientation.Portrait))
            {
                NavigationService.Navigate(new Uri("/DataUsers.xaml?Id=" + Id, UriKind.Relative));  
            }
            else
            {
                //NavigationService.Navigate(new Uri("/DataUserGraphc.xaml?Id=" + Id, UriKind.Relative));

            }
        }
    }

    public class Pesos
    {
        public DateTime Fecha { get; set; }
        public String Peso { get; set; }
    }

	 public class IMCS
    {
        public String Fecha { get; set; }
        public String IMC { get; set; }
    }

	   public class Grasas
    {
        public String Fecha { get; set; }
        public String Grasa { get; set; }
    }
	public class TestDataItem
    {
        public String Fecha { get; set; }
       // public double val1 { get; set; }
       // public double val2 { get; set; }
        public String Peso { get; set; }
    }
}