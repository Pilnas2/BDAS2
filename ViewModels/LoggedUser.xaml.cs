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

namespace SemPrace_BDAS2.ViewModels
{
    /// <summary>
    /// Interakční logika pro LoggedUser.xaml
    /// </summary>
    public partial class LoggedUser : Page
    {
        public LoggedUser()
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
                    cmd.CommandText = "SELECT o.jmeno, o.prijmeni, o.rodne_cislo, o.datum_narozeni, o.je_zamestnan, o.id_adresa, o.id_narodnost, o.id_pohlavi, a.id_obec " +
                              "FROM obcan o " +
                              "JOIN adresa a ON o.id_adresa = a.id_adresa";

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
                                RodneCislo = dr["rodne_cislo"].ToString(),
                                DatumNarozeni = dr["datum_narozeni"].ToString(),
                                JeZamestnan = (dr["je_zamestnan"].ToString() == "1") ? true : false,
                                IdAdresa = Convert.ToInt32(dr["id_adresa"]),
                                IdObec = Convert.ToInt32(dr["id_obec"]),
                                IdNarodnost = Convert.ToInt32(dr["id_narodnost"]),
                                IdPohlavi = Convert.ToInt32(dr["id_pohlavi"]),

                                // Initialize address properties
                                Ulice = "",
                                CisloPopisne = 0,
                                Stat = "",
                                Nazev = ""
                            };
                            using (OracleCommand narodnostCmd = new OracleCommand())
                            {
                                narodnostCmd.CommandText = "SELECT nazev FROM narodnost WHERE id_narodnost = :idNarodnost";
                                narodnostCmd.Connection = con;
                                narodnostCmd.CommandType = CommandType.Text;
                                narodnostCmd.Parameters.Add(new OracleParameter(":idNarodnost", obcan.IdNarodnost));

                                using (OracleDataReader narodnostDr = narodnostCmd.ExecuteReader())
                                {
                                    if (narodnostDr.Read())
                                    {
                                        obcan.Narodnost = narodnostDr["nazev"].ToString();
                                    }
                                }
                            }
                            using (OracleCommand pohlaviCmd = new OracleCommand())
                            {
                                pohlaviCmd.CommandText = "SELECT nazev FROM pohlavi WHERE id_pohlavi = :idPohlavi";
                                pohlaviCmd.Connection = con;
                                pohlaviCmd.CommandType = CommandType.Text;
                                pohlaviCmd.Parameters.Add(new OracleParameter(":idPohlavi", obcan.IdPohlavi));

                                using (OracleDataReader pohlaviDr = pohlaviCmd.ExecuteReader())
                                {
                                    if (pohlaviDr.Read())
                                    {
                                        obcan.Pohlavi = pohlaviDr["nazev"].ToString();
                                    }
                                }
                            }


                            // Load address information based on id_adresa
                            using (OracleCommand addressCmd = new OracleCommand())
                            {
                                addressCmd.CommandText = "SELECT ulice, cislo_popisne, stat, id_obec FROM adresa WHERE id_adresa = :idAdresa";
                                addressCmd.Connection = con;
                                addressCmd.CommandType = CommandType.Text;
                                addressCmd.Parameters.Add(new OracleParameter(":idAdresa", obcan.IdAdresa));

                                using (OracleDataReader addressDr = addressCmd.ExecuteReader())
                                {
                                    if (addressDr.Read())
                                    {
                                        obcan.Ulice = addressDr["ulice"].ToString();
                                        obcan.CisloPopisne = Convert.ToInt32(addressDr["cislo_popisne"]);
                                        obcan.Stat = addressDr["stat"].ToString();
                                        int idObec = Convert.ToInt32(addressDr["id_obec"]);

                                        // Load Obec name based on id_obec
                                        using (OracleCommand obecCmd = new OracleCommand())
                                        {
                                            obecCmd.CommandText = "SELECT nazev FROM obec WHERE id_obec = :idObec";
                                            obecCmd.Connection = con;
                                            obecCmd.CommandType = CommandType.Text;
                                            obecCmd.Parameters.Add(new OracleParameter(":idObec", idObec));

                                            using (OracleDataReader obecDr = obecCmd.ExecuteReader())
                                            {
                                                if (obecDr.Read())
                                                {
                                                    obcan.Nazev = obecDr["nazev"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

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
            DataGrid.Columns[5].Visibility = Visibility.Collapsed;
            DataGrid.Columns[6].Visibility = Visibility.Collapsed;
            DataGrid.Columns[11].Visibility = Visibility.Collapsed;


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
            DataGrid.Columns[1].Visibility = Visibility.Collapsed;
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
                    cmd.CommandText = "SELECT tp.nazev, tp.id_pojistovna, p.nazev AS pojistovna_nazev " +
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

        private void buttonInsuranceCard_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = LoadInsuranceCardsFromDatabase();

        }
        private ObservableCollection<PrukazPojistovny> LoadInsuranceCardsFromDatabase()
        {
            ObservableCollection<PrukazPojistovny> insuranceCards = new ObservableCollection<PrukazPojistovny>();

            String connectionString = "User Id=st67040;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)));";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = "SELECT pp.cislo_prukazu, pp.datum_vydani, pp.platnost_do, pp.id_obcan, pp.id_pojistovna, o.jmeno, o.prijmeni, poj.nazev AS pojistovna_nazev " +
                                      "FROM prukaz_pojistovny pp " +
                                      "JOIN obcan o ON pp.id_obcan = o.id_obcan " +
                                      "JOIN pojistovna poj ON pp.id_pojistovna = poj.id_pojistovna";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PrukazPojistovny insuranceCard = new PrukazPojistovny
                            {
                                CisloPrukazu = dr["cislo_prukazu"].ToString(),
                                DatumVydani = dr["datum_vydani"].ToString(),
                                PlatnostDo = dr["platnost_do"].ToString(),
                                //IdObcan = Convert.ToInt32(dr["id_obcan"]),
                                //IdPojistovna = Convert.ToInt32(dr["id_pojistovna"]),
                                //JmenoObcana = dr["jmeno"].ToString(),
                                //PrijmeniObcana = dr["prijmeni"].ToString(),
                                //TypPojistovny = new TypPojistovny
                                //{
                                //    Nazev = dr["pojistovna_nazev"].ToString()
                                //}
                            };

                            Obcan obcan = new Obcan
                            {
                                Jmeno = dr["jmeno"].ToString(),
                                Prijmeni = dr["prijmeni"].ToString(),
                                // other properties from Obcan
                            };

                            insuranceCards.Add(insuranceCard);
                        }
                    }
                }
            }

            return insuranceCards;
        }
    }
}

