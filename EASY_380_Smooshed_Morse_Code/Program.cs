using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EASY_380_Smooshed_Morse_Code
{
    class Program
    {

        static string[] morseCode;

        static void Main(string[] args)
        {

            morseCode = ".- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --..".Split(' ');

            String[] enableWords = File.ReadAllLines(@"./enable1.txt");

            Dictionary<string, int> bonusOneTracker = new Dictionary<string, int>();

            List<string> encodings = new List<string>();


            for (int i = 0; i < 8192; i++)
            {
                string g = Convert.ToString(i, 2);
                string s = Convert.ToString(i << (13 - g.Length), 2).Replace('1', '-').Replace('0', '.');

                encodings.Add(s);
            }


            string bonus_two = "";
            string bonus_three = "";
            string bonus_four = "";

            foreach (string word in enableWords)
            {
                string morseWord = smorse(word);


                for (int i = encodings.Count - 1; i >= 0; i--)
                {
                    if (morseWord.Contains(encodings[i]))
                    {
                        encodings.RemoveAt(i);
                    }
                }

                if (!bonusOneTracker.ContainsKey(morseWord))
                {
                    bonusOneTracker.Add(morseWord, 1);
                }
                else
                {
                    bonusOneTracker[morseWord]++;
                }


                if (morseWord.Contains("---------------") &&
                    !morseWord.Contains("----------------"))
                {
                    bonus_two = word;
                }

                if (word.Length == 21 &&
                    morseWord.Count(c => c == '.') == morseWord.Count(c => c == '-'))
                {
                    bonus_three = word;
                }


                if (word.Length == 13 &&
                    morseWord == new string(morseWord.Reverse().ToArray()))
                {
                    bonus_four = word;
                }
            }

            Console.WriteLine("Bonus One: {0}", bonusOneTracker.First(c => c.Value == 13).Key);
            Console.WriteLine("Bonus Two: {0}", bonus_two);
            Console.WriteLine("Bonus Three: {0}", bonus_three);
            Console.WriteLine("Bonus Four: {0}", bonus_four);
            Console.WriteLine("Bonus Five: [ {0} ]", string.Join(" , ", encodings.Skip(1)));

            Console.ReadLine();

        }

        public static String smorse(string inputStr)
        {
            string result = "";

            foreach (char c in inputStr.ToLower())
            {
                result += morseCode[c - 97];
            }

            return result;
        }
    }
}
