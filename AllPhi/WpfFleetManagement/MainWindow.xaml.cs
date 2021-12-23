using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer.Managers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FleetDatabase;
using System.Configuration;
using WpfFleetManagement.Voertuig;
using WpfFleetManagement.Tankkaart;

namespace WpfFleetManagement {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public static BestuurderManager bestuurderManager;
        public static TankkaartManager tankkaartManager;
        public static VoertuigManager voertuigManager;
        public string connectionstring;

        public MainWindow() {
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            connectionstring = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            bestuurderManager = new BestuurderManager(new BestuurderRepositoryADO(connectionstring));
            tankkaartManager = new TankkaartManager(new TankkaartRepositoryADO(connectionstring));
            voertuigManager = new VoertuigManager(new VoertuigRepositoryADO(connectionstring));
        }

        private void Button_Bestuurder_Click(object sender, RoutedEventArgs e)
        {
            BestuurderWindow bestuurderWindow = new BestuurderWindow();
            bestuurderWindow.Show();
            this.Close();
        }

        private void Button_Voertuig_Click(object sender, RoutedEventArgs e)
        {
            VoertuigWindow voertuigWindow = new VoertuigWindow();
            voertuigWindow.Show();
            this.Close();
        }

        private void Button_Tankkaart_Click(object sender, RoutedEventArgs e)
        {
            TankkaartWindow tankkaartWindow = new TankkaartWindow();
            tankkaartWindow.Show();
            this.Close();
        }
    }
}
