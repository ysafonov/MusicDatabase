using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static ProjectWPF.Enums;

namespace ProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database _database;
        private ObservableCollection<MusicGroupsFolder.MusicGroup> _musicGroups;
        public MainWindow()
        {
            InitializeComponent();
            _database = new Database();
            _database.createSavingFolder();
            // createDemoDatabase();
            RefreshData();
        }

        public void createDemoDatabase()
        {
            MusicGroupsFolder.MusicGroup OceanElsy = new MusicGroupsFolder.MusicGroup("Okean Elzy", "UKR", TypeOfMusic.Metal, new DateTime(1995, 10, 12), new DateTime(1995, 10, 12));
            MusicGroupsFolder.MusicGroup LinkinPark = new MusicGroupsFolder.MusicGroup("Linkin Park", "USA", TypeOfMusic.Metal, new DateTime(1996, 4, 13));
            MusicGroupsFolder.MusicGroup Metallica = new MusicGroupsFolder.MusicGroup("Metallica", "USA", TypeOfMusic.Rock, new DateTime(1981, 5, 5));

            OceanElsy.AddSong(new MusicGroupsFolder.Song("Tam de nas nema", new DateTime(2007, 10, 12), TypeOfMusic.Metal, "3:25"));
            OceanElsy.AddSong(new MusicGroupsFolder.Song("Obiymy", new DateTime(2013, 2, 11), TypeOfMusic.Metal, "3:55"));
            OceanElsy.AddSong(new MusicGroupsFolder.Song("Not Your War", new DateTime(2017, 6, 25), TypeOfMusic.Metal, "3:45"));
            OceanElsy.AddMusician(new MusicGroupsFolder.Musician("Svyatoslav", "Vakarchuk", new DateTime(1975, 5, 14), nationality: "UKR", sex: "Male"));


            LinkinPark.AddSong(new MusicGroupsFolder.Song("Numb", new DateTime(2002, 10, 12), TypeOfMusic.Metal, "3:25"));
            LinkinPark.AddSong(new MusicGroupsFolder.Song("New Divide", new DateTime(2010, 2, 11), TypeOfMusic.Metal, "3:55"));
            LinkinPark.AddSong(new MusicGroupsFolder.Song("Burn It Down", new DateTime(2012, 6, 25), TypeOfMusic.Metal, "3:45"));
            LinkinPark.AddMusician(new MusicGroupsFolder.Musician("Chester", "Bennington", new DateTime(1976, 3, 20), nationality: "USA", sex: "Male"));

            Metallica.AddSong(new MusicGroupsFolder.Song("Fight Fire With Fire", new DateTime(2008, 10, 21), TypeOfMusic.Metal, "5:25"));
            Metallica.AddSong(new MusicGroupsFolder.Song("To Live Is to Die", new DateTime(2005, 2, 19), TypeOfMusic.Metal, "3:00"));
            Metallica.AddSong(new MusicGroupsFolder.Song("Disposable Heroes", new DateTime(2009, 6, 12), TypeOfMusic.Metal, "4:45"));
            Metallica.AddMusician(new MusicGroupsFolder.Musician("James", "Hetfield", new DateTime(1976, 8, 3), nationality: "USA", sex: "Male"));

            _database.AddForm(OceanElsy);
            _database.AddForm(LinkinPark);
            _database.AddForm(Metallica);
            _database.SaveDatabase();
            _database.LoadDatabase();
        }

        public void RefreshData(object sender = null, EventArgs e = null)
        {
            _musicGroups = _database.ShowListOfMusicGroup();
            MusicGroupGrid.ItemsSource = _musicGroups;
        }

        // This list is for dislay in combobox editor list. It converts a enum, in this case TyeOfMusic to a list, which can be disaply in combobox. Taken from StackOverflow 
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _database.SaveDatabase();
        }

        private void ToCSV(object sender, RoutedEventArgs e)
        {
            string header = ((MenuItem)sender).Header.ToString();
            if (header == "_Import")
            {
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        _database.ChangeWorkSpasePath(dialog.SelectedPath);
                        _database.LoadDatabase();
                        RefreshData();
                        MessageBox.Show("Databáze byla úspěšně nahrana!");
                    }
                    else
                    {
                        MessageBox.Show("Zvolte prosím adresář!");
                    }
                }
            }
            else
            {
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        _database.ChangeWorkSpasePath(dialog.SelectedPath);
                        _database.createSavingFolder();
                        _database.SaveDatabase();
                        MessageBox.Show("Databáze byla úspěšně uložena!");
                    }
                    else
                    {
                        MessageBox.Show("Zvolte prosím adresář!");
                    }
                }
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_musicGroups.ElementAtOrDefault(MusicGroupGrid.SelectedIndex) != null)
            {
                _musicGroups.RemoveAt(MusicGroupGrid.SelectedIndex);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(_musicGroups, _database);
            addWindow.MyEvent += new EventHandler(RefreshData);
            addWindow.Show();
        }

        private void SaveToDatabase(object sender, RoutedEventArgs e)
        {
            foreach (var item in _musicGroups)
            {
                    _database.AddForm(item);
            }
            _database.SaveDatabase();
            MessageBox.Show("Změny byly úspěšně provedeny!");
        }

        private void FindMusicGroup(object sender, RoutedEventArgs e)
        {
            TextBox foundTextBox = (TextBox)LayoutRoot.FindName("textBoxFind");
            string searchingString = foundTextBox.Text;
            _musicGroups = _database.FindMusicGroupName(searchingString);
            MusicGroupGrid.ItemsSource = _musicGroups;
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            var data = _musicGroups.ElementAtOrDefault(MusicGroupGrid.SelectedIndex);
            DetailWindow detailWindow = new DetailWindow(_musicGroups.ElementAtOrDefault(MusicGroupGrid.SelectedIndex), _database);
            detailWindow.Show();
        }
    }
}
