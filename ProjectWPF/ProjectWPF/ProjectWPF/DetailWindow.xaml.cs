using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectWPF
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private MusicGroupsFolder.MusicGroup _musicGroup;
        public DetailWindow(MusicGroupsFolder.MusicGroup musicGroup, Database database)
        {
            try
            {
                InitializeComponent();
                _musicGroup = musicGroup;
                ObservableCollection<MusicGroupsFolder.Song> songs = database.SongsOfMusicGroup(musicGroup);
                ObservableCollection<MusicGroupsFolder.Musician> musicians = database.MusiciansOfMusicGroup(musicGroup);
                detailMusicGroupName.Text = _musicGroup.Name;
                detailMusicGroupNationality.Text = _musicGroup.Nationality;
                detailMusicGroupType.Text = _musicGroup.Type.ToString();
                detailMusicGroupCreatedDate.Text = _musicGroup.CreatedDate.ToString();
                detailMusicGroupEndedDate.Text = _musicGroup.EndDate.ToString();
                MusicianGrid.ItemsSource = musicians;
                {
                    if (_musicGroup.Image != null)

                        profileImage.Source = LoadImage(_musicGroup.Image.ToArray());
                }
                SongGrid.ItemsSource = songs;

            }
            catch (System.Exception e)
            {
                MessageBox.Show("Detail je prázdný!\n\n" + e.Message);
            }
            
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            try
            {
                if (imageData == null || imageData.Length == 0) return null;
                var image = new BitmapImage();
                using (var mem = new System.IO.MemoryStream(imageData))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Není možné načíst obrázek!\n\n" + e.Message);
                return null;
            }
           
            
        }
    }
}
