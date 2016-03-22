using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
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

namespace demo.SQLiteCostura
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
            // Parameter name: path1
            // Look at: http://stackoverflow.com/questions/35659171/sqlite-how-to-get-rid-of-annoying-trace-messages-native-library-pre-loader

            if (!File.Exists("demodb.sqlite"))
            {
                using (SQLiteConnection connect = new SQLiteConnection("Data Source = demodb.sqlite; Version = 3;"))
                {
                    connect.Open();

                    using (SQLiteCommand sqlc = connect.CreateCommand())
                    {
                        sqlc.CommandText = @"CREATE table highscores (name varchar(20), score int)";
                        sqlc.ExecuteNonQuery();

                        sqlc.CommandText = @"INSERT into highscores (name, score) values ('IDmedia', 9001)";
                        sqlc.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Database created!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connect = new SQLiteConnection("Data Source = demodb.sqlite; Version = 3;"))
            {
                connect.Open();
                using (SQLiteCommand sqlc = connect.CreateCommand())
                {
                    sqlc.CommandText = @"SELECT * FROM highscores";
                    SQLiteDataReader r = sqlc.ExecuteReader();
                    while (r.Read())
                    {
                        MessageBox.Show("Result: " + r["name"]);
                    }
                }
            }
        }
    }
}
