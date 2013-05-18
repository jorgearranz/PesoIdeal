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
using Microsoft.Phone.Tasks;
namespace PesoIdeal
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly().FullName;
            var version = assembly.Split('=')[1].Split(',')[0];

            textBlock1.Text = "Version:" + version.ToString();
        }

        private void rate_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();
        }

        private void more_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceSearchTask task = new MarketplaceSearchTask();
            task.ContentType = MarketplaceContentType.Applications;

            task.SearchTerms = "COCOSOFT";
            task.Show();
        }
    }
}