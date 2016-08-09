using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToUnicode.Config
{
    public class Icon
    {
        public string ImagePath { get; set; }
        public double CharId { get; set; }
        public override string ToString()
        {
            return string.Format("\"{0}\",{1},0,0,0", ImagePath, this.CharId);
        }
    }
}
