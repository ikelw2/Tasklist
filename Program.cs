
using MiniProject_Working1.Models;
using Spectre.Console;


class Program
{
    static void Main(string[] args)
    {

        ListContainer LC = new();
        LC.ShowAboutApp();
        LC.EditUserInfo();
        
        Random random = new();
        ConsoleKeyInfo keyInfo;
        // main loop
        do
        {
            // clear each time

            
            
            Console.WriteLine("S / \\ search, N + create new, E DEL edit, ENTER load, SPACE activate, ESC exit");
            //keyInfo = Console.ReadKey();

            // Capture the keystroke
            keyInfo = Console.ReadKey(intercept: true);
            

            // Display captured data
            Console.WriteLine($"\nKey Pressed: {keyInfo.Key}");
            Console.WriteLine($"Character: {keyInfo.KeyChar}");
            Console.WriteLine($"Modifiers: {keyInfo.Modifiers}");

            //Console.SetCursorPosition(0, 24);
            Console.WriteLine("\n\n\nESC to exit.");
        } while (keyInfo.Key != ConsoleKey.Escape);
        //===========================================================



    }
}

