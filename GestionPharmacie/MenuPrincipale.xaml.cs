using GestionPharmacie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionPharmacie
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppDataContext DbContext = new AppDataContext();
            if(!DbContext.VerifiTrial())
            {
                OtherView.MessageWindow ActiWindow = new OtherView.MessageWindow();
                if(!ActiWindow.ShowDialog().Value)
                {
                    MessageBox.Show("Activation échoué");
                    Application.Current.Shutdown();

                }
            }

            DeclarationsView.NouvelleDeclaration NvDeclar = new DeclarationsView.NouvelleDeclaration();
            NvDeclar.ExpNvDeclaration.IsExpanded = true;
            Conteneur.NavigationService.RemoveBackEntry();
            Conteneur.NavigationService.Content = NvDeclar;
            Conteneur.NavigationService.RemoveBackEntry();
        }

        private void BtnDeclaration_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            DeclarationsView.NouvelleDeclaration NvDeclar = new DeclarationsView.NouvelleDeclaration();
            Conteneur.NavigationService.RemoveBackEntry();
            Conteneur.NavigationService.Content= NvDeclar;
            Conteneur.NavigationService.RemoveBackEntry();
        }

        private void BtnPersonnels_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PersonnelsView.PersonnelPrincipalePage PersPrincPage = new PersonnelsView.PersonnelPrincipalePage();
            Conteneur.NavigationService.RemoveBackEntry();
            Conteneur.NavigationService.Content = PersPrincPage;
            Conteneur.NavigationService.RemoveBackEntry();
        }

        private void BtnMedecins_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MedecinsView.NouvelMedecin MdecinPage = new MedecinsView.NouvelMedecin();
            Conteneur.NavigationService.Content = MdecinPage;
            Conteneur.NavigationService.RemoveBackEntry();
        }

        private void BtnMouvements_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouvementView.MouvementPrincipalePage MouvementPage = new MouvementView.MouvementPrincipalePage();
            Conteneur.NavigationService.Content = MouvementPage;
            Conteneur.NavigationService.RemoveBackEntry();
        }

        private void BtnStock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StockView.StockPrincipalePage StockPrincipalePage = new StockView.StockPrincipalePage();
            Conteneur.NavigationService.Content = StockPrincipalePage;
            Conteneur.NavigationService.RemoveBackEntry();
        }
    }
}
