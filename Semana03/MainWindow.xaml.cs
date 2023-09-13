using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Semana03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            McDataGrid.ItemsSource = ListarEstudiantesListaObjetos();

        }
        /// List of Authors
        /// </summary>
        /// <returns></returns>
        /// 
        public static string connectionString = "Data Source=LAB1504-28\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=admin;Password=admin";
      
        private static List<Student> ListarEstudiantesListaObjetos()
        {
            List<Student> empleados = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Consulta SQL para seleccionar datos
                string query = "SELECT StudentId,FirstName,LastName FROM Students";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verificar si hay filas
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Lista de Estudiantes:");
                            while (reader.Read())
                            {
                                // Leer los datos de cada fila

                                empleados.Add(new Student
                                {
                                    StudentId = (int)reader["StudentId"],
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString()
                                });

                            }
                        }
                    }
                }
                // Cerrar la conexión
                connection.Close();


            }
            return empleados;

        }
        private void FilterByFirstName_Click(object sender, RoutedEventArgs e)
        {
            string firstNameFilter = FirstNameFilterTextBox.Text;


            List<Student> filteredStudents = ListarEstudiantesListaObjetos().Where(student => student.FirstName.Contains(firstNameFilter)).ToList();


            McDataGrid.ItemsSource = filteredStudents;
        }

        private void RowColorButton_Click(object sender, RoutedEventArgs e)
        {
      
        }
    }
}
