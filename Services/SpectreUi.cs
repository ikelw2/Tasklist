using MiniProject_Working1.Models;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
            
            // live display required when selecting... on main screens..
            // exit live display when taking input from user... 

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

        // Note: this function derived from AI-derived refactoring of my code,
        // with slight adjustments and my comments to better understand it.
        // See original function below for my original attempt.

        // define a panel (small red rectangle) to show briefly before the full splashscreen
        var panel = new Panel("").BorderColor(Color.Red);  
        var centeredPanel = Align.Center(panel);

        // use IRenderable factory pattern that builds layout with parameters, for custom appearance trick
        IRenderable BuildMainSplashPanel(string version, string author)
        {
            var spacer = new Text(" ");
            var combinedContent = new Rows(
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new FigletText(_lc.AppName).Centered().Color(Color.Cyan), // splash panel shows title first
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new Markup( version ).Centered(), // markup handles colors, so I added colors
                new Markup( author  ).Centered(),
                new Padder(spacer, new Padding(0, 1, 0, 0))
            );

            return Align.Center(new Panel(combinedContent).BorderColor(Color.Red));
        }

        // run live to prevent clipping/flashing on display
        AnsiConsole.Live(Align.Center(centeredPanel)).Start(liveDisplayContext => // code block
        {
            // helper function to deduplicate the Update/Refresh pipeline
            void RenderFrame(IRenderable target, int ms)
            {
                liveDisplayContext.UpdateTarget(target); // swap target to current view without clipping/flashing
                liveDisplayContext.Refresh(); // refresh screen so user sees change immediately
                if (ms > 0) 
                    Thread.Sleep(ms); // wait a specified amount of ms (500 = 0.5 sec)
            }

            // Frame 1: Show initial small red square
            RenderFrame(Align.Center(centeredPanel), 200);

            // Frame 2: Show app title centered
            RenderFrame(BuildMainSplashPanel(" ", " "), 300);

            // Frame 3: show app title, with version date underneath it
            RenderFrame(BuildMainSplashPanel(formatAppVersionDate(), " "), 200);

            // Frame 4: show app title, version date, and author underneath it
            RenderFrame(BuildMainSplashPanel(formatAppVersionDate(), formatAppAuthor()), 1000);

            // Frame 5: clear view
            RenderFrame(new Text(string.Empty), 0);

            // do not check for any input here; user has to wait the 1.7 seconds for it to dissapear
        });

        //Console.ReadKey();
        Console.Clear(); // reset to top of screen
    }
    //------------------------------------
    public void ShowSplashOriginal()
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
            Thread.Sleep(1000);
            // show 1 sec of Author, beneath version date, beneath app title

            ctx.UpdateTarget(new Text(string.Empty));
            ctx.Refresh();
            // then reset the screen how it was before
        });

        //Console.ReadKey();
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
    public string formatAppVersionDate() // returns _lc.AppVersionDate formatted for spectre markup
    {
        string v = _lc.AppVersionDate;

        if (string.IsNullOrWhiteSpace(v))
            return v;

        string[] words = v.TrimStart().Split(' ', 5); // divide v into five parts by spaces
                                                      // "Version 0.2  Build 2026.08.25";
                                                      // "       ^   ^^     ^          "
        
        if (words.Length == 5) // if successfully found 5 words and 4 spaces
        {
            //foreach (string w in words) Console.WriteLine(w); // debug to check correct selection
            
            if (words[0].Equals("Version") && words[3].Equals("Build")) // if words 1 and 3 are correct
            {
                // add green spectre formatting to 2nd word (index 1)
                // and blue spectre formatting to 4th word (index 3)
                string output = $"{Markup.Escape(words[0])} " + 
                    $"[green]{Markup.Escape(words[1])}[/]  " + 
                    $"{Markup.Escape(words[3])} " +
                    $"[blue]{Markup.Escape(words[4])}[/]";

                return output;
            }
            return "version words Version and Build not correctly located";
        }
        return "version not accurate word count";
    }
    //------------------------------------
    public string formatAppAuthor() // returns _lc.AppAuthor formatted for spectre markup
    {
        string a = _lc.AppAuthor;

        if (string.IsNullOrWhiteSpace(a))
            return a;

        string[] words = a.TrimStart().Split(' ', 4); // divide a into four parts by spaces
                                                      // "By Michael Wood";
                                                      // "  ^       ^    "
        
        if (words.Length == 3) // if successfully found 3 words, and 2 spaces
        {
            //foreach (string w in words) Console.WriteLine(w); // debug to check correct selection

            if (words[0].Equals("By")) // if words 1 is 'By'
            {
                // add green spectre formatting to first character in author first name 2nd word
                // and blue spectre formatting to first character in author second name 3nd word
                string output = $"{Markup.Escape(words[0])} " +
                    $"[green]{Markup.Escape(words[1].Substring(0, 1))}[/]" + // first char 2nd word
                    $"{Markup.Escape(words[1].Substring(1))}" +
                    $"[blue]{Markup.Escape(words[2].Substring(0, 1))}[/]" + // first char 3rd word
                    $"{Markup.Escape(words[2].Substring(1))}";

                return output;
            }
            return "author word By not correctly located";
        }
        return "author not accurate word count";
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

