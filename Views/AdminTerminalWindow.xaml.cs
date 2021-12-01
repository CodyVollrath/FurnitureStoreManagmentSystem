using FurnitureStoreManagmentSystem.ViewModel;
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

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    /// Interaction logic for AdminPortalWindow.xaml
    /// </summary>
    public partial class AdminTerminalWindow : Window
    {
        private AdminTerminalViewModel adminPortalVM;
        public AdminTerminalWindow()
        {
            InitializeComponent();
            this.adminPortalVM = new AdminTerminalViewModel();
            DataContext = adminPortalVM;
            this.sqlDataGridView.Visibility = Visibility.Collapsed;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            this.adminPortalVM.SendCommand();
            this.txtOutput.Text = this.adminPortalVM.Output;
            this.populateGridView(this.adminPortalVM.Output);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void populateGridView(string output) 
        {
            this.clearDataGrid();
            this.sqlDataGridView.Visibility = Visibility.Visible;
            try 
            {
                var lines = output.Split('\n');
                var columns = lines[0].Split(',');
                Dictionary<string, string> dataMap = new Dictionary<string, string>();
                foreach (var column in columns) 
                {
                    DataGridTextColumn textColumn = new DataGridTextColumn();
                    textColumn.Header = column;
                    textColumn.Binding = new Binding(column.Replace(" ", ""));
                    this.sqlDataGridView.Columns.Add(textColumn);
                }
                for (int rowIndex = 1; rowIndex < lines.Length; rowIndex++) 
                {
                    var line = lines[rowIndex];
                    var fields = line.Split(',');
                    dynamic exo = new System.Dynamic.ExpandoObject();
                    for (int colIndex = 0; colIndex < fields.Length; colIndex++) 
                    {
                        var columnname = columns[colIndex].Replace(" ", "");
                        ((IDictionary<string, object>) exo).Add(columnname, $"{fields[colIndex]}");
                        
                    }
                    this.sqlDataGridView.Items.Add(exo);

                }
            } 
            catch (Exception ex)
            {
                this.txtOutput.Text = ex.Message;
                this.clearDataGrid();
                this.sqlDataGridView.Visibility = Visibility.Collapsed;
            }
            
        }

        private void clearDataGrid() 
        {
            this.sqlDataGridView.Columns.Clear();
            this.sqlDataGridView.Items.Clear();
        }
    }
}
