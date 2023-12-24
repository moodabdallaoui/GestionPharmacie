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
using System.Windows.Shapes;
using GestionPharmacie.Model;

namespace GestionPharmacie.StockView
{
    /// <summary>
    /// Logique d'interaction pour ModifierStockWindow.xaml
    /// </summary>
    public partial class ModifierStockWindow : Window
    {
        public ModifierStockWindow(Medicament MdfMedicament)
        {
            InitializeComponent();
            this.MdfMedicament = MdfMedicament;
            DbContext = new AppDataContext();
            txtDesignation.Text = MdfMedicament.Designation;
            DpDateExpiration.SelectedDate = MdfMedicament.DateExpiration;
            txtQuantitéStock.Text = MdfMedicament.QuantiterStock.ToString();

        }
        AppDataContext DbContext;
        Medicament MdfMedicament;
        private void BtnSupprimer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(DbContext.GetDetailMouvement().Where(m=>m.Idmedicament==MdfMedicament.Id).Count()!=0)
            {
                MessageBox.Show("Impossible de supprimer il est deja utiliser\ndans plusieurs enregistrement");
            }
            else
            {
                DbContext.DeleteMedicament(MdfMedicament.Id);
                MessageBox.Show("Médicament étè bien supprimer");
                DialogResult = true;
            }
        }

        private void BtnValider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                int Qs;
                if (!DpDateExpiration.SelectedDate.HasValue)
                {
                    throw new Exception("La date d'expiration doit etre selectionner");
                }
                if (!int.TryParse(txtQuantitéStock.Text,out Qs))
                {
                    throw new Exception("La quantité  doit etre numerique");
                }
                Medicament NvMedicament = new Medicament(MdfMedicament.Id, txtDesignation.Text, DpDateExpiration.SelectedDate.Value, Qs);
                DbContext.UpdateMedicament(NvMedicament);
                MessageBox.Show("Médicament bine modifier");
                DialogResult = true;


            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }
        }

        private void BtAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }
    }
}
