using System;
using System.Windows.Forms;
using System.IO;

namespace Cesar_Cipher
{
    class Cipher
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Write("Enrypt or decrypt? (e/d) (q to quit): ");
            string selection = Console.ReadLine();
            selection = selection.ToLower();
            while (selection != "q")
            {
                switch (selection)
                {
                    case "e":
                        Console.Write("\n");
                        encrypt();
                        break;
                    case "d":
                        Console.Write("\n");
                        decrypt();
                        break;
                    case "q":
                        break;
                    default:
                        Console.WriteLine("Error: Comand not recognized.");
                        Console.Write("\n");
                        break;
                }
                Console.Write("\nEnrypt or decrypt? (e/d) (q to quit): ");
                selection = Console.ReadLine();
                selection = selection.ToLower();
                Console.Write("\n");
            }
            Console.Write("Exiting the program. Press any key to contunue..");
            Console.ReadKey();
        }

        public static void encrypt()
        {
            Console.WriteLine("Specify a file that you want to encrypt.");
            string path;
            path = getFile();
            if (path != null)
                shiftKeys(path, "encrypt");
            else
            {
                Console.WriteLine("Error: Could not open file. Check user privilages or file protection.");
                Console.WriteLine("EReturning to the main menu.");
                return;
            }
        }
        public static void shiftKeys(string fileName, string command)
        {
            Console.Write("How many positions do you wish to shift the index? (Type 'help' for help): ");
            string option = Console.ReadLine();
            Console.Write("\n");
            int shiftIndex = 0;
            option = option.ToLower();

            if (option.Equals("help"))
            {
                Console.Write("\n");
                Console.WriteLine("The shift index states how far you wish to shift the key values. for example..");
                Console.WriteLine("If you wished to shift by an index of 2, the letter 'A' will be replaced by the letter 'C'.");
                Console.WriteLine("The letter 'C' will be replaced by the letter 'E' and so on...");
                Console.Write("\nHow many positions do you wish to shift the index? (Type 'help' for help): ");
                option = Console.ReadLine();
                Console.Write("\n");
            }

            for (int i = 0; i <= 4; i++)
            {
                try
                {
                    shiftIndex = Convert.ToInt32(option.Trim());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: the shift index must be a number.");
                    Console.Write("How many positions do you wish to shift the index?: ");
                    option = Console.ReadLine();
                    Console.Write("\n");
                }

                if (i == 4)
                {
                    Console.WriteLine("It appears you are having trouble, returning to the main menu.");
                    Console.Write("\n");
                    return;
                }
            }

            string plaintext = System.IO.File.ReadAllText(fileName);

            plaintext = plaintext.ToUpper();
            string alteredText = "";
            char letter;
            for (int i = 0; i < plaintext.Length; i++)
            {
                letter = plaintext[i];
                if ((letter >= 'a' && letter <= 'z') || (letter >= 'A' && letter <= 'Z'))
                {
                    if (command.Equals("encrypt"))
                        letter = (char)(letter + (shiftIndex % 26));
                    if (command.Equals("decrypt"))
                        letter = (char)(letter - (shiftIndex % 26));

                    if (letter > 'Z')
                        letter = (char)(letter - 26);
                    if (letter < 'A')
                        letter = (char)(letter + 26);
                    alteredText += letter;

                    Console.WriteLine(plaintext[i] + " ---> " + letter);
                }
                else
                    alteredText += letter;
            }

            Console.WriteLine("Printing file to desktop.");
            string path = "";
            if (command.Equals("encrypt"))
                path = @"c:\Users\" + Environment.UserName + @"\Desktop\encrypted.txt"; ;
            if (command.Equals("decrypt"))
                path = @"c:\Users\" + Environment.UserName + @"\Desktop\decrypted.txt";

            using (StreamWriter sw = new StreamWriter(path,false))
            {
                sw.WriteLine(alteredText);
            } 
        }

        public static void decrypt()
        {
            Console.WriteLine("Specify a file that you want to decrypt.");
            string path;
            path = getFile();
            if (path != null)
                shiftKeys(path, "decrypt");
            else
            {
                Console.WriteLine("Error: Could not open file. Check user privilages or file protection.");
                Console.WriteLine("EReturning to the main menu.");
                return;
            }
        }

        public static String getFile()
        {
            OpenFileDialog fileSelector = new OpenFileDialog();

            fileSelector.Title = "Browse Files";
            fileSelector.InitialDirectory = @"c:\Users\" + Environment.UserName + "\\Desktop";
            fileSelector.Filter = "txt files (*.txt)|*.txt";

            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                return fileSelector.FileName;
            }
            else
            {
                Console.WriteLine("Error: Could not open file.");
                return null;
            }
        }
    }
}
