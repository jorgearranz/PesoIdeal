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
using Microsoft.Phone.Controls.Primitives;

using System.ComponentModel;
using System.Threading;
using System.Globalization;
using Microsoft.Phone.Tasks;
namespace PesoIdeal
{
    public partial class MainPage : PhoneApplicationPage
    {
        //GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
       
        //protected GeoCoordinateWatcher Watcher { get; set; }
        //private double _altitude;
        //public double Altitude
        //{
        //    get { return _altitude; }

        //    set { _altitude = value; PropertyChanged(this, new PropertyChangedEventArgs("Altitude")); }
        //}

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
 

        List<Genero> generos = new List<Genero>();
        List<Altura> alturas = new List<Altura>();
       
        // Constructor
        public MainPage()
        {
            InitializeComponent();


            //Alturas

            indiceactivida.Add(new IndiceActividad() { indice = 1.0, descripcion = Resource.Indice_PersonaSedentaria            });
            indiceactivida.Add(new IndiceActividad() { indice = 1.2, descripcion = Resource.Indice_Actividadfisicaligera        });
            indiceactivida.Add(new IndiceActividad() { indice = 1.4, descripcion = Resource.Indice_ActividadfisicaMedia         });
            indiceactivida.Add(new IndiceActividad() { indice = 1.6, descripcion = Resource.Indice_Personamuyactiva             });
            indiceactivida.Add(new IndiceActividad() { indice = 1.8, descripcion = Resource.Indice_Personaextremadamenteactiva  });
          

         
            //Generos   
            generos.Add(new Genero() { genero = "Masculino" });
            generos.Add(new Genero() { genero = "Femenino" });



          
            (ApplicationBar.Buttons[0] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.About_Text;
            (ApplicationBar.Buttons[3] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Users_Text;

        }
       
      
        
        private void textBox1_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //IndiceActividad ia = ((IndiceActividad)indices.SelectedItem);

            //if (ia == null) dataIndice = ""; else dataIndice = ia.indice.ToString(); 
            if (rdHombre.IsChecked == true) dataGenero = "HOMBRE";
            if (rdMujer.IsChecked == true ) dataGenero = "MUJER";
            dataIndiceDesc = txtIndice.Text;
            //NavigationService.Navigate(new Uri("/LoopAltura.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso, UriKind.Relative));
			NavigationService.Navigate(new Uri("/LoopAltura.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso+ "&dataGramos=" + dataGramos, UriKind.Relative));
        }
        private void textBox2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //IndiceActividad ia = ((IndiceActividad)indices.SelectedItem);
           // if (ia == null) dataIndice = ""; else dataIndice = ia.indice.ToString(); 
            if (rdHombre.IsChecked == true) dataGenero = "HOMBRE";
            if (rdMujer.IsChecked == true) dataGenero = "MUJER";
            dataIndiceDesc = txtIndice.Text;
            //NavigationService.Navigate(new Uri("/LoopEdad.xaml", UriKind.Relative));
            //NavigationService.Navigate(new Uri("/LoopEdad.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso , UriKind.Relative));
			NavigationService.Navigate(new Uri("/LoopEdad.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso + "&dataGramos=" + dataGramos, UriKind.Relative));
        }
        private void textBox3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //IndiceActividad ia = ((IndiceActividad)indices.SelectedItem);
            //if (ia == null) dataIndice = ""; else dataIndice = ia.indice.ToString(); 
            if (rdHombre.IsChecked == true) dataGenero = "HOMBRE";
            if (rdMujer.IsChecked == true) dataGenero = "MUJER";
            dataIndiceDesc = txtIndice.Text;
            
            //NavigationService.Navigate(new Uri("/LoopPeso.xaml", UriKind.Relative));
            //NavigationService.Navigate(new Uri("/LoopPeso.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso , UriKind.Relative));
			NavigationService.Navigate(new Uri("/LoopPeso.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso + "&dataGramos=" + dataGramos, UriKind.Relative));
        }
        private void txtIndice_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (rdHombre.IsChecked == true) dataGenero = "HOMBRE";
            if (rdMujer.IsChecked == true) dataGenero = "MUJER";
            dataIndiceDesc = txtIndice.Text;
           // NavigationService.Navigate(new Uri("/LoopIndice.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso , UriKind.Relative));
			NavigationService.Navigate(new Uri("/LoopIndice.xaml?dataIndiceDesc=" + dataIndiceDesc + "&dataGenero=" + dataGenero + "&dataIndice=" + dataIndice + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso + "&dataGramos=" + dataGramos, UriKind.Relative));
        }


        string dataAltura   = string.Empty;
        string dataPeso     = string.Empty;
		string dataGramos	= string.Empty;
        string dataEdad     = string.Empty;
        string dataIndice   = string.Empty;
        string dataIndiceDesc = string.Empty;
        string dataGenero = string.Empty; 
        List<IndiceActividad> indiceactivida = new List<IndiceActividad>();
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("dataAltura", out dataAltura))
            {
                txtAltura.Text = dataAltura;
            }
            if (NavigationContext.QueryString.TryGetValue("dataPeso", out dataPeso))
            {
				if(!String.IsNullOrEmpty(dataPeso))
				{
					if(App.IsMetric)
					{
						 var cultura = CultureInfo.CurrentCulture;
						RegionInfo regionInfo = new RegionInfo(cultura.Name);
						
						if(NavigationContext.QueryString.TryGetValue("dataGramos", out dataGramos))
						{
							txtPeso.Text = dataPeso + cultura.NumberFormat.CurrencyDecimalSeparator + dataGramos.PadRight(3,'0') .Substring(0,3);
						}
						else
						{
							dataGramos = dataPeso.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray()).Last().PadRight(3,'0').Substring(0,3) ;
						    txtPeso.Text = dataPeso.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray()).First() +cultura.NumberFormat.CurrencyDecimalSeparator+ dataPeso.Split(cultura.NumberFormat.CurrencyDecimalSeparator.ToCharArray()).Last().PadRight(3,'0').Substring(0,3) ;
						}
					}
					else
					txtPeso.Text = dataPeso;
				}
				
            }
            if (NavigationContext.QueryString.TryGetValue("dataEdad", out dataEdad))
            {
                txtEdad.Text = dataEdad;
            }
            
            if (NavigationContext.QueryString.TryGetValue("dataIndice", out dataIndice))
            {
                //txtIndice.Text = dataIndice;
            }
            if (NavigationContext.QueryString.TryGetValue("dataIndiceDesc", out dataIndiceDesc))
            {
                txtIndice.Text = dataIndiceDesc;
            }
            if (NavigationContext.QueryString.TryGetValue("dataGenero", out dataGenero))
            {
                if (dataGenero.Contains("MUJER"))
                    rdMujer.IsChecked = true;
                
                if (dataGenero.Contains("HOMBRE"))
                    rdHombre.IsChecked = true;
                

            }
            

           // GC.Collect();
            //NavigationService.RemoveBackEntry();
            base.OnNavigatedTo(e);

        }
        //protected override void OnRemovedFromJournal(JournalEntryRemovedEventArgs e)
        //{
            
        //    base.OnRemovedFromJournal(e);
        //}
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var cultura = CultureInfo.CurrentCulture;
            RegionInfo regionInfo = new RegionInfo(cultura.Name);

            if (  String.IsNullOrEmpty(txtAltura.Text) || String.IsNullOrEmpty(txtEdad.Text) || String.IsNullOrEmpty(txtPeso.Text) || (rdHombre.IsChecked == false && rdMujer.IsChecked == false))
                MessageBox.Show(Resource.CompleteInformacion);
            else 
            {
                 Datos datos = new Datos();
                datos.edad = Convert.ToInt32(txtEdad.Text);
                if (rdHombre.IsChecked == true)
                    datos.genero = "HOMBRE";
                else
                    datos.genero = "MUJER";

				 if(App.IsMetric)
					datos.altura    = Convert.ToDouble(txtAltura.Text.Replace(System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator, ".")) * 100;
                 else
					datos.altura    = Convert.ToDouble(txtAltura.Text.Replace(System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator, "."));
			
				datos.peso      = Convert.ToDouble(txtPeso.Text.Replace(System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator.ToString(), "."));
				
                datos.indice    = indiceactivida.Where(o => o.descripcion.ToString().Contains(txtIndice.Text)).First().indice;
               
				 if(App.IsMetric)
                    NavigationService.Navigate(new Uri("/Resultadoss.xaml?dataIndiceDesc=" + txtIndice.Text + "&dataIndice=" + datos.indice.ToString() + "&dataGenero=" + datos.genero + "&dataEdad=" + dataEdad + "&dataAltura=" + datos.altura.ToString() + "&dataPeso=" + datos.peso.ToString() , UriKind.Relative));
                else
					NavigationService.Navigate(new Uri("/Resultadoss.xaml?dataIndiceDesc="+txtIndice.Text+"&dataIndice="+datos.indice.ToString()+ "&dataGenero=" + datos.genero + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso, UriKind.Relative));
            
				//if(App.IsMetric)
                //    NavigationService.Navigate(new Uri("/Resultadoss.xaml?dataIndiceDesc=" + txtIndice.Text + "&dataIndice=" + datos.indice.ToString() + "&dataGenero=" + datos.genero + "&dataEdad=" + dataEdad + "&dataAltura=" + datos.altura.ToString() + "&dataPeso=" + dataPeso , UriKind.Relative));
                //else
                //NavigationService.Navigate(new Uri("/Resultadoss.xaml?dataIndiceDesc="+txtIndice.Text+"&dataIndice="+datos.indice.ToString()+ "&dataGenero=" + datos.genero + "&dataEdad=" + dataEdad + "&dataAltura=" + dataAltura + "&dataPeso=" + dataPeso , UriKind.Relative));
            }

           
         
        }
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //if (MessageBox.Show( Resource.Exit_Ques,Resource.Exit_Text,
            //                        MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            //{
                e.Cancel = false;
               
            //}
        }

        #region Looping
        // abstract the reusable code in a base class
        // this will allow us to concentrate on the specifics when implementing deriving looping data source classes
        public abstract class LoopingDataSourceBase : ILoopingSelectorDataSource
        {
            private object selectedItem;

            #region ILoopingSelectorDataSource Members

            public abstract object GetNext(object relativeTo);

            public abstract object GetPrevious(object relativeTo);

            public object SelectedItem
            {
                get
                {
                    return this.selectedItem;
                }
                set
                {
                    // this will use the Equals method if it is overridden for the data source item class
                    if (!object.Equals(this.selectedItem, value))
                    {
                        // save the previously selected item so that we can use it 
                        // to construct the event arguments for the SelectionChanged event
                        object previousSelectedItem = this.selectedItem;
                        this.selectedItem = value;
                        // fire the SelectionChanged event
                        this.OnSelectionChanged(previousSelectedItem, this.selectedItem);
                    }
                }
            }

            public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

            protected virtual void OnSelectionChanged(object oldSelectedItem, object newSelectedItem)
            {
                EventHandler<SelectionChangedEventArgs> handler = this.SelectionChanged;
                if (handler != null)
                {
                    handler(this, new SelectionChangedEventArgs(new object[] { oldSelectedItem }, new object[] { newSelectedItem }));
                }
            }

            #endregion
        }

        public class ListLoopingDataSource<T> : LoopingDataSourceBase
        {
            private LinkedList<T> linkedList;
            private List<LinkedListNode<T>> sortedList;
            private IComparer<T> comparer;
            private NodeComparer nodeComparer;

            public ListLoopingDataSource()
            {
            }

            public IEnumerable<T> Items
            {
                get
                {
                    return this.linkedList;
                }
                set
                {
                    this.SetItemCollection(value);
                }
            }

            private void SetItemCollection(IEnumerable<T> collection)
            {
                this.linkedList = new LinkedList<T>(collection);

                this.sortedList = new List<LinkedListNode<T>>(this.linkedList.Count);
                // initialize the linked list with items from the collections
                LinkedListNode<T> currentNode = this.linkedList.First;
                while (currentNode != null)
                {
                    this.sortedList.Add(currentNode);
                    currentNode = currentNode.Next;
                }

                IComparer<T> comparer = this.comparer;
                if (comparer == null)
                {
                    // if no comparer is set use the default one if available
                    if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                    {
                        comparer = Comparer<T>.Default;
                    }
                    else
                    {
                        throw new InvalidOperationException("There is no default comparer for this type of item. You must set one.");
                    }
                }

                this.nodeComparer = new NodeComparer(comparer);
                this.sortedList.Sort(this.nodeComparer);
            }

            public IComparer<T> Comparer
            {
                get
                {
                    return this.comparer;
                }
                set
                {
                    this.comparer = value;
                }
            }

            public override object GetNext(object relativeTo)
            {
                // find the index of the node using binary search in the sorted list
                int index = this.sortedList.BinarySearch(new LinkedListNode<T>((T)relativeTo), this.nodeComparer);
                if (index < 0)
                {
                    return default(T);
                }

                // get the actual node from the linked list using the index
                LinkedListNode<T> node = this.sortedList[index].Next;
                if (node == null)
                {
                    // if there is no next node get the first one
                    node = this.linkedList.First;
                }
                return node.Value;
            }

            public override object GetPrevious(object relativeTo)
            {
                int index = this.sortedList.BinarySearch(new LinkedListNode<T>((T)relativeTo), this.nodeComparer);
                if (index < 0)
                {
                    return default(T);
                }
                LinkedListNode<T> node = this.sortedList[index].Previous;
                if (node == null)
                {
                    // if there is no previous node get the last one
                    node = this.linkedList.Last;
                }
                return node.Value;
            }

            private class NodeComparer : IComparer<LinkedListNode<T>>
            {
                private IComparer<T> comparer;

                public NodeComparer(IComparer<T> comparer)
                {
                    this.comparer = comparer;
                }

                #region IComparer<LinkedListNode<T>> Members

                public int Compare(LinkedListNode<T> x, LinkedListNode<T> y)
                {
                    return this.comparer.Compare(x.Value, y.Value);
                }

                #endregion
            }

        }


        
        #endregion
        private void appbarBack_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml?", UriKind.Relative));
        }
        private void appbarUser_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ListadoUsuarios.xaml?", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ListadoUsuarios.xaml?loadData=true", UriKind.Relative));
        }

       
        
      	private void fb_Click(object sender, EventArgs e)
		{
			ShareLinkTask shareLinkTask = new ShareLinkTask();
		
			shareLinkTask.LinkUri = new Uri((App.Current as App).UrlToShared, UriKind.Absolute);
			shareLinkTask.Message ="Ideal Weight" ;
			shareLinkTask.Show();
		}

		private void twitter_Click(object sender, EventArgs e)
		{
			ShareLinkTask shareLinkTask = new ShareLinkTask();
			shareLinkTask.LinkUri = new Uri((App.Current as App).UrlToShared, UriKind.Absolute);
			shareLinkTask.Message ="Ideal Weight" ;
			shareLinkTask.Show();
		}

        

      
        

      


    }
}