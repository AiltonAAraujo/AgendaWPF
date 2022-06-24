using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;


namespace UseAgenda
{
 
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            
            client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/api/clientes");
                response.EnsureSuccessStatusCode(); 
                var clientes = await response.Content.ReadAsAsync<IEnumerable<Cliente>>();
                clientesListView.ItemsSource = clientes;
            }
            catch (Exception)
            {
                //MessageBox.Show("Erro : " + ex.Message);
            }
        }
                
        private async void btnGetCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/api/clientes/" + txtID.Text);
                response.EnsureSuccessStatusCode(); 
                var clientes = await response.Content.ReadAsAsync<Cliente>();
                clienteDetalhesPanel.Visibility = Visibility.Visible;
                clienteDetalhesPanel.DataContext = clientes;
            }
            catch (Exception)
            {
                MessageBox.Show("Cliente não localizado");
            }
        }

        private async void btnNovoCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cliente = new Cliente()
                {
                    id = int.Parse(txtIDCliente.Text),
                    nome = txtNomeCliente.Text,                    
                    sobrenome = txtSobrenome.Text,
                    telefone = int.Parse(txtTelefone.Text)
                };
                var response = await client.PostAsJsonAsync("/api/clientes/", cliente);
                response.EnsureSuccessStatusCode();
                MessageBox.Show("Cliente incluído com sucesso", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                clientesListView.ItemsSource = await GetAllClientes();
                clientesListView.ScrollIntoView(clientesListView.ItemContainerGenerator.Items[clientesListView.Items.Count - 1]);
            }
            catch (Exception)
            {
                MessageBox.Show("O Cliente não foi incluído. (Verifique se o ID não esta duplicado)");
            }
        }

        private async void btnAtualiza_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cliente = new Cliente()
                {
                    id = int.Parse(txtIDCliente.Text),
                    nome = txtNomeCliente.Text,                    
                    sobrenome = txtSobrenome.Text,
                    telefone = int.Parse(txtTelefone.Text)
                };
                var response = await client.PostAsJsonAsync("/api/clientes/", cliente);
                response.EnsureSuccessStatusCode(); 
                MessageBox.Show("Cliente incluído com sucesso", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                clientesListView.ItemsSource = await GetAllClientes();
                clientesListView.ScrollIntoView(clientesListView.ItemContainerGenerator.Items[clientesListView.Items.Count - 1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDeletaCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("/api/clientes/" + txtID.Text);
                response.EnsureSuccessStatusCode(); 
                MessageBox.Show("Cliente deletado com sucesso");
                clientesListView.ItemsSource = await GetAllClientes();
                clientesListView.ScrollIntoView(clientesListView.ItemContainerGenerator.Items[clientesListView.Items.Count - 1]);
            }
            catch (Exception)
            {
                MessageBox.Show("Cliente deletado com sucesso");
            }
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            HttpResponseMessage response = await client.GetAsync("/api/clientes");
            response.EnsureSuccessStatusCode(); 
            var clientes = await response.Content.ReadAsAsync<IEnumerable<Cliente>>();
            return clientes;
        }
    }
}
