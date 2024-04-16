using Avalonia.Controls;

namespace _1010frekSSS;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}

ОТОБРАЖЕНИЕ (предварительно создать класс)

private List<clients> _clients;
private string connStr = "server=localhost;database=ekz1604;port=3306;User Id=root;password=кщще;";
private MySqlConnection conn;

    public MainWindow()
    {
        InitializeComponent();
        string table = "select*from clients";
        ShowData(table);
        Filter();
    }

    public void ShowData(string sql)
    {
        _clients = new List<clients>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CCClients = new clients
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
            };
            _clients.Add(CCClients);
        }
        conn.Close();
        ClientsGrid.ItemsSource = _clients;
    }

РАЗМЕТКА ОТОБРАЖЕНИЯ
<StackPanel Orientation="Horizontal">
            <DataGrid Name="ClientsGrid" x:CompileBindings="False" Margin ="10 10 0 0" Width="600" Height="200">
                <DataGrid.Columns>
                    <DataGridTextColumn Header="id" Binding="{Binding id}"></DataGridTextColumn>

ФИЛЬТРАЦИЯ
private void Cmb_Phone(object? sender, SelectionChangedEventArgs e)
    {
        var phone = (ComboBox)sender;
        var CCClients = phone.SelectedItem as clients;
        var filtrPhone = _clients.Where(x => x.phone == CCClients.phone).ToList();
        ClientsGrid.ItemsSource = filtrPhone;
    }
    public void Filter()
    {
        _clients = new List<clients>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from clients", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CCClients = new clients()
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
            };
            _clients.Add(CCClients);
        }
        conn.Close();
        var cliCmb = this.Find<ComboBox>(name: "CmbPhone");
        cliCmb.ItemsSource = _clients;
    }

ФИЛЬТРАЦИЯ РАЗМЕТКА
<StackPanel Orientation="Horizontal">
                <TextBlock x:Name="FilterPhone" Margin ="10 10 0 0">Фильтрация по телефону: </TextBlock>
                <ComboBox Name="CmbPhone" Width="200" Margin ="10 10 0 0" x:CompileBindings="False" SelectionChanged="Cmb_Phone">
                    <ComboBox.ItemTemplate>
                        <DataTemplate>
                            <TextBlock Text="{Binding phone}"></TextBlock>

СОРТИРОВКА
 private void AZ_OnClick(object? sender, RoutedEventArgs e)
    {
        var sortedName = ClientsGrid.ItemsSource.Cast<clients>().OrderBy(s => s.name).ToList();
        ClientsGrid.ItemsSource = sortedName;
    }
    
    private void ZA_OnClick(object? sender, RoutedEventArgs e)
    {
        var sortedName = ClientsGrid.ItemsSource.Cast<clients>().OrderByDescending(s => s.name).ToList();
        ClientsGrid.ItemsSource = sortedName;
    }

ПЕРЕХОД
private void dolz(object? sender, RoutedEventArgs e)
    {
        var dolz = new dolz();
        dolz.Show();
        this.Hide();
    }

УДАЛЕНИЕ
private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            sotrudnik sssotr = SotrudnikGrid.SelectedItem as sotrudnik;
            if (sssotr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM sotrudnik WHERE id = " + sssotr.id;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            _sotr.Remove(sssotr);
            ShowTable("SELECT id, surname, name, lastname, dolznost_id FROM sotrudnik;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

УДАЛЕНИЕ РАЗМЕТКА
<TextBox Name="id" x:CompileBindings="False" Text="{Binding id}" Width="200" Height="5" Margin = "10 25 0 0"></TextBox>
                <Button Name="Delete" Margin = "10 25 0 0" Content="Удалить" Background="White" Click="DeleteData"></Button>

ДОБАВЛЕНИЕ И РЕДАКТИРОВАНИЕ
ГЛАВНАЯ ФОРМА
private void AddData(object? sender, RoutedEventArgs e)
    {
        sotrudnik newSotr = new sotrudnik();
        up1.add add = new up1.add(newSotr, _sotr);
        add.Show();
        this.Hide();
    }
ФОРМА ДОБАВЛЕНИЯ
private List<sotrudnik> Sssotr;
    private string connStr = "server=localhost;database=sed_up;port=3306;User Id=root;password=кщще";
    private MySqlConnection conn;
    private sotrudnik _Sotr;
    
    public add(sotrudnik sssotr, List<sotrudnik> _sotr)
    {
        InitializeComponent();
        _Sotr = sssotr;
        this.DataContext = _Sotr;
        Sssotr = _sotr;
    }
    
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var zzz = Sssotr.FirstOrDefault(x => x.id == _Sotr.id);
        if (zzz == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO sotrudnik (id, surname, name, lastname, dolznost_id) VALUES (" + Convert.ToInt32(id.Text) + ", '" + surname.Text + "', '"+ name.Text + "', '"+ lastname.Text + "', " + Convert.ToInt32(dolznost_id.Text) + ");";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE sotrudnik SET surname = '" + surname.Text + "', '"+ name.Text + "', '"+ lastname.Text + "', " + Convert.ToInt32(dolznost_id.Text) + " WHERE id = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }
РАЗМЕТКА
 <StackPanel Orientation="Horizontal">
                                <TextBlock Margin = "30 30 0 0">surname: </TextBlock>
                                <TextBox Name="surname" x:CompileBindings="False" Text="{Binding surname}" Width="200" Height="5" Margin = "27 20 0 0"></TextBox>
                        </StackPanel>
                        
