using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF
{
    public class Enums
    {
        public enum TypeOfMusic
        {
            Metal,
            Rock,
            Hip_hop,
            Pop,
            Country
        }
    }

    public static class EnumLists
    {
        public static IList<Enums.TypeOfMusic> TypeOfMusicList
        {
            get => Enum.GetValues(typeof(Enums.TypeOfMusic)).Cast<Enums.TypeOfMusic>().ToList();
        }
    }
}
