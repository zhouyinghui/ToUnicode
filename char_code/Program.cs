using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace char_code
{
    class Program
    {
        static void Main(string[] args)
        {
         
        }
        static int get_char_code(char character)
        {
            UTF32Encoding encoding = new UTF32Encoding();
            byte[] bytes = encoding.GetBytes(character.ToString().ToCharArray());
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
