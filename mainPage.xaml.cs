using Oracle.ManagedDataAccess.Client;
using SemPrace_BDAS2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
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
    /// Interakční logika pro mainPage.xaml
    /// </summary>
    public partial class mainPage : Window
    {
        public mainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String connectionString = "User Id=st67040;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)));";

            OracleConnection con = new OracleConnection();
            con.ConnectionString = connectionString;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select nazev from typ_pojisteni where id_typ_pojisteni = 2";
            cmd.Connection = con;

            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();

            dr.Read();
            label.Content = dr.GetString(0);
        }

        private void btnBezPrihlaseni_Click(object sender, RoutedEventArgs e)
        {
            Page1 page1 = new Page1();

            Window newWindow = new Window
            {
                Content = page1,

                Width = 800,
                Height = 450,

                Title = "Nepřihlášený uživatel",
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            newWindow.Show();

            this.Hide();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = TextBoxEmail.Text;
            string password = PasswordBoxPassword.Password;

            String connectionString = "User Id=st67040;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)));";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM Zamestnanec WHERE heslo = :heslo AND Email = :Email ", con))
                    {
                        cmd.Parameters.Add("Email", OracleDbType.Varchar2).Value = email;
                        cmd.Parameters.Add("heslo", OracleDbType.Varchar2).Value = password;

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userCount == 1)
                        {
                            MessageBox.Show("Přihlášení úspěšné!");
                            // Provést další akce po přihlášení

                            // Zde můžete přidat kód pro skrytí nebo zavření aktuálního okna a otevření nového okna pro přihlášeného uživatele.
                            // Například:
                            LoggedUser loggedUser = new LoggedUser();
                            Window newWindow = new Window
                            {
                                Content = loggedUser,
                                Width = 1200,
                                Height = 550,
                                Title = "Přihlášený uživatel",
                                WindowStartupLocation = WindowStartupLocation.CenterScreen
                            };

                            newWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Chybný email nebo heslo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba při přihlašování: {ex.Message}");
                }
            }
        }


    }
}

