using System;

namespace DMEmu.Utility
{
    public static class Logging
    {
#if DEBUG
        public static bool isConsoleOut = true;
#endif
        public static void log(string str)
        {
            if (isConsoleOut)
            {
                Console.WriteLine(str);
            }
        }
        
        public static void log(string strVal, params string[] args)
        {
            string str = string.Format(strVal, args);
            if (isConsoleOut)
            {
                Console.WriteLine(str);
            }
        }

        public static void debug(string str)
        {
#if DEBUG
            Console.WriteLine("[DEBUG] {0}", str);
#endif
        }

        public static void debug(string strVal, params string[] args)
        {
#if DEBUG
            string str = string.Format(strVal, args);
            Console.WriteLine("[DEBUG] {0}", str);
#endif
        }
    }
}
