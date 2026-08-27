using MiniProject_Working1.Models;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace MiniProject_Working1.Services;

internal class SpectreUi : IListUiInterface
{
    private ListContainer _lc;
    public List<TaskObject> SelectedList { get; set; } // int
    public TaskObject SelectedTask { get; set; }
    public AppStatus Status { get; set; } = AppStatus.Loading;
    public bool SearchOrFilterEnabled { get; set; } = false;

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
        // hide cursor  (only show cursor when editing)

        ShowSplash();

        ShowHeader();


        //// main loop
        //ConsoleKeyInfo keyInfo;
        //do
        //{
        //    // live display required when selecting... on main screens..
        //    // exit live display when taking input from user... 
        //
        //    // show header
        //    // show container or individual list
        //    // show options
        //
        //    //Console.SetCursorPosition(0, 24);
        //    Console.WriteLine("\n\n\nESC to exit.");
        //} while (keyInfo.Key != ConsoleKey.Escape);
        //===========================================================

        // show cursor again upon exit
    }
    //------------------------------------
    public void ShowHeader() 
    {
        //AnsiConsole.Cursor.Show();
        //Console.SetCursorPosition(0, (maxRowReached* 2) + 3);
        //Console.SetCursorPosition(branchX, screenRow + 1);
        //AnsiConsole.Markup("[yellow]\\[/]")
        //AnsiConsole.Cursor.Hide();
        //int maxRowReached = 0;

        // reformat this to look good
        Console.WriteLine("UP/DOWN/ENT - Select");
        Console.WriteLine("SPC - Toggle Active");
        Console.WriteLine("N - Create New");
        Console.WriteLine("E - Edit Subject");
        Console.WriteLine("PAGE UP/DOWN - Reorder");
        Console.WriteLine("DEL - Delete List");
        Console.WriteLine("S - Search");
        Console.WriteLine("F - Filter");
        Console.WriteLine("ESC - Show All / Exit");
        Console.WriteLine("U - Show User Info");
        Console.WriteLine("A - About App");
    }
    //------------------------------------
    public void ShowLists(List<ListObject> lists) 
    {
        Status = AppStatus.ShowLists;
        
        // loop {

            // print out lists, with selected list in special font

            // accept user input, process to determine next steps

        // end loop

        
        // return to loading upon exit from this stage
        Status = AppStatus.Loading;
    }
    //------------------------------------
    public void ShowTasks(List<TaskObject> tasks) 
    {
        Status = AppStatus.ShowTasks;

        // loop {

        // print out items of selected list, with selected item in special font

        // accept user input, process to determine next steps

        // end loop


        // return to loading upon exit from this stage
        Status = AppStatus.Loading;
    }
    //------------------------------------
    public void ShowAboutApp()
    {
        Status = AppStatus.ShowAboutApp;

        // loop for 10 sec {

        // print out About app // splashscreen again?

        // accept any key to quit early

        // end loop


        // return to loading upon exit from this stage
        Status = AppStatus.Loading;
    }
    //------------------------------------
    public void ShowUserInfo()
    {
        Status = AppStatus.ShowUserInfo;

        // loop {

        // print out user info

        // accept user input if E or Enter, allow to edit user info

        // end loop


        // return to loading upon exit from this stage
        Status = AppStatus.Loading;
    }
    //------------------------------------
    public void EditUserInfo()
    {
        //dedicated input to edit user info here

        // likely not asynch/threaded
    }
    //------------------------------------
    public AppStatus HandleUserInput(AppStatus curStatus) 
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // 'intercept' prevents input from echoing to screen

        switch (Status) {
            case AppStatus.ShowLists:
            case AppStatus.ShowTasks:
                switch (keyInfo.Key)
                {
                    //case ConsoleKey.UpArrow:
                    //    return UserIntention.ArrowUp;
                    //case ConsoleKey.DownArrow:
                    //    return UserIntention.ArrowDown;
                    //case ConsoleKey.Enter:
                    //    if (Status == AppStatus.ShowLists)
                    //        return UserIntention.Open; // enter to open a list and show tasks inside
                    //    if (Status == AppStatus.ShowTasks)
                    //        return UserIntention.EditName; // enter to edit name if showing tasks already
                    //    break;
                    //case ConsoleKey.Spacebar:
                    //    return UserIntention.Toggle;
                    //case ConsoleKey.N:
                    //    return UserIntention.CreateNew;
                    //case ConsoleKey.E:
                    //    return UserIntention.EditName;
                    //case ConsoleKey.PageUp:
                    //    return UserIntention.MoveUp;
                    //case ConsoleKey.PageDown:
                    //    return UserIntention.MoveDown;
                    //case ConsoleKey.Delete:
                    //    return UserIntention.Delete;
                    //case ConsoleKey.S:
                    //    return UserIntention.Search;
                    //case ConsoleKey.F:
                    //    return UserIntention.Filter;
                    //case ConsoleKey.Escape:
                    //    if (SearchOrFilterEnabled) return UserIntention.ShowAll; // clears search or filter if either active
                    //    return UserIntention.Escape; // or escape if neither active
                    //case ConsoleKey.A:
                    //    return UserIntention.ShowAppInfo;
                    //case ConsoleKey.U:
                    //    return UserIntention.ShowUserInfo;
                    default:
                        Console.Beep(); // if key not appropriate, beep to indicate so to user
                        break;
                }
                break;

            //case AppStatus.ShowAboutApp:
            //    return UserIntention.Escape; // any key to exit this screen

            case AppStatus.ShowSplashScreen: // input read another way in ShowSplashScreen
            case AppStatus.Loading: // accept no input while in this status
            default:
                break; 
        }
        return AppStatus.ShowLists;
        
        // extra code
        // //debug captured data
        //Console.WriteLine($"\nKey Pressed: {keyInfo.Key}");
        //Console.WriteLine($"Character: {keyInfo.KeyChar}");
        //Console.WriteLine($"Modifiers: {keyInfo.Modifiers}");
    }
    //------------------------------------
    public void ShowSplash()
    {
        // show splashscreen (title, version, author) at the start
        Status = AppStatus.ShowSplashScreen;

        // Note: some of this code was initially derived from AI-refactoring of my
        // original code beneath this function, since then I've adjusted/enlarged
        // it to accomodate more functionality, key to escape. To see original
        // code, see function ShowSplashOriginal() below.

        // define a panel (small red rectangle) to show briefly before the full splashscreen
        var panel = new Panel("").BorderColor(Color.Red);  
        var centeredPanel = Align.Center(panel);

        // use IRenderable factory pattern that builds layout with parameters, for custom appearance trick
        IRenderable BuildMainSplashPanel(string version = " ", 
                                         string author = " ", 
                                         string userName = " ", 
                                         string emailContact = " ", 
                                         string ifFoundContact = " ",
                                         string pressAnyKey = " ")
        {
            var spacer = new Text(" ");
            var combinedContent = new Rows(
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new FigletText(_lc.AppName).Centered().Color(Color.Cyan), // splash panel shows title first
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new Markup( version ).Centered(), 
                new Markup( author  ).Centered(),
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new Markup($"[yellow]{Markup.Escape( userName )}[/]").Centered(), 
                new Markup($"[yellow]{Markup.Escape( emailContact )}[/]").Centered(), 
                new Markup($"[yellow]{Markup.Escape( ifFoundContact )}[/]").Centered(), 
                new Padder(spacer, new Padding(0, 1, 0, 0)),
                new Markup($"[blink white]{Markup.Escape(pressAnyKey)}[/]").Centered()
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
                int elapsed = 0;
                while (elapsed < ms)
                {
                    if (Console.KeyAvailable)
                    {
                        //AnsiConsole.Console.Input.ReadKey(intercept: true); // simple process keypress
                        break;
                    }
                    Thread.Sleep(100);
                    elapsed += 100;
                }
            }

            // Frame 1: Show initial small red square
            RenderFrame(Align.Center(centeredPanel), 200);

            // Frame 2: Show app title centered
            RenderFrame(BuildMainSplashPanel(), 300);

            // Frame 3: show app title, with version date underneath it
            RenderFrame(BuildMainSplashPanel(formatAppVersionDate()), 200);

            // Frame 4: show app title, version date, and author underneath it
            RenderFrame(BuildMainSplashPanel(formatAppVersionDate(), formatAppAuthor()), 300);

            // Frame 5: show app title, version date, author and USER INFORMATION
            RenderFrame(BuildMainSplashPanel(formatAppVersionDate(), formatAppAuthor(), _lc.UserName, _lc.EmailContact, _lc.IfFoundContact, 
                        "Press Any Key To Continue" ), 10000);

            // Frame 6: clear view
            RenderFrame(new Text(string.Empty), 0);

            // do not check for any input here; user has to wait the 1.7 seconds for it to dissapear
        });

        //Console.ReadKey();
        Console.Clear(); // reset to top of screen
        
        // reset status to loading
        Status = AppStatus.Loading;
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
                new Text(formatAppVersionDate(), new Style(Color.Grey)).Centered(),
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
                new Text(formatAppVersionDate(), new Style(Color.Grey)).Centered(),
                new Text(formatAppAuthor(), new Style(Color.Grey)).Centered(),
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

