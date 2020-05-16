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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LoginWPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void loopThroughDataT(DataTable element, List<string> lista)
        {
            foreach (DataRow dbRow in element.Rows)
            {
                foreach (DataColumn dbColumns in element.Columns)
                {
                    var field1 = dbRow[dbColumns].ToString();
                    //Console.WriteLine(field1);
                    lista.Add(field1);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnZaloguj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string loginString = "SERVER=localhost;DATABASE=db_system_realizacji_zamowien_posilkow;UID=root;PASSWORD=zaq1@WSX";
                MySqlConnection mysql = new MySqlConnection(loginString);
                mysql.Open();
                
                string ZapytanieSQL = "SELECT login, password FROM tb_workers;";
                MySqlDataAdapter AdapterSQL = new MySqlDataAdapter();
                AdapterSQL.SelectCommand = new MySqlCommand(ZapytanieSQL, mysql);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(AdapterSQL);

                DataTable n1 = new DataTable();
                AdapterSQL.Fill(n1);

                var Users = new List<string>(); // znajduja sie tu wszystkie elementy z bazy danych(cena, nazwa itd)
                loopThroughDataT(n1, Users);


                for (int i = 0; i < Users.Count; i += 2)
                {
                    if(txtUsername.Text == Users[i])
                    {
                        if (txtPassword.Password == Users[i + 1])
                        {
                            MessageBox.Show("Hello " + txtUsername.Text +"!");
                            break;
                        }
                    }
                    if (i > Users.Count - 3)
                        MessageBox.Show("Incorrect login or password. Try again.");
                }


                mysql.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
