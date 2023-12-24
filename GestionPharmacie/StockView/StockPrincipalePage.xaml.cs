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

namespace GestionPharmacie.StockView
{
    /// <summary>
    /// Logique d'interaction pour StockPrincipalePage.xaml
    /// </summary>
    public partial class StockPrincipalePage : Page
    {
        public StockPrincipalePage()
        {
            InitializeComponent();
            DbContext = new AppDataContext();
            listeMedicament = DbContext.GetMedicament();
            datagridMedicament.ItemsSource = listeMedicament;
            seuil = DbContext.GetSeuil();
            labNbMedicamentExpier.Content = listeMedicament.Where(m => m.DateExpiration.AddDays(seuil.Nbjour) >= DateTime.Now).Count();
            labNbMedicamentManqueStock.Content = listeMedicament.Where(m => m.QuantiterStock <= seuil.NbQuantite).Count();
            txtNbJour.Text = seuil.Nbjour.ToString();
            txtQuManque.Text = seuil.NbQuantite.ToString();
            
            
        }
        Seuil seuil;
        AppDataContext DbContext;
        List<Medicament> listeMedicament;
        private void BtnAjouter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(!DpDateContrat.SelectedDate.HasValue)
                {
                    throw new Exception("La date d'expiration doit etre selectionner");
                }
                Medicament NvMedicament = new Medicament(-1, txtDesignation.Text, DpDateContrat.SelectedDate.Value, 0);
                DbContext.insertMedicament(NvMedicament);
                listeMedicament = DbContext.GetMedicament();
                datagridMedicament.ItemsSource = listeMedicament;
                labNbMedicamentExpier.Content = listeMedicament.Where(m => m.DateExpiration.AddDays(seuil.Nbjour) >= DateTime.Now).Count();
                labNbMedicamentManqueStock.Content = listeMedicament.Where(m => m.QuantiterStock <= seuil.NbQuantite).Count();
                txtDesignation.Text = "";
                DpDateContrat.SelectedDate = null;
                ExpNvMedic.IsExpanded = false;
                MessageBox.Show("Médicament été bien ajouter");


            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }
        }

        private void BtAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtDesignation.Text = "";
            DpDateContrat.SelectedDate = null;
            ExpNvMedic.IsExpanded = false;
        }

        private void BtnEdition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Model.Medicament MdfMedicament = (Medicament)((MyControlLibrary.Button)sender).Tag;
            StockView.ModifierStockWindow ModifierStockFenetre = new ModifierStockWindow(MdfMedicament);
            if(ModifierStockFenetre.ShowDialog().Value)
            {
                listeMedicament = DbContext.GetMedicament();
                datagridMedicament.ItemsSource = listeMedicament;
                labNbMedicamentExpier.Content = listeMedicament.Where(m => m.DateExpiration.AddDays(seuil.Nbjour) >= DateTime.Now).Count();
                labNbMedicamentManqueStock.Content = listeMedicament.Where(m => m.QuantiterStock <= seuil.NbQuantite).Count();
            }
        }

        private void BtnAfficher_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (RadioExpirer.IsChecked.Value)
            {
                datagridMedicament.ItemsSource = listeMedicament.Where(m => m.DateExpiration.AddDays(seuil.Nbjour) >= DateTime.Now);
            }
            else if(RadioManqueStock.IsChecked.Value)
            {
                datagridMedicament.ItemsSource = listeMedicament.Where(m => m.QuantiterStock <= seuil.NbQuantite);
            }
            else if(RadioTous.IsChecked.Value)
            {
                datagridMedicament.ItemsSource = listeMedicament;
            }
        }

        private void BtnValiderSeuil_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int NbQu;
                int NbJour;
                if (!int.TryParse(txtQuManque.Text, out NbQu))
                {
                    throw new Exception("La quantité du stock doit etre numerique");
                }
                if (!int.TryParse(txtNbJour.Text,out NbJour))
                {
                    throw new Exception("Le nombre de jour doit etre numerique");
                }
                DbContext.UpdateSeuil(new Seuil() { Nbjour = NbJour, NbQuantite = NbQu });
                seuil = DbContext.GetSeuil();
                MessageBox.Show("La seuil bien modifier");
                labNbMedicamentExpier.Content = listeMedicament.Where(m => m.DateExpiration.AddDays(seuil.Nbjour) >= DateTime.Now).Count();
                labNbMedicamentManqueStock.Content = listeMedicament.Where(m => m.QuantiterStock <=seuil.NbQuantite).Count();
            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }
        }

        private void BtAnnulerSeuil_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
