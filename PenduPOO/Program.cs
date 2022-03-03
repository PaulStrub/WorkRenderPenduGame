using System;
using System.Threading;
using System.Collections;

namespace PenduPOO
{


    public class program {
        static void Main(string[] args)
        {
            Play();
        }

        public static char[] PrintableTab;
        public static Stack NotLetterInWord = new Stack();
        public static string Word;
        public static int HP = 6;

        static void Play()
        {
            Console.WriteLine("Bonjour ! bienvenue dans ce jeu du pendu ");
            Thread.Sleep(500);
            Console.WriteLine("Les règles sont simples, tu dois trouver le mot caché. Pour cela tu devra me donner une lettre à la fois et je te dirais si elle fait partie du mot que tu dois deviner. Attention tu n'as que 6 erreurs possible avant de perdre");
            ChoiceLvl();
        }

        static void ChoiceLvl()
        {
            Console.WriteLine("Comme on est gentil on te laisse le choix de la difficulté, écrit 1 pour un niveau facile et 2 pour un niveau impossible :");
            string answer = Console.ReadLine();
            while (answer == "1" && answer == "2")
            {
                Console.WriteLine("il nous faut que tu répondes par 1 ou 2");
                answer = Console.ReadLine();
            }
            
            Word = GenerateWords(answer);
            bool win = PlayTime();
        }
        static string GenerateWords(string lvl)
        {
            
            string[] listA = {  };

            string[] listB = {  };

            if (lvl == "1") 
            {
                Random rand = new Random();
                string word = listA[rand.Next(0, listA.Length)];
                
                PrintableTab = initPrintableTab(word);
                return word;
            }
            else
            {
                Random rand = new Random();
                string word = listB[rand.Next(0, listB.Length)];
                PrintableTab = initPrintableTab(word);
                return word;
            }
        }

        static char[] initPrintableTab(string word)
        {
            char[] tab = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                tab[i] = '-';
            }
            return tab;
        }

        static bool PlayTime()
        {

            bool winGame = false;

            while ( !winGame)
            {
                char charChoose = ChoiceChar();
                bool inWord = isInWord(charChoose);
                if (!inWord)
                {
                    HP -= 1;
                    NotLetterInWord.Push(charChoose); 
                }
                winGame = isWIn();
                if (HP < 0)
                {
                    printDefeat();
                    return false;
                }
                if (!winGame)
                {
                    printPendu();
                }
            }
            printVictory();
            return true; 
        }

        static void printPendu()
        {

            Console.Clear();
            if (HP > 1)
            {
                Console.WriteLine("Il te reste " + HP + " points de vie");
            }
            if (HP == 1)
            {
                Console.WriteLine("Il te reste " + HP + " point de vie");
            }
            Console.WriteLine("");
            Console.WriteLine("Les letres que tu as deja donné et qui ne font pas partie du mot sont les suivantes : ");
            foreach (char elem in NotLetterInWord)
            {
                Console.Write(elem + " ;");
            }
            Console.WriteLine();
            if (HP == 6)
            {
                Console.WriteLine("---------");
                Console.WriteLine("  |    \\|");
                Console.WriteLine("        |");
                Console.WriteLine("        |");
                Console.WriteLine("        |");
                Console.WriteLine("        |");
                Console.WriteLine("   -----------");
            }
            else if (HP == 5)
            {
                Console.WriteLine("---------");
                Console.WriteLine("  |    \\|");
                Console.WriteLine("  0     |");
                Console.WriteLine("        |");
                Console.WriteLine("        |");
                Console.WriteLine("        |");
                Console.WriteLine("   -----------");
            }
            else if (HP == 4)
            {
                Console.WriteLine("---------");
                Console.WriteLine("  |    \\|");
                Console.WriteLine("  0     |");
                Console.WriteLine(" /      |");
                Console.WriteLine("        |");
                Console.WriteLine("        |");
                Console.WriteLine("   -----------");
            }
            else if (HP == 3)
            {
                Console.WriteLine("---------");
                Console.WriteLine("  |    \\|");
                Console.WriteLine("  0     |");
                Console.WriteLine(" /|     |");
                Console.WriteLine("        |");
                Console.WriteLine("        |");
                Console.WriteLine("   -----------");
            }
            else if (HP == 2)
            {
                Console.WriteLine("---------");
                Console.WriteLine("  |    \\|");
                Console.WriteLine("  0     |");
                Console.WriteLine(" /|\\    |");
                Console.WriteLine("        |");
                Console.WriteLine("        |");
                Console.WriteLine("   -----------");
            }
            else if (HP == 1)
            {
                Console.WriteLine("---------");
                Console.WriteLine("  |    \\|");
                Console.WriteLine("  0     |");
                Console.WriteLine(" /|\\    |");
                Console.WriteLine(" /      |");
                Console.WriteLine("        |");
                Console.WriteLine("   -----------");
            }
            else if (HP == 0)
            {
                Console.WriteLine("---------");
                Console.WriteLine("  |    \\|");
                Console.WriteLine("  0     |");
                Console.WriteLine(" /|\\    |");
                Console.WriteLine(" / \\    |");
                Console.WriteLine("        |");
                Console.WriteLine("   -----------");
            }
            Console.WriteLine("Le mot ressemble pour l'instant à ceci : ");
            foreach(char charP in PrintableTab)
            {
                Console.Write(charP + " ");
            }
        }

        static void printDefeat()
        {
            Console.Clear();
            Console.WriteLine("HO NONNNN tu as PERDU ! c'est dommage... c'est pas grave peut être la prochaine fois");
            Console.WriteLine("Le mot était : " + Word);
            Console.WriteLine("Si tu veux réessayer écris 1");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                Console.Clear();
                ChoiceLvl();
            }
            else
            {
                Console.Clear();
            }
        }

        static void printVictory()
        {

            Console.Clear();
            Console.WriteLine("Bravo ! tu as gagné tu es le grand champion du PENDU !");
            Console.WriteLine("Le mot était : " + Word);
            Console.WriteLine("Tu veux refaire une partie ? si oui écris 1 ");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                Console.Clear();
                ChoiceLvl();
            }
            else
            {
                Console.Clear(); 
            }
        }

        static bool isWIn()
        {
;
            for (int i=0; i < Word.Length;i++)
            {
                if(Word[i] != PrintableTab[i])
                {
                    return false;
                }
            }
            return true;
        }

        static bool isInWord(char charChoose)
        {
            bool inWord = false; 

            for (int i = 0; i<Word.Length; i++)
            {
                if (charChoose == Word[i])
                {
                    inWord = true;
                    PrintableTab[i] = charChoose;
                }
            }
            return inWord;
        }

        static char ChoiceChar()
        {
            Console.WriteLine("Donnes nous une lettre :");
            string character = Console.ReadLine();

            bool notInTabPrintable = false;
            bool notInTabNotLetterInWord = false;
            foreach (char charP in PrintableTab)
            {
                if (charP == character[0])
                {
                    notInTabPrintable = true;
                }
            }
            foreach (char charN in NotLetterInWord)
            {
                if (charN == character[0])
                {
                    notInTabNotLetterInWord = true;
                }
            }
            if (character.Length == 1  && !notInTabNotLetterInWord && !notInTabPrintable )

            {
                if ((character[0] >= 'a' && character[0] <= 'z') || (character[0] >= 'A' && character[0] <= 'Z')){
                    return character[0];
                }
                else
                {
                    Console.WriteLine("Il nous faut une lettre de l'alphabet et bien une seule. Verifie egalement que tu n'aies pas deja donne cette lettre");
                    return ChoiceChar();
                }
                    
            }
            else
            {
                Console.WriteLine("Il nous faut une lettre de l'alphabet et bien une seule. Verifie egalement que tu n'aies pas deja donne cette lettre");
                return ChoiceChar();
            }
        }
    }
}
