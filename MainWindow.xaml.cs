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

namespace SemPrace_BDAS2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
    }
}
