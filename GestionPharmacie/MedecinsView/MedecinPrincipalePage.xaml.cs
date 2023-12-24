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
using GestionPharmacie.Model;

namespace GestionPharmacie.MedecinsView
{
    /// <summary>
    /// Logique d'interaction pour NouvelMedecin.xaml
    /// </summary>
    public partial class NouvelMedecin : Page
    {
        public NouvelMedecin()
        {
            InitializeComponent();
            DbContext = new AppDataContext();
            List<Medecin> listeMedecins = DbContext.GetMedecint();
            datagridMedecins.ItemsSource = listeMedecins;
           
        }
        AppDataContext DbContext ;
        private void BtnAjouter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Medecin NvMedecin = new Medecin(-1,txtNom.Text, txtPrenom.Text, txtAdresse.Text);
            if (DbContext.insertmedecin(NvMedecin))
            {
                MessageBox.Show("Médecin bien ajoute");
                datagridMedecins.ItemsSource = DbContext.GetMedecint();
                ClearMedecin();
                ExpanderNvMedecin.IsExpanded = false;
            }
            else
                MessageBox.Show("echec d'enregistrement");
        }
        private void ClearMedecin()
        {
            txtNom.Clear();
            txtPrenom.Clear();
            txtAdresse.Clear();
        }
        private void BtAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearMedecin();
            ExpanderNvMedecin.IsExpanded = false;
        }

        private void BtnEdition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Medecin MedecinMdf = (Medecin)((MyControlLibrary.Button)sender).Tag;
            MedecinsView.ModifierMedecin ModifierMedecinWindow = new ModifierMedecin(MedecinMdf);
            if (ModifierMedecinWindow.ShowDialog().Value)
                datagridMedecins.ItemsSource = DbContext.GetMedecint();

        }
    }
}