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
    public partial class EditUser : PhoneApplicationPage
    {
        public string Id;
        public EditUser()
        {
            InitializeComponent();
			 (ApplicationBar.Buttons[0] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Users_Text;
             (ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).Text = Resource.Save_Text;
        }
       
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("Id", out Id);
            ShowUser();
        }
        void ShowUser()
        {
            using (ContextoDatos ctx = new ContextoDatos())
            {
                var user = ctx.Users.Where(x => x.Id == Convert.ToInt32(Id)).SingleOrDefault();
                txtUser.Text = user.Nombre;
            }
        }
        private void appbarSave_Click(object sender, EventArgs e)
        {
            if ((String.IsNullOrEmpty(txtUser.Text) || String.IsNullOrWhiteSpace(txtUser.Text)))
            {
                MessageBox.Show(Resource.CompleteInformacion);
            }
            else
            { 
                try
                {
                    using (ContextoDatos ctx = new ContextoDatos())
                    {

                        ctx.Users.Where(x => x.Id == Convert.ToInt32(Id)).SingleOrDefault().Nombre = txtUser.Text;
                        
                        ctx.SubmitChanges();

                        MessageBox.Show(Resource.DataSaved);
                        NavigationService.Navigate(new Uri("/ListadoUsuarios.xaml?", UriKind.Relative));
                    }
                }
                catch (DbException x)
                {
                    MessageBox.Show("Error al actualizar: " + x.Message);
                }
            }
        }

        private void appbarUser_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ListadoUsuarios.xaml?", UriKind.Relative));
        }
    }
}