using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.classes
{
    class ressource_circle
    {
        point center; //center.x & center.y
        int rayon; //rayon in percent (100% )
        int ressource_type;
        int density;

        public void Generate()
        {
            Random rand = new Random();
            rayon = rand.Next();
        }
    }

}
