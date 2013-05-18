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
    public partial class ListadoUsuarios : PhoneApplicationPage
    {
        string loadData;
        List<IndiceActividad> indiceactivida = new List<IndiceActividad>();
        public ListadoUsuarios()
        {
            InitializeComponent();
            loadUsers();

			(ApplicationBar.Buttons[0] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Home_Text;
            (ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Edit;
            (ApplicationBar.Buttons[2] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.EliminarCaptionText;
            (ApplicationBar.Buttons[3] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.DataUser_Text;

            String p1 = Resource.Indice_PersonaSedentaria;
            String p2 = Resource.Indice_Actividadfisicaligera.ToString();
            String p3 = Resource.Indice_ActividadfisicaMedia.ToString();
            String p4 = Resource.Indice_Personamuyactiva.ToString();
            String p5 = Resource.Indice_Personaextremadamenteactiva.ToString();

            indiceactivida.Add(new IndiceActividad() { indice = 1.0, descripcion = p1 });
            indiceactivida.Add(new IndiceActividad() { indice = 1.2, descripcion = p2 });
            indiceactivida.Add(new IndiceActividad() { indice = 1.4, descripcion = p3 });
            indiceactivida.Add(new IndiceActividad() { indice = 1.6, descripcion = p4 });
            indiceactivida.Add(new IndiceActividad() { indice = 1.8, descripcion = p5 });
        }

        void loadUsers()
        {
            listBox1.ItemsSource = null;
            List<Usuario> listaUsuarios = new List<Usuario>();
            ContextoDatos ctx = new ContextoDatos();
            var data = ctx.Users.OrderBy(o => o.Nombre);
			if(ctx.DatabaseExists())
			{
				foreach (User user in data)
				{
					listaUsuarios.Add(new Usuario { Id = user.Id, Nombre = user.Nombre });
				}

				listBox1.ItemsSource = listaUsuarios;
			}
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("loadData", out loadData);
            if (loadData == "true")
            {
                (ApplicationBar.Buttons[0] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = false;
                (ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = false;
                (ApplicationBar.Buttons[2] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = false;
                (ApplicationBar.Buttons[3] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = false;
            }

            //NavigationService.RemoveBackEntry();

           
        }
        public Usuario userSelected;
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //Get the data object that represents the current selected item

            //TipoJuego data = (sender as ListBox).SelectedItem as TipoJuego;

            //Get the selected ListBoxItem container instance    
            //ListBoxItem selectedItem = this.listBox1.ItemContainerGenerator.ContainerFromItem(data) as ListBoxItem;

            userSelected = (sender as ListBox).SelectedItem as Usuario;
            if (loadData == "true")
            {
                //Metric

                String AlturaMetrico = "";
                //Ingles
                double Pulgadas;
                double Pies;
                String AlturaIngles = "";

                DataUser userData = new DataUser();
                using (ContextoDatos ctx = new ContextoDatos())
                {
                    var user = ctx.Users.Where(x => x.Id == userSelected.Id).FirstOrDefault();
                    userData = ctx.Datas.Where(x => x.IdUsuario == userSelected.Id).OrderByDescending(o=>o.Fecha).First();

                    if (App.IsMetric)
                    {
                        double altura1 = Math.Floor(userData.Altura);
                        double altura2 = Math.Round(altura1 / 100, 2);
                        AlturaMetrico = String.Format("{0:0.00}", altura2);
                    }
                    else
                    {
                        Conversion.ToPies(userData.Altura, out Pies, out Pulgadas);
                        AlturaIngles = String.Format("{0}.{1}", Pies, Pulgadas);
                    }
                        
                }

                if (App.IsMetric)
                {
                    NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + indiceactivida.Where(o => o.indice == userData.Indice).SingleOrDefault().descripcion + "&dataGenero=" + userData.Genero + "&dataIndice=" + userData.Indice + "&dataEdad=" + userData.Edad + "&dataAltura=" + AlturaMetrico + "&dataPeso=" + Math.Floor(userData.Peso).ToString(), UriKind.Relative));
                }
                else 
                {
                    NavigationService.Navigate(new Uri("/MainPage.xaml?dataIndiceDesc=" + indiceactivida.Where(o => o.indice == userData.Indice).SingleOrDefault().descripcion + "&dataGenero=" + userData.Genero + "&dataIndice=" + userData.Indice + "&dataEdad=" + userData.Edad + "&dataAltura=" + AlturaIngles + "&dataPeso=" + Math.Floor(Conversion.ToLibras(userData.Peso)).ToString(), UriKind.Relative));
                }
                
            }
            //ListBoxItem selectedItem = this.listBox1.ItemContainerGenerator.ContainerFromItem(userSelected) as ListBoxItem;

            //NavigationService.Navigate(new Uri("/SelectTypePivot.xaml?tipojuego=clasico", UriKind.Relative));
        }

        private void delete_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Usuario data = (sender as Image).DataContext as Usuario;

            string id = data.Id.ToString();
            //NavigationService.Navigate(new Uri("/SelectTypePivot.xaml?tipojuego=" + data.Value, UriKind.Relative));
        }

        private void appbarDelete_Click(object sender, EventArgs e)
        {


            if (userSelected == null)
            {
                MessageBox.Show(Resource.EliminarNoSelected);
            }
            else
            {
                if (MessageBox.Show(Resource.EliminarMessageBoxText + " " + userSelected.Nombre, Resource.EliminarCaptionText, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    try
                    {
                        using (ContextoDatos ctx = new ContextoDatos())
                        {

                            var user        = ctx.Users.Where(x => x.Id == userSelected.Id).FirstOrDefault();
                            var userData    = ctx.Datas.Where(x => x.IdUsuario == userSelected.Id);

                            ctx.GetTable<DataUser>().DeleteAllOnSubmit(userData);
                            ctx.GetTable<User>().DeleteOnSubmit(user);

                            ctx.SubmitChanges();
                            loadUsers();
                        }
                    }
                    catch (DbException x)
                    {
                        MessageBox.Show("An error has occurred!, we worked to resolved this problem");
                    }
                }
            }
        }

        private void appbarEdit_Click(object sender, EventArgs e)
        {
            if (userSelected == null)
            {
                MessageBox.Show(Resource.EditarUserNoSelected);
            }
            else
            {
                NavigationService.Navigate(new Uri("/EditUser.xaml?Id=" + userSelected.Id, UriKind.Relative));
            }
        }

        private void appbarVer_Click(object sender, EventArgs e)
        {
            if (userSelected == null)
            {
                MessageBox.Show(Resource.EditarUserNoSelected);
            }
            else
            {
                NavigationService.Navigate(new Uri("/DataUsers.xaml?Id=" + userSelected.Id, UriKind.Relative));
            }
        }

		private void appbarHome_Click(object sender, EventArgs e)
		{
			 NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
		}
    }
}