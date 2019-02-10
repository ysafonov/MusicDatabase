using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using static ProjectWPF.Enums;
using System.Windows;

namespace ProjectWPF
{
    public class Database
    {
        private DataClassesDataContext _database;
        public string ErrorString { get; set; }
        private string _savingPath = "C:\\Users\\Yehor Safonov\\Desktop\\";
        private string _savingFolder = "MusicGroups";

        public Database()                                        // Hlavni Konstruktor definuje databazi a vytvari pracovni adresar
        {
            _database = new DataClassesDataContext();
        }

        public void AddForm(MusicGroupsFolder.MusicGroup currentGroup)  // Funkce pridani do tabulky hudebni skupiny
        {

            List<System.Guid> SongsIDs = new List<System.Guid>();
            List<System.Guid> MusicianIDs = new List<System.Guid>();

            System.Guid tmpIDMusicGroup = AddMusicGroup(currentGroup);       // Pridani do databazi vygenerovane skupiny

            foreach (var song in currentGroup.Songs)                         // Pridani do databazi novych pisnicek
            {
                SongsIDs.Add(AddSong(song));                                 // Ulozeni jejich IDs do listu
            }
            foreach (var idSong in SongsIDs)                                     // Nastavovani vazeb Skupina - Pisnicka
            {
                AddMusicGroup_Song(tmpIDMusicGroup, idSong);
            }

            foreach (var musician in currentGroup.Musicians)                      // Pridani do databazi novych hudebniku
            {
                MusicianIDs.Add(AddMusician(musician));                           // Ulozeni jejich IDs do listu
            }
            foreach (var idMusicians in MusicianIDs)                      // Nastavovani vazeb Skupina - Hudebnik
            {
                AddMusicGroup_Musician(tmpIDMusicGroup, idMusicians);
            }
        }


        public System.Guid AddMusician(MusicGroupsFolder.Musician musician)  // Pridani do databaze hudebnika
        {
            Musician tmpMusician = new Musician()
            {
                MusicianName = musician.Name,
                MusicianSurname = musician.Surname,
                MusicianDateBirth = musician.DateOfBirth,
                MusicianInstrument = musician.MusicianInstrument,
                MusicianNationality = musician.Nationality,
                MusicianSex = musician.Sex
            };
            try
            {
                _database.Musicians.InsertOnSubmit(tmpMusician);
                _database.SubmitChanges();
            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se přidat hudebníka do databáze!\n\n" + ErrorString);
            }

            return tmpMusician.MusicianID;
        }

        public System.Guid AddMusicGroup(MusicGroupsFolder.MusicGroup group)  // Pridani hudebni skupiny bez referenci
        {
            var alreadyInDatabase = _database.MusicGroups.FirstOrDefault(stored => stored.MusicGroupID == group.ID);
            if (alreadyInDatabase == null)
            {
                MusicGroup tmpMusicGroup = new MusicGroup()
                {
                    MusicGroupName = group.Name,
                    MusicGroupCreatedDate = group.CreatedDate,
                    MusicGroupEndDate = group.EndDate,
                    MusicGroupNationality = group.Nationality,
                    MusicGroupType = group.Type.ToString(),
                    MusicGroupPhoto = group.Image
                };
                try
                {
                    _database.MusicGroups.InsertOnSubmit(tmpMusicGroup);
                    _database.SubmitChanges();

                }
                catch (Exception e)
                {
                    ErrorString = e.Message;
                    MessageBox.Show("Nepodařilo se přidat hudební skupinu do databáze!\n\n" + ErrorString);
                }
                return tmpMusicGroup.MusicGroupID;
            }
            else
            {

                alreadyInDatabase.MusicGroupEndDate = group.EndDate;
                alreadyInDatabase.MusicGroupCreatedDate = group.CreatedDate;
                alreadyInDatabase.MusicGroupName = group.Name;
                alreadyInDatabase.MusicGroupNationality = group.Nationality;
                alreadyInDatabase.MusicGroupPhoto = group.Image;
                alreadyInDatabase.MusicGroupType = group.Type.ToString();
                _database.MusicGroups.Context.SubmitChanges();
                return alreadyInDatabase.MusicGroupID;
            }
        }

        public System.Guid AddSong(MusicGroupsFolder.Song song)  // Pridani pisnicky
        {
            Song tmpSong = new Song()
            {
                SongName = song.Name,
                SongGenre = song.Genre.ToString(),
                SongLength = song.Length,
                SongReleased = song.Released,
                SongWriter = song.Writer
            };
            try
            {
                _database.Songs.InsertOnSubmit(tmpSong);
                _database.SubmitChanges();
            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se přidat pisničku do databáze!\n\n" + ErrorString);
            }
           
            return tmpSong.SongID;
        }

        public void AddMusicGroup_Song(System.Guid idGroup, System.Guid idSong)    // Pridani do databaze vazby Skupina - pisnicka
        {
            try
            {
                _database.MusicGroup_Songs.InsertOnSubmit(new MusicGroup_Song() { MusicGroupID = idGroup, SongID = idSong });
                _database.SubmitChanges();
            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se přidat vazbu do databáze!\n\n" + ErrorString);
            }
          
        }

        public void AddMusicGroup_Musician(System.Guid idGroup, System.Guid idMusician)
        {
            try
            {
                _database.MusicGroup_Musicians.InsertOnSubmit(new MusicGroup_Musician() { MusicGroupID = idGroup, MusicianID = idMusician });
                _database.SubmitChanges();
            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se přidat vazbu do databáze!\n\n" + ErrorString);
            }
           
        }

        public ObservableCollection<MusicGroupsFolder.Musician> ShowListOfMusician()   // zobrazeni vsech hudebniku bez filtrovani
        {
            var musicians = from mus in _database.Musicians
                            select new MusicGroupsFolder.Musician(mus.MusicianName, mus.MusicianSurname, (System.DateTime)mus.MusicianDateBirth, mus.MusicianInstrument, mus.MusicianNationality, mus.MusicianSex, mus.MusicianID);
            return new ObservableCollection<MusicGroupsFolder.Musician>(musicians);
        }

        public ObservableCollection<MusicGroupsFolder.Song> ShowListOfSongs()    // zobrazeni vsech pisnicek bez filtrovani
        {
            var songs = from song in _database.Songs
                        select new MusicGroupsFolder.Song(song.SongName, (System.DateTime)song.SongReleased, (TypeOfMusic)Enum.Parse(typeof(TypeOfMusic), song.SongGenre), song.SongLength, song.SongWriter, song.SongID);
            return new ObservableCollection<MusicGroupsFolder.Song>(songs);
        }

        public ObservableCollection<MusicGroupsFolder.MusicGroup> ShowListOfMusicGroup()   // zobrazeni vsech hudebnich skupin bez filtrovani
        {
            var musicGroups = from gr in _database.MusicGroups
                              select new MusicGroupsFolder.MusicGroup(gr.MusicGroupName, gr.MusicGroupNationality, (TypeOfMusic)Enum.Parse(typeof(TypeOfMusic), gr.MusicGroupType), (System.DateTime)gr.MusicGroupCreatedDate, (System.DateTime)gr.MusicGroupEndDate, gr.MusicGroupPhoto, gr.MusicGroupID);

            return new ObservableCollection<MusicGroupsFolder.MusicGroup>(musicGroups);
        }
        public ObservableCollection<MusicGroupsFolder.Song> SongsOfMusicGroup(MusicGroupsFolder.MusicGroup group)  //zobrazeni pisnicek konkretni hudebni skupiny (hledani podle ID)
        {
            if (group == null) return new ObservableCollection<MusicGroupsFolder.Song>();
            var id = group.ID;
            var songs = from song in _database.Songs
                        join gr in _database.MusicGroup_Songs
                        on song.SongID equals gr.SongID
                        where gr.MusicGroupID == id
                        select new MusicGroupsFolder.Song(song.SongName, (System.DateTime)song.SongReleased, (TypeOfMusic)Enum.Parse(typeof(TypeOfMusic), song.SongGenre), song.SongLength, song.SongWriter, song.SongID);
            return new ObservableCollection<MusicGroupsFolder.Song>(songs);
        }

        public ObservableCollection<MusicGroupsFolder.Musician> MusiciansOfMusicGroup(MusicGroupsFolder.MusicGroup group)  //zobrazeni hudebniku konkretni hudebni skupiny (hledani podle ID)
        {
            if (group == null) return new ObservableCollection<MusicGroupsFolder.Musician>();
            var id = group.ID;
            var musicians = from mus in _database.Musicians
                            join gr in _database.MusicGroup_Musicians
                            on mus.MusicianID equals gr.MusicianID
                            where gr.MusicGroupID == id
                            select new MusicGroupsFolder.Musician(mus.MusicianName, mus.MusicianSurname, (System.DateTime)mus.MusicianDateBirth, mus.MusicianInstrument, mus.MusicianNationality, mus.MusicianSex, mus.MusicianID);
            return new ObservableCollection<MusicGroupsFolder.Musician>(musicians);
        }

        public ObservableCollection<MusicGroupsFolder.MusicGroup> FindMusicGroupName(string groupName)  // Vyhledani konkretni skupiny podle jmena + vraceni objektu MusicGroup
        {
            string text = groupName.ToLower();
            var groups = from gr in _database.MusicGroups
                         where (gr.MusicGroupName.ToLower()).Contains(text) || (gr.MusicGroupNationality.ToLower()).Contains(text) || (gr.MusicGroupType.ToLower()).Contains(text)
                         select new MusicGroupsFolder.MusicGroup(gr.MusicGroupName, gr.MusicGroupNationality, (TypeOfMusic)Enum.Parse(typeof(TypeOfMusic), gr.MusicGroupType), (System.DateTime)gr.MusicGroupCreatedDate, (System.DateTime)gr.MusicGroupEndDate, gr.MusicGroupPhoto, gr.MusicGroupID);
            return new ObservableCollection<MusicGroupsFolder.MusicGroup>(groups);
        }

        public static string DelWhiteSpaces(string str)  // Funkce odstraneni mezer (pouziva se pri ukladani dat do csv souboru)
        {
            return Regex.Replace(str, @"\s+", String.Empty);
        }

        public void SaveDatabase()   // Ulozeni cele databaze do csv souboru
        {
            SaveMusicGroupTable();
            SaveMusicianTable();
            SaveSongTable();
            SaveMusicGroup_SongTable();
            SaveMusicGroup_MusicianTable();
        }

        public void LoadDatabase()  // Nacteni databaze z CSV souboru
        {
            var musicGroups = LoadMusicGroupTable();
            var songs = LoadSongTable();
            var musicians = LoadMusicianTable();
            LoadMusicGroup_SongTable(musicGroups, songs);
            LoadMusicGroup_MusicianTable(musicGroups, musicians);
            foreach (var item in musicGroups)
            {
                if (Exist(item.Key))
                {
                    AddForm(item.Value);
                }
            }
        }

        public void ChangeWorkSpasePath(string path)
        {
            _savingPath = path + "\\";
        }

        public Boolean Exist(string id)
        {
            var items = from i in _database.MusicGroups
                        where i.MusicGroupID.ToString() == id
                        select i;
            if (items.Count() > 0) return false;
            return true;
        }

        private void SaveMusicianTable()   // ulozeni tabulky hudebniku .csv
        {
            using (StreamWriter reader = new StreamWriter(_savingPath + "MusicianTable.csv"))
            {
                try
                {
                    foreach (var item in ShowListOfMusician())
                    {
                        reader.WriteLine(item.ID + ";" + DelWhiteSpaces(item.Name) + ";" + DelWhiteSpaces(item.Surname) + ";" + ConvertDateToString(item.DateOfBirth) + ";" + item.MusicianInstrument + ";" + DelWhiteSpaces(item.Nationality) + ";" + DelWhiteSpaces(item.Sex));
                    }
                }
                catch (Exception e)
                {
                    ErrorString = e.Message;
                    MessageBox.Show("Nepodařilo se uložit tabulku hudebníků!\n\n" + ErrorString);
                }
            }
        }

        private void SaveMusicGroupTable()   // ulozeni tabulky skupin .csv
        {
            using (StreamWriter reader = new StreamWriter(_savingPath + "MusicGroupTable.csv"))
            {
                try
                {
                    foreach (var item in ShowListOfMusicGroup())
                    {
                        string path = "";
                        if (item.Image != null)
                        {
                            using (Image image = Image.FromStream(new MemoryStream(item.Image.ToArray())))
                            {
                                path = _savingPath + "\\Images\\" + item.ID + ".jpg";
                                image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }
                        reader.WriteLine(item.ID + ";" + DelWhiteSpaces(item.Name) + ";" + item.Type.ToString() + ";" + ConvertDateToString(item.CreatedDate) + ";" + ConvertDateToString(item.EndDate) + ";" + DelWhiteSpaces(item.Nationality) + ";" + path);
                    }
                }
                catch (Exception e)
                {
                    ErrorString = e.Message;
                    MessageBox.Show("Nepodařilo se uložit tabulku skupin!\n\n" + ErrorString);
                }
            }
        }

        private void SaveSongTable()   // ulozeni tabulky pisnicek .csv
        {


            using (StreamWriter reader = new StreamWriter(_savingPath + "SongTable.csv"))
            {
                try
                {
                    foreach (var item in ShowListOfSongs())
                    {
                        reader.WriteLine(item.ID + ";" + item.Name + ";" + ConvertDateToString(item.Released) + ";" + item.Genre.ToString() + ";" + DelWhiteSpaces(item.Length) + ";" + item.Writer);
                    }
                }
                catch (Exception e)
                {
                    ErrorString = e.Message;
                    MessageBox.Show("Nepodařilo se uložit tabulku pisniček!\n\n" + ErrorString);
                }
            }
        }

        public void createSavingFolder(string path = "")   // Vytvoreni Pracovniho adresare s uvnit umistenym Adresarem obrazku
        {
            try
            {
                if (!Directory.Exists(_savingPath + _savingFolder))
                {
                    DirectoryInfo di = Directory.CreateDirectory(_savingPath + _savingFolder);
                }
                if (!Directory.Exists(_savingPath + _savingFolder + "\\Images"))
                {
                    DirectoryInfo ditmp = Directory.CreateDirectory(_savingPath + _savingFolder + "\\Images");
                }
                _savingPath += _savingFolder + "\\";
            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se vytvořit pracovní adresář!\n\n" + ErrorString);
            }
        }

        private void SaveMusicGroup_MusicianTable()   // ulozeni tabulky vazeb skupina - hudebnik .csv
        {
            var lines = from line in _database.MusicGroup_Musicians
                        select line;
            using (StreamWriter reader = new StreamWriter(_savingPath + "MusicGroup_Musician.csv"))
            {
                try
                {
                    foreach (var item in lines)
                    {
                        reader.WriteLine(item.MusicGroupID + ";" + item.MusicianID);
                    }
                }
                catch (Exception e)
                {
                    ErrorString = e.Message;
                    MessageBox.Show("Nepodařilo se uložit tabulku skupina - hudebník!\n\n" + ErrorString);
                }
            }

        }

        private void SaveMusicGroup_SongTable()    // ulozeni tabulky vazeb skupina - pisnicka .csv
        {
            var lines = from line in _database.MusicGroup_Songs
                        select line;
            using (StreamWriter reader = new StreamWriter(_savingPath + "MusicGroup_Song.csv"))
            {
                try
                {
                    foreach (var item in lines)
                    {
                        reader.WriteLine(item.MusicGroupID + ";" + item.SongID);
                    }
                }
                catch (Exception e)
                {
                    ErrorString = e.Message;
                    MessageBox.Show("Nepodařilo se uložit tabulku skupina - pisnička!\n\n" + ErrorString);
                }
            }
        }

        private void LoadMusicGroup_MusicianTable(Dictionary<string, MusicGroupsFolder.MusicGroup> musicGroupDictionary, Dictionary<string, MusicGroupsFolder.Musician> musicianDictionary)
        {
            try
            {
                using (StreamReader reader = File.OpenText(_savingPath + "MusicGroup_Musician.csv"))  // Nahrani Vazeb do tabulky pouzitim predchozich ID
                {

                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(';');
                        if (data.Length == 2)
                        {
                            if (musicGroupDictionary.ContainsKey(data[0]) && musicianDictionary.ContainsKey(data[1]))
                            {
                                musicGroupDictionary[data[0]].AddMusician(musicianDictionary[data[1]]);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se nahrat vazební tabulky!\n\n" + ErrorString);
            }
        }

        private void LoadMusicGroup_SongTable(Dictionary<string, MusicGroupsFolder.MusicGroup> musicGroupDictionary, Dictionary<string, MusicGroupsFolder.Song> songDictionary)
        {
            try
            {
                using (StreamReader reader = File.OpenText(_savingPath + "MusicGroup_Song.csv"))  // Nahrani Vazeb do tabulky pouzitim predchozich ID
                {

                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(';');
                        if (data.Length == 2)
                        {
                            if (musicGroupDictionary.ContainsKey(data[0]) && songDictionary.ContainsKey(data[1]))
                            {
                                musicGroupDictionary[data[0]].AddSong(songDictionary[data[1]]);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se nahrat vazební tabulky!\n\n" + ErrorString);
            }
        }

        private Dictionary<string, MusicGroupsFolder.Song> LoadSongTable()   // Nahrani tabulky Pisnicek do Dictionary
        {
            Dictionary<string, MusicGroupsFolder.Song> dictionary = new Dictionary<string, MusicGroupsFolder.Song>();

            try
            {
                using (StreamReader reader = File.OpenText(_savingPath + "SongTable.csv"))
                {

                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(';');
                        if (data.Length == 6)
                        {
                            string name = data[1];
                            DateTime created = ConvertStringDateToDateTime(data[2]);
                            string genre = data[3];
                            string length = data[4];
                            string writer = data[5];

                            MusicGroupsFolder.Song tmpGroup = new MusicGroupsFolder.Song(name, created, (TypeOfMusic)Enum.Parse(typeof(TypeOfMusic), genre), length, writer);
                            dictionary.Add(data[0], tmpGroup);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se nahrat tabulku pisniček!\n\n" + ErrorString);
            }
            return dictionary;
        }

        private Dictionary<string, MusicGroupsFolder.Musician> LoadMusicianTable()   // Nahrani tabulky Hudebniku do Dictionary
        {
            Dictionary<string, MusicGroupsFolder.Musician> dictionary = new Dictionary<string, MusicGroupsFolder.Musician>();
            try
            {
                using (StreamReader reader = File.OpenText(_savingPath + "MusicianTable.csv"))
                {

                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(';');
                        if (data.Length == 7)
                        {
                            string name = data[1];
                            string surname = data[2];
                            DateTime born = ConvertStringDateToDateTime(data[3]);
                            string instrument = data[4];
                            string country = data[5];
                            string sex = data[6];
                            MusicGroupsFolder.Musician tmpGroup = new MusicGroupsFolder.Musician(name, surname, born, instrument, country, sex);
                            dictionary.Add(data[0], tmpGroup);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se nahrat tabulku hudebníků!\n\n" + ErrorString);
            }
            return dictionary;
        }


        private Dictionary<string, MusicGroupsFolder.MusicGroup> LoadMusicGroupTable()   // Nahrani tabulky Skupin do Dictionary
        {
            Dictionary<string, MusicGroupsFolder.MusicGroup> dictionary = new Dictionary<string, MusicGroupsFolder.MusicGroup>();

            try
            {
                using (StreamReader reader = File.OpenText(_savingPath + "MusicGroupTable.csv"))
                {

                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(';');
                        if (data.Length == 7)
                        {
                            string name = data[1];
                            string type = data[2];
                            DateTime start = ConvertStringDateToDateTime(data[3]);
                            DateTime end = ConvertStringDateToDateTime(data[4]);
                            string nationality = data[5];
                            System.Data.Linq.Binary image = null;
                            if (data[6].Length > 0)
                            {
                                image = File.ReadAllBytes(data[6]);
                            }
                            MusicGroupsFolder.MusicGroup tmpGroup = new MusicGroupsFolder.MusicGroup(name, nationality, (TypeOfMusic)Enum.Parse(typeof(TypeOfMusic), type), start, end, image);
                            dictionary.Add(data[0], tmpGroup);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show("Nepodařilo se nahrat tabulku hudebních skupin!\n\n" + ErrorString);
            }
            return dictionary;
        }

        public string ConvertDateToString(DateTime time) // prevod datetime do textoveho formatu
        {
            string str = "";
            string FMT = "O";
            try
            {
                str = time.ToString(FMT);
            }
            catch (Exception e)
            {
                ErrorString = e.Message;
            }
            return str;
        }
        public DateTime ConvertStringDateToDateTime(string time)  // prevod date string do DateTime
        {
            DateTime tmpDate = new DateTime();
            string FMT = "O";
            try
            {
                tmpDate = DateTime.ParseExact(time, FMT, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                ErrorString = e.Message;
                MessageBox.Show(ErrorString);
            }
            return tmpDate;
        }

    }
}