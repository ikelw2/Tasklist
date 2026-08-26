using MiniProject_Working1.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Services;

internal class SpectreUi : IListUiInterface
{
    public List<TaskObject> SelectedList { get; set; }
    public TaskObject SelectedTask { get; set; }
    public ViewStatus ViewMode { get; set; }

    
    public void ShowSplash(string text) 
    { 
    
    }
    
    public void ShowHeader(string text) 
    { 
    
    }
    
    public void ShowLists(List<ListObject> lists) 
    { 
    
    }
    
    public void ShowItems(List<TaskObject> tasks) 
    { 
    
    }

    public void ShowAboutApp()
    {

    }

    public void ShowUserInfo()
    {

    }

    public void EditUserInfo()
    {

    }

    public ListUiCommand GetUserInput() 
    {
        return ListUiCommand.NoCommand;
    }

}


/// /////////////////////////////////////////////////////////////////////////////////
//SpectrePreloaded.StartupPanel("SA_4_3_1", "Calculate electricity bill with ranges");
////Console.WriteLine("SA_4_3_1 Calculate electricity bill with ranges");
////SpectrePreloaded.HighlightMethod("Method 1", "tbd", 1);
//while (true)
//{
//    Console.WriteLine("test 123");
//    if (SpectrePreloaded.AskUserToContinue() == false) { break; }
//}
//SpectrePreloaded.ShutdownTasks(doReadline: false, doClear: false);
//////////////////////////////// USE ABOVE FOR NEW CONSOLE PROGRAMS //////////////

internal static class zExtraSpectreCode
{
    public static void StartupPanel(string shortTitle, string description)
    {
        AnsiConsole.Write(new Panel($"[gray]{shortTitle}: [/]{description}").BorderColor(Color.MediumVioletRed));
    }

    public static void HighlightMethod(string shortTitle, string description, int linesFollowing = 1)
    {
        AnsiConsole.MarkupLine($"\n\n[red on white]{shortTitle}: {description}[/]");
        for (int i = 0; i < linesFollowing; i++)
        {
            Console.WriteLine();
        }
    }
    //===========================================================================
    public static bool PrimitiveDoesUserWantToQuit()
    {
        Console.Write($"\nEnter Q to quit or press ENTER to continue.");
        string userInput = Console.ReadLine();
        bool result = (userInput.Trim().Equals("q", StringComparison.OrdinalIgnoreCase) == true);
        Console.WriteLine("-------------------------------");
        return result;
    }
    //===========================================================================
    public static bool AskUserToContinue()
    {
        // 1. Create a selection prompt (drop-down style)
        var prompt = new SelectionPrompt<string>()
            .Title("\nDo you wish to continue?")
            .AddChoices(new[] { "yes", "no" })
            .DefaultValue("yes");

        // 2. Use Live Display context or Status to auto-hide the console menu
        string selection = AnsiConsole.Live(new Text("")).Start(ctx =>
        {
            // Present the prompt to the user
            return AnsiConsole.Prompt(prompt);
        });

        bool result = selection.Equals("yes", StringComparison.OrdinalIgnoreCase);
        Console.WriteLine("-------------------------------");
        return result;
    }
    //===========================================================================
    public static void ShutdownTasks(bool doReadline = false, bool doClear = false)
    {
        if (doReadline)
            Console.ReadLine();
        if (doClear)
            Console.Clear();
    }
    //===========================================================================
    public static string AskUserForString()
    {
        string inputString = AnsiConsole.Prompt(
            new TextPrompt<string>("Please input a string: ")
            );
        return inputString;
    }
    //===========================================================================
    public static bool AskUserYesOrNoQuestion(string question, bool defaultAnswer = true)
    {
        // 1. Create a selection prompt (drop-down style)
        var prompt = new SelectionPrompt<string>()
            .Title(question)
            .AddChoices(new[] { "yes", "no" })
            .DefaultValue(defaultAnswer ? "yes" : "no");

        // 2. Use Live Display context or Status to auto-hide the console menu
        string selection = AnsiConsole.Live(new Text("")).Start(ctx =>
        {
            // Present the prompt to the user
            return AnsiConsole.Prompt(prompt);
        });
        return selection.Equals("yes", StringComparison.OrdinalIgnoreCase);
    }
    //===========================================================================
    public static bool GetStringAskUserYesNoQuestion(string prompt)
    {
        string? answer = AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            //.AddChoice("y")
            //.AddChoice("n")
            .DefaultValue("y")
            .InvalidChoiceMessage("[red]Invalid option.[/] Please reply with n or press enter for y.")
            .Validate(input =>
            {
                string cleanInput = input.Trim().ToLower();
                return cleanInput is "y" or "n"
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Invalid option![/] Please reply with n or press enter for y.");
            }));
        if (answer != null)
        {
            if (answer.Equals("yes") || answer.Equals("y"))
                return true;
            else
                return false;
        }
        Console.WriteLine("error at end of line");
        Console.Beep();
        return false;
    }
}

