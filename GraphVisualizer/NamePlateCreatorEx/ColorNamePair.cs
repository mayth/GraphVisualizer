using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NamePlateCreatorEx
{
    public class ColorNamePair
    {
        public string Name { get; set; }
        public System.Windows.Media.Color Color { get; set; }

        public ColorNamePair(string name, System.Windows.Media.Color color)
        {
            Name = name;
            Color = color;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Color.ToString());
        }
    }
}
