using System;
using System.Threading;
using System.Collections;
using System.IO;

namespace PenduPOO
{

    public class program {
        static void Main(string[] args) //launch the game
        {
            Play();
            
        }

        public static char[] PrintableTab;
        public static Stack NotLetterInWord = new Stack();
        public static string Word;
        public static int HP = 6;

        static void Play() // starting of the game 
        {
            Console.WriteLine("Bonjour ! bienvenue dans ce jeu du pendu ");
            ChoiceLvl();
        }

        static void ChoiceLvl() // choice what difficulty the player want 
        {
            Console.WriteLine("Comme on est gentil on te laisse le choix de la difficulté, écris ");
            Console.WriteLine(" - 1 pour un niveau facile :");
            Console.WriteLine("     les pays !");
            Console.WriteLine(" - 2 pour un niveau impossible :");
            Console.WriteLine("     les mots inconnus de la langue française ! ");
            string answer = Console.ReadLine();
            while (answer != "1" && answer != "2")
            {
                Console.WriteLine("il nous faut que tu répondes par 1 ou 2");
                answer = Console.ReadLine();
            }
            
            Word = GenerateWords(answer);
            bool win = PlayTime();
        }
        static string GenerateWords(string lvl) //generate a random word depending of the difficulty level
        {

            string[] listA = File.ReadAllLines(@"C:\Users\Paul\OneDrive\Git\Pendu\WorkRenderPenduGame\PenduPOO\ListA.txt");

            string[] listB = File.ReadAllLines(@"C:\Users\Paul\OneDrive\Git\Pendu\WorkRenderPenduGame\PenduPOO\ListB.txt");


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

        static char[] initPrintableTab(string word) //initialize the tab witch countains char of the word when the player give us 
        {
            char[] tab = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                tab[i] = '-';
            }
            return tab;
        }

        static bool PlayTime() //function that is player turn 
        {
            printPendu();
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
                if (HP == 0)
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

        static void printPendu() //This function is use to print on the consol the step of the game, it returns the HP number, letters that the player has already gave us and witch aren't in the word, letters that the player has already gave us and witch are in the word, moreover a drawing.
        {

            Console.Clear();
            if (HP == 0)
            {
                Console.WriteLine("  +-----+\n" +
                    "  |    \\|\n" +
                    "  0     |\n" +
                    " /|\\    |\n" +
                    "        |\n" +
                    "   -----------");
                
            }
            else
            {
                if (HP > 1)
                {
                    Console.WriteLine("Il te reste " + HP + " points de vie");
                }
                else if (HP < 2)
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
                Console.WriteLine("  +-----+");
                Console.WriteLine("  |    \\|");
                if (HP < 6)
                {
                    Console.WriteLine("  0     |");
                }
                else
                {
                    Console.WriteLine("        |");
                }
                if (HP < 5)
                {
                    if (HP < 4)
                    {
                        if (HP < 3)
                        {
                            Console.WriteLine(" /|\\    |");
                        }
                        else
                        {
                            Console.WriteLine(" /|     |");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  |     |");
                    }
                }
                else
                {
                    Console.WriteLine("        |");
                }
                if (HP < 3)
                {
                    if (HP < 2)
                    {
                        Console.WriteLine(" / \\    |");
                    }
                    else
                    {
                        Console.WriteLine(" /      |");
                    }
                }
                else
                {
                    Console.WriteLine("        |");
                }
                Console.WriteLine("        |");
                Console.WriteLine("   -----------");


                Console.WriteLine("Le mot ressemble pour l'instant à ceci : ");
                foreach (char charP in PrintableTab)
                {
                    Console.Write(charP + " ");
                }
                Console.WriteLine(" ");
            }
        }

        static void printDefeat() //Print the defeat of the player.
        {
            Console.Clear();
            Console.WriteLine("HO NONNNN tu as PERDU ! c'est dommage... c'est pas grave peut être la prochaine fois");
            Console.WriteLine("Le mot était : " + Word);
            Console.WriteLine("Si tu veux réessayer écris 1");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                Console.Clear();
                NotLetterInWord.Clear();
                Word = null;
                PrintableTab = null;
                HP = 6;
                ChoiceLvl();
            }
            else
            {
                Console.Clear();
            }
        }

        static void printVictory() //Print the victory of the player.
        {

            Console.Clear();
            Console.WriteLine("Bravo ! tu as gagné tu es le grand champion du PENDU !");
            Console.WriteLine("Le mot était : " + Word);
            Console.WriteLine("Tu veux refaire une partie ? si oui écris 1 ");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                Console.Clear();
                NotLetterInWord.Clear();
                Word = null;
                PrintableTab = null;
                HP = 6;
                ChoiceLvl();
            }
            else
            {
                Console.Clear(); 
            }
        }

        static bool isWIn() //return true if the array printableTab (witch countains every letters that the player give us and wich are in the word) and the word are equal : win 
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

        static bool isInWord(char charChoose) // return true if the char is in word
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

        static char ChoiceChar() //permit to choose a char 
        {
            Console.WriteLine("Donnes nous une lettre :");
            string character = Console.ReadLine();
            char Charchoice;
            bool notInTabPrintable = false;
            bool notInTabNotLetterInWord = false;
            
            if (character.Length == 1)
            {
                Charchoice = Char.Parse(character);
                Charchoice = Char.ToLower(Charchoice);
                foreach (char charP in PrintableTab)
                {
                    if (charP ==Charchoice)
                    {
                        notInTabPrintable = true;
                    }
                }
                foreach (char charN in NotLetterInWord)
                {
                    if (charN == Charchoice)
                    {
                        notInTabNotLetterInWord = true;
                    }
                }
                
                
                    if ( Charchoice >= 'a' && Charchoice <= 'z' && !notInTabNotLetterInWord && !notInTabPrintable)
                    {
                        return Charchoice;
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
