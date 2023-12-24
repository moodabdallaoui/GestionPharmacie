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

namespace GestionPharmacie.OtherView
{
    /// <summary>
    /// Logique d'interaction pour MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow()
        {
            InitializeComponent();

        }

        private void BtnActiver_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AppDataContext db = new AppDataContext();
            db.EntrerKey(txtKey.Text);
            if (db.VerifiTrial())
                DialogResult = true;
            else
                DialogResult = false;
        }
    }
}
