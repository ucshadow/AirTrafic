using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Console;

namespace AirTr.Util {

    public static class Helpers {

        public static void Print(string message,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string className = "") {
            var cName = className.Split('\\').Last().Split('.').First();

            ForegroundColor = ConsoleColor.White;

            var spaced = CalculateDistance($"[ {cName}: {methodName} ]");

            var t = DateTime.Now;
            var h = t.Hour < 10 ? $"0{t.Hour}" : $"{t.Hour}";
            var m = t.Minute < 10 ? $"0{t.Minute}" : $"{t.Minute}";
            var s = t.Second < 10 ? $"0{t.Second}" : $"{t.Second}";
            var time = $"{h}:{m}:{s}";
            WriteLine($"{time} {spaced}  ->  {message}");
        }

        public static void Print(string message, ConsoleColor textColor,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string className = "") {
            var cName = className.Split('\\').Last().Split('.').First();

            ForegroundColor = textColor;

            var spaced = CalculateDistance($"[ {cName}: {methodName} ]");

            var t = DateTime.Now;
            var h = t.Hour < 10 ? $"0{t.Hour}" : $"{t.Hour}";
            var m = t.Minute < 10 ? $"0{t.Minute}" : $"{t.Minute}";
            var s = t.Second < 10 ? $"0{t.Second}" : $"{t.Second}";
            var time = $"{h}:{m}:{s}";
            WriteLine($"{time} {spaced}  ->  {message}");

            ForegroundColor = ConsoleColor.White;
        }

        private static bool SuperContains(IEnumerable<string> list, string s) {
            return list.Any(s1 => OneInTheOther(s1, s));
        }

        private static bool OneInTheOther(string a, string b) {
            return a.Length > b.Length ? a.Contains(b) : b.Contains(a);
        }

        private static string CalculateDistance(string s) {
            for (var i = s.Length; i < 55; i++) {
                s += " ";
            }

            return s;
        }
    }
}