using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.MusicGroupsFolder
{
    public class Musician
    {
        public Musician(string name = " ", string surname = " ", DateTime dateOfBirth = new DateTime(), string musicianInstrument = " ", string nationality = " ", string sex = "", System.Guid id = new Guid())
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            MusicianInstrument = musicianInstrument;
            Nationality = nationality;
            Sex = sex;
            ID = id;
        }
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MusicianInstrument { get; set; }
        public string Nationality { get; set; }
        public string Sex { get; set; }
    }
}
