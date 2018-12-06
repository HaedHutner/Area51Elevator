using System;

namespace Area51Elevator {

    public static class Log {

        public static void Info (string message, params object[] parameters) {
            Console.WriteLine($"[{DateTime.Now.ToString()}] " + message, parameters);
        }

    }

}