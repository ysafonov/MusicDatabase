using System;
using static ProjectWPF.Enums;

namespace ProjectWPF.MusicGroupsFolder
{
    public class Song
    {
        public Song(string name = " ", DateTime released = new DateTime(), TypeOfMusic genre = 0, string length = " ", string writer = " ", System.Guid id = new Guid())
        {
            Name = name;
            Released = released;
            Genre = genre;
            Length = length;
            Writer = writer;
            ID = id;
        }
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime Released { get; set; }
        public TypeOfMusic Genre { get; set; }
        public string Length { get; set; }
        public string Writer { get; set; }
    }
}
