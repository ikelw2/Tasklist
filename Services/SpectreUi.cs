using MiniProject_Working1.Models;

using Spectre.Console;
using Spectre.Console.Rendering;
using System.Threading;

using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Services;

internal class SpectreUi : IListUiInterface
{
    private ListContainer _lc;
    public List<TaskObject> SelectedList { get; set; }
    public TaskObject SelectedTask { get; set; }
    public ViewStatus ViewMode { get; set; }

    public Table ViewTable { get; set; } = new Table();
    public int ViewTotalColumns { get; set; }
    public int ViewTotalRows { get; set; }



    public SpectreUi(ListContainer listContainer)
    {
        _lc = listContainer;
    }
    //------------------------------------
    

    //AnsiConsole.Cursor.Show();
    //Console.SetCursorPosition(0, (maxRowReached* 2) + 3);
    //Console.SetCursorPosition(branchX, screenRow + 1);
    //AnsiConsole.Markup("[yellow]\\[/]")
    //AnsiConsole.Cursor.Hide();
    //int maxRowReached = 0;
    //------------------------------------
    public void Start()
    {
        ShowSplash();

        ConsoleKeyInfo keyInfo;

        // main loop
        do
        {
            
            
            // show header
            // show container or individual list
            // show options


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
    //------------------------------------
    public void ShowHeader(string text) 
    { 
    
    }
    //------------------------------------
    public void ShowLists(List<ListObject> lists) 
    { 
    
    }
    //------------------------------------
    public void ShowItems(List<TaskObject> tasks) 
    { 
    
    }
    //------------------------------------
    public void ShowAboutApp()
    {

    }
    //------------------------------------
    public void ShowUserInfo()
    {

    }
    //------------------------------------
    public void EditUserInfo()
    {

    }
    //------------------------------------
    public ListUiCommand GetUserInput() 
    {
        return ListUiCommand.NoCommand;
    }
    //------------------------------------
    public void ShowSplash() 
    {
        // show splashscreen (title, version, author) at the start

        var panel = new Panel("").BorderColor(Color.Red);
        var centeredPanel = Align.Center(panel).Width(50);

        AnsiConsole.Live(centeredPanel).Start(ctx =>
        {
            ctx.UpdateTarget(Align.Center(centeredPanel));
            ctx.Refresh();
            Thread.Sleep(200);
            // show 0.2 sec of small red square

            var spacer = new Text(" ");
            var combinedContent = new Rows(
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new FigletText(_lc.AppName).Centered().Color(Color.Cyan),
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new Text(" ", new Style(Color.Grey)).Centered(),
                new Text(" ", new Style(Color.Grey)).Centered(),
                new Padder(spacer, new Padding(0, 1, 0, 0))
            );
            var updatedPanel = new Panel(combinedContent).BorderColor(Color.Red);
            ctx.UpdateTarget(Align.Center(updatedPanel));
            ctx.Refresh();
            Thread.Sleep(300);
            // show 0.3 sec of large box with app title centered

            combinedContent = new Rows(
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new FigletText(_lc.AppName).Centered().Color(Color.Cyan),
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new Text(_lc.AppVersionDate, new Style(Color.Grey)).Centered(),
                new Text(" ", new Style(Color.Grey)).Centered(),
                new Padder(spacer, new Padding(0, 1, 0, 0))
            );
            updatedPanel = new Panel(combinedContent).BorderColor(Color.Red);
            ctx.UpdateTarget(Align.Center(updatedPanel));
            ctx.Refresh();
            Thread.Sleep(200);
            // show 0.2 sec of app title centered, and app version beneath it

            combinedContent = new Rows(
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new FigletText(_lc.AppName).Centered().Color(Color.Cyan),
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new Text(_lc.AppVersionDate, new Style(Color.Grey)).Centered(),
                new Text(_lc.AppAuthor, new Style(Color.Grey)).Centered(),
                new Padder(spacer, new Padding(0, 1, 0, 0))
            );
            updatedPanel = new Panel(combinedContent).BorderColor(Color.Red);
            ctx.UpdateTarget(Align.Center(updatedPanel));
            ctx.Refresh();
            Thread.Sleep(800);
            // show 0.8 sec of Author, beneath version date, beneath app title

            ctx.UpdateTarget(new Text(string.Empty));
            ctx.Refresh();
            // then reset the screen how it was before
        });

        Console.ReadKey();
        Console.Clear();
    }
    //------------------------------------
    public void getScreenDimensions()
    {
        ViewTotalColumns = Console.WindowWidth;
        ViewTotalRows = Console.WindowHeight;
        AnsiConsole.WriteLine($"Screen size: {ViewTotalColumns} characters wide x {ViewTotalRows} text lines high.");
    }
    //------------------------------------
}

/////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================
////////==========================================================================================

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

