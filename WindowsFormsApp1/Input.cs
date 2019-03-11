using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    class Input         //Class for movement by arrows
    {
        private static Hashtable keyTable = new Hashtable();        //hashtable for key kontrols
        
        public static bool KeyPress(Keys key)
        {
            if (keyTable[key] == null)
            {
                return false;
            }
            return (bool)keyTable[key];
        }

        public static void changeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }
    }
}
