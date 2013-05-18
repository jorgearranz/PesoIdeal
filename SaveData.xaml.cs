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
using System.Data.Common;

namespace PesoIdeal
{
	public partial class SaveData : PhoneApplicationPage
	{
		string dataAltura   = string.Empty;
        string dataPeso     = string.Empty;
		string dataGramos	= string.Empty;
        string dataEdad     = string.Empty;
        string dataGenero   = string.Empty;
        string dataIndice   = string.Empty;
        string dataIndiceDbl = string.Empty;

		public SaveData()
		{
			InitializeComponent();
			Loaded +=new RoutedEventHandler(MainPage_Loaded);

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
		 }

		 void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
           
			loadUsers();
				
        }

		void loadUsers()
		{
			listBox1.ItemsSource = null;
			List<Usuario> listaUsuarios = new List<Usuario>();
			ContextoDatos ctx = new ContextoDatos();
			var data = ctx.Users.OrderBy(o=>o.Nombre);
			if(ctx.DatabaseExists())
			{
				foreach(User user in data)
				{
					if((App.Current as App).CurrentUser.HasValue)
					{
						if(user.Id  == (App.Current as App).CurrentUser.Value)
							listaUsuarios.Add(new Usuario{ Id = user.Id, Nombre = user.Nombre, Selected = true});
						else
							listaUsuarios.Add(new Usuario{ Id = user.Id, Nombre = user.Nombre});
					}
					else
					{
						listaUsuarios.Add(new Usuario{ Id = user.Id, Nombre = user.Nombre});
					}
						
				}
				listBox1.ItemsSource = listaUsuarios;

				
					
			}
		}
		private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			string kk="";
            //Get the data object that represents the current selected item
           
            //TipoJuego data = (sender as ListBox).SelectedItem as TipoJuego;

            //Get the selected ListBoxItem container instance    
            //ListBoxItem selectedItem = this.listBox1.ItemContainerGenerator.ContainerFromItem(data) as ListBoxItem;

			//Usuario userSelected = (sender as ListBox).SelectedItem as Usuario;
			//ListBoxItem selectedItem = this.listBox1.ItemContainerGenerator.ContainerFromItem(userSelected) as ListBoxItem;

            //NavigationService.Navigate(new Uri("/SelectTypePivot.xaml?tipojuego=clasico", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //TipoJuego data = (sender as Button).DataContext as TipoJuego;
          //  NavigationService.Navigate(new Uri("/SelectTypePivot.xaml?tipojuego="+data.Value, UriKind.Relative));
        }

		

		private void textBox1_GotFocus(object sender, RoutedEventArgs e)
		{
			desmarcar();
			listBox1.Visibility = Visibility.Collapsed;
		}

		private void textBox1_LostFocus(object sender, RoutedEventArgs e)
		{
			
			listBox1.Visibility = Visibility.Visible;
			
		}

		void desmarcar()
		{
			
			var items = (List<Usuario>)listBox1.ItemsSource;
			if(items != null)
			{
				var selectedItem = items.Where(x => x.Selected).FirstOrDefault();
		
				if(selectedItem != null)
				{
					if(selectedItem.Selected == true)
					{
						selectedItem.Selected = false;
						(App.Current as App).CurrentUser = null;
						loadUsers();
					}
				}
			}
					
					
			
		}
		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			textBox1.Text ="";
		}

        private void appbarSave_Click(object sender, EventArgs e)
        {
            var items = (List<Usuario>)listBox1.ItemsSource;

            var selectedItem = items != null ? items.Where(x => x.Selected).FirstOrDefault() : null;

            if ((String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrWhiteSpace(textBox1.Text)) && selectedItem == null)
                MessageBox.Show(Resource.CompleteInformacion);
            else
            {

                String Nombre = "";
                String Id = "";
                if (selectedItem != null)
                    Nombre = selectedItem.Nombre;
                else
                    Nombre = textBox1.Text;



                try
                {

                    if (selectedItem == null)
                    {

                        //ContextoDatos.DeleteDB();
                        ContextoDatos.CrearDBSiNoExiste();
                        User user = new User()
                        {
                            Nombre = textBox1.Text
                        };

                        DataUser datauser = new DataUser()
                        {
                            Altura = App.IsMetric ? Conversion.ConverDouble(dataAltura) : Conversion.ToCentimetros(Conversion.ConverDouble(dataAltura.Split('.').First()),
                                                                                                                 Conversion.ConverDouble(dataAltura.Split('.').Last())),
                            Edad = Convert.ToInt32(dataEdad),
                            Fecha =Convert.ToDateTime(datePicker1.Value),
                            Genero = dataGenero,
                            Indice = Conversion.ConverDouble(dataIndice),
                            //Peso = App.IsMetric ? Convert.ToDouble(dataPeso) : Conversion.ToKilogramos(Convert.ToDouble(dataPeso)),
							 Peso = App.IsMetric ? Convert.ToDouble(dataPeso) : Conversion.ToKilogramos(Convert.ToDouble(dataPeso)),
                            
                            User = user
                        };


                        using (ContextoDatos ctx = new ContextoDatos())
                        {
                            ctx.GetTable<User>().InsertOnSubmit(user);
                            ctx.GetTable<DataUser>().InsertOnSubmit(datauser);
                            ctx.SubmitChanges();

                            Id = user.Id.ToString();
                        }
                    }
                    else
                    {
                        DataUser datauser = new DataUser();
                        
                        datauser.Altura = App.IsMetric ? Conversion.ConverDouble(dataAltura)  : Conversion.ToCentimetros(Conversion.ConverDouble(dataAltura.Split('.').First()),
                                                                                                                 Conversion.ConverDouble(dataAltura.Split('.').Last()));
                        datauser.Edad	= Convert.ToInt32(dataEdad);
                        datauser.Fecha	= Convert.ToDateTime(datePicker1.Value);
                        datauser.Genero = dataGenero;
                        datauser.Indice = Conversion.ConverDouble(dataIndice);
                       // datauser.Peso	= App.IsMetric ? Convert.ToDouble(dataPeso)  : Conversion.ToKilogramos(Convert.ToDouble(dataPeso));
						//datauser.Peso	= App.IsMetric ? Convert.ToDouble(dataPeso.Replace(",",".")): Conversion.ToKilogramos(Convert.ToDouble(dataPeso.Replace(",",".")));
						datauser.Peso   = App.IsMetric?  Convert.ToDouble(dataPeso.Replace(System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator.ToString(), ".")):Conversion.ToKilogramos(Convert.ToDouble(dataPeso));

				

                        

                        using (ContextoDatos ctx = new ContextoDatos())
                        {
                            datauser.User = ctx.Users.Where(x => x.Id == selectedItem.Id).SingleOrDefault();

                            ctx.GetTable<DataUser>().InsertOnSubmit(datauser);
                            ctx.SubmitChanges();

                            Id = selectedItem.Id.ToString();
                        }
                    }

					(App.Current as App).CurrentUser = null;
                    MessageBox.Show(Resource.DataSaved);
                    NavigationService.Navigate(new Uri("/DataUsers.xaml?Id=" + Id, UriKind.Relative));
                    //loadUsers();
                }
                catch (DbException x)
                {
                    MessageBox.Show("Error al insertar: " + x.Message);
                }
            }
        }

        private void appbarCancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
	}
}