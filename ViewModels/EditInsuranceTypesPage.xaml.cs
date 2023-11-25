using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace SemPrace_BDAS2.ViewModels
{
    /// <summary>
    /// Interakční logika pro EditInsuranceTypesPage.xaml
    /// </summary>
    public partial class EditInsuranceTypesPage : Page
    {
        private int idInsuranceType;
        private int idInsurenceCompany;
        public event EventHandler DataUpdated;
        public EditInsuranceTypesPage(int idInsuranceType, int idInsurenceCompany, string nazev, string nazevPojistovna)
        {
            InitializeComponent();
            textBoxNazev.Text = nazev;
            textBoxNazevPojistovny.Text = nazevPojistovna;
            this.idInsuranceType = idInsuranceType;
            this.idInsurenceCompany = idInsurenceCompany;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string updatedNazev = textBoxNazev.Text;
            string updatedNazevPojistovna = textBoxNazevPojistovny.Text;
            UpdateInsuranceCompanyInDatabase(idInsurenceCompany, updatedNazevPojistovna);
            UpdateInsuranceTypeInDatabase(idInsuranceType, updatedNazev);

            MessageBox.Show("Změny byly uloženy.");
            Window.GetWindow(this).Close();
            //LoggedUser logged = new LoggedUser();
            //logged.buttonInsurance_Click(sender, e);
        }

        private void UpdateInsuranceTypeInDatabase(int idInsuranceType, string updatedNazev)
        {
            String connectionString = "User Id=st67040;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)));";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = "UPDATE TYP_POJISTENI SET nazev = :updatedNazev WHERE id_typ_pojisteni = :id";
                    cmd.Parameters.Add(new OracleParameter("updatedNazev", updatedNazev));
                    cmd.Parameters.Add(new OracleParameter("id", idInsuranceType));
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void UpdateInsuranceCompanyInDatabase(int idInsuranceCompany, string updatedNazev)
        {
            String connectionString = "User Id=st67040;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)));";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = "UPDATE POJISTOVNA SET nazev = :updatedNazev WHERE id_pojistovna = :id";
                    cmd.Parameters.Add(new OracleParameter("updatedNazev", updatedNazev));
                    cmd.Parameters.Add(new OracleParameter("id", idInsuranceCompany));
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

