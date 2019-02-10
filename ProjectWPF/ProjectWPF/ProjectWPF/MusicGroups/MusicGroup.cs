using System;
using System.Collections.Generic;
using static ProjectWPF.Enums;

namespace ProjectWPF.MusicGroupsFolder
{
    public class MusicGroup
    {
        public MusicGroup(string name = " ", string nationality = " ", TypeOfMusic type = 0, DateTime createdDate = new DateTime(), DateTime endDate = new DateTime(), System.Data.Linq.Binary image = null, System.Guid id = new Guid())
        {
            Name = name;
            Nationality = nationality;
            Type = type;
            CreatedDate = createdDate;
            EndDate = endDate;
            Image = image;
            ID = id;
            _musicians = new List<Musician>();
            _songs = new List<Song>();
        }

        private List<Musician> _musicians;
        private List<Song> _songs;

        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public TypeOfMusic Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public System.Data.Linq.Binary Image { get; set; }
        public List<Musician> Musicians { get { return _musicians; } set { _musicians = value; } }
        public List<Song> Songs { get { return _songs; } set { _songs = value; } }


        public void AddMusician(Musician musician)
        {
            _musicians.Add(musician);
        }

        public void RemoveMusician(Musician musician)
        {
            _musicians.Remove(musician);
        }

        public void AddSong(Song song)
        {
            _songs.Add(song);
        }

        public void RemoveSong(Song song)
        {
            _songs.Remove(song);
        }
    }

}
