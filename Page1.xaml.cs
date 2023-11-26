using Oracle.ManagedDataAccess.Client;
using SemPrace_BDAS2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SemPrace_BDAS2
{
    /// <summary>
    /// Interakční logika pro Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public NavigationService Page1NavigationService { get; set; }
        public Page1()
        {
            InitializeComponent();
        }


        private ObservableCollection<Obcan> LoadDataFromDatabase()
        {
            ObservableCollection<Obcan> obcane = new ObservableCollection<Obcan>();

            String connectionString = "User Id=st67040;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)));";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = "SELECT jmeno, prijmeni FROM obcan";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Obcan obcan = new Obcan
                            {
                                Jmeno = dr["jmeno"].ToString(),
                                Prijmeni = dr["prijmeni"].ToString(),
                            };

                            obcane.Add(obcan);
                        }
                    }
                }
            }

            return obcane;
        }

        private void buttonCitizents_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = LoadDataFromDatabase();
            DataGrid.Columns[0].Visibility = Visibility.Collapsed;
            DataGrid.Columns[6].Visibility = Visibility.Collapsed;
            DataGrid.Columns[7].Visibility = Visibility.Collapsed;
            DataGrid.Columns[12].Visibility = Visibility.Collapsed;
            DataGrid.Columns[14].Visibility = Visibility.Collapsed;


        }

        private void buttonOffices_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = LoadOfficesFromDatabase();
        }
        private ObservableCollection<Adresa> LoadOfficesFromDatabase()
        {
            ObservableCollection<Adresa> addresses = new ObservableCollection<Adresa>();

            String connectionString = "User Id=st67040;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)));";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = "SELECT p.nazev, a.ulice, a.cislo_popisne " +
                                      "FROM pobocka p " +
                                      "JOIN adresa a ON p.id_adresa = a.id_adresa";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Adresa adresa = new Adresa
                            {
                                Nazev = dr["nazev"].ToString(),
                                Ulice = dr["ulice"].ToString(),
                                CisloPopisne = dr["cislo_popisne"].ToString(),
                            };
                            addresses.Add(adresa);
                        }
                    }
                }
            }

            return addresses;
        }

        private void buttonEnd_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Opravdu chcete ukončit aplikaci?", "Potvrzení", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void buttonInsurance_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = LoadInsuranceTypesFromDatabase();
            DataGrid.Columns[0].Visibility = Visibility.Collapsed;
            DataGrid.Columns[2].Visibility = Visibility.Collapsed;


        }
        private ObservableCollection<TypPojisteni> LoadInsuranceTypesFromDatabase()
        {
            ObservableCollection<TypPojisteni> insuranceTypes = new ObservableCollection<TypPojisteni>();

            String connectionString = "User Id=st67040;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)));";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = "SELECT tp.id_typ_pojisteni, tp.nazev, tp.id_pojistovna, p.nazev AS pojistovna_nazev " +
                  "FROM TYP_POJISTENI tp " +
                  "INNER JOIN POJISTOVNA p ON tp.id_pojistovna = p.id_pojistovna";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TypPojisteni insuranceType = new TypPojisteni
                            {
                                IdTypPojisteni = Convert.ToInt32(dr["id_typ_pojisteni"]),
                                Nazev = dr["nazev"].ToString(),
                                IdPojistovna = Convert.ToInt32(dr["id_pojistovna"]),
                                NazevPojistovna = dr["pojistovna_nazev"].ToString()
                            };

                            insuranceTypes.Add(insuranceType);
                        }
                    }
                }
            }

            return insuranceTypes;
        }

        private void buttonBackLogin_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
            mainPage mainPage = new mainPage();
            mainPage.Show();
        }
    }
}

