using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static ProjectWPF.Enums;

namespace ProjectWPF
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public event EventHandler MyEvent;
        private String _regexDate = "^(0[1-9]|[12][0-9]|3[01])[- //](0[1-9]|1[012])[- //](19|20)\\d\\d$";
        private ObservableCollection<MusicGroupsFolder.Song> _songs;
        private ObservableCollection<MusicGroupsFolder.Musician> _musicians;
        private ObservableCollection<MusicGroupsFolder.MusicGroup> _musicGroup;
        private byte[] _image;
        private Database _database;


        public AddWindow(ObservableCollection<MusicGroupsFolder.MusicGroup> musicGroups, Database database)
        {
            _songs = new ObservableCollection<MusicGroupsFolder.Song>();
            _musicians = new ObservableCollection<MusicGroupsFolder.Musician>();
            _database = database;
            _musicGroup = musicGroups;
            InitializeComponent();
            MusicianGrid.ItemsSource = _musicians;
            SongGrid.ItemsSource = _songs;
        }

        private void textDate_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox1 = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text,
                    _regexDate))
                {
                    MessageBox.Show("Špatný formát datumu.");
                }
            }
        }

        private void buttonImage_onClick(object sender, RoutedEventArgs args)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "images |*.jpg;*.png";
            string path = "";
            if (file.ShowDialog() == true)
            {
                path = file.FileName;
                profileImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(path);
                _image = File.ReadAllBytes(path);
            }
        }


        private void addSinger_onClick(object sender, RoutedEventArgs args)
        {
            MusicianWindow.WindowState = Xceed.Wpf.Toolkit.WindowState.Open;
        }

        private void addSong_onClick(object sender, RoutedEventArgs args)
        {
            SongWindow.WindowState = Xceed.Wpf.Toolkit.WindowState.Open;
        }

        private void deleteMusician_onClick(object sender, RoutedEventArgs args)
        {
            if (_musicians.ElementAtOrDefault(MusicianGrid.SelectedIndex) != null)
            {
                _musicians.RemoveAt(MusicianGrid.SelectedIndex);
            }
        }

        private void deleteSong_onClick(object sender, RoutedEventArgs args)
        {
            if (_songs.ElementAtOrDefault(SongGrid.SelectedIndex) != null)
            {
                _songs.RemoveAt(SongGrid.SelectedIndex);
            }
        }

        private void textString_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox1 = sender as TextBox;
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[a-zA-Z]+"))
                {
                    MessageBox.Show("Špatný formát. Nezadávejte čísla.");
                }
            }
        }

        private void AddMusician(object sender, RoutedEventArgs e)
        {

            _musicians.Add(
                new MusicGroupsFolder.Musician(
                    musicianName.Text,
                    musicianSurname.Text,
                    musicianDateOfBirth.SelectedDate ?? new DateTime(),
                    musicianInstrument.Text,
                    musicianNationality.Text,
                    musicianSex.Text));
            MusicianWindow.WindowState = Xceed.Wpf.Toolkit.WindowState.Closed;

            MakeEmptyMusician();

        }

        private void AddSong(object sender, RoutedEventArgs e)
        {

            _songs.Add(
                new MusicGroupsFolder.Song(
                    songName.Text,
                    songReleased.SelectedDate ?? new DateTime(),
                    (TypeOfMusic)songTypeOfMusic.SelectedValue,
                    songLength.Text,
                    songWriter.Text));
            SongWindow.WindowState = Xceed.Wpf.Toolkit.WindowState.Closed;
            MakeEmptySong();

        }

        private void AddMusicGroup(object sender, RoutedEventArgs e)
        {
            if (musicGroupName.Text != "" && musicGroupNationality.Text != "" && musicGroupTypeOfMusic.SelectedValue != null)
            {
                MusicGroupsFolder.MusicGroup group = new MusicGroupsFolder.MusicGroup(
                        musicGroupName.Text,
                        musicGroupNationality.Text,
                        (TypeOfMusic)musicGroupTypeOfMusic.SelectedValue,
                        musicGroupStartedDate.SelectedDate ?? new DateTime(),
                        musicGroupEndedDate.SelectedDate ?? new DateTime(),
                        _image);
                group.Musicians = _musicians.ToList();
                group.Songs = _songs.ToList();
                // _musicGroup.Add(group);
                _database.AddForm(group);
                MakeEmptyMusicGroup();
                MyEvent(this, EventArgs.Empty);
                Close();
            }
            else
            {
                MessageBox.Show("Zadejte prosím povinné údaje!");
            }
           
        }


        private void MakeEmptySong()
        {
            songName.Text = string.Empty;
            songLength.Text = string.Empty;
            songWriter.Text = string.Empty;
            songTypeOfMusic.SelectedValue = null;
            songReleased = new DatePicker();
        }


        private void MakeEmptyMusician()
        {
            musicianName.Text = string.Empty;
            musicianSurname.Text = string.Empty;
            musicianInstrument.Text = string.Empty;
            musicianNationality.Text = string.Empty;
            musicianSex.Text = string.Empty;
            musicianDateOfBirth = new DatePicker();
        }

        private void MakeEmptyMusicGroup()
        {
            musicGroupName.Text = string.Empty;
            musicGroupNationality.Text = string.Empty;
            musicGroupEndedDate = new DatePicker();
            musicGroupStartedDate = new DatePicker();
            musicGroupTypeOfMusic.SelectedValue = null;

        }
    }
}