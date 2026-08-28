using MiniProject_Working1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Threading;

namespace MiniProject_Working1.Services;

internal class ConsoleUi : IListUiInterface
{
    private ListContainer _lc;
    public int SelectedList { get; set; } = 0;
    public int SelectedTask { get; set; } = 0;
    public AppStatus Status { get; set; } = AppStatus.Loading;
    public bool ListSearchOrFilterEnabled { get; set; } = false;
    public bool TaskSearchOrFilterEnabled { get; set; } = false;
    public int ViewTotalWidth { get; set; }
    public int ViewTotalHeight { get; set; }

    public bool EditSearchString { get; set; } = false;

    public string ListSearchString { get; set; } = string.Empty;
    public string TaskSearchString { get; set; } = string.Empty;
    public int ListFilter { get; set; } = 0;
    public int TaskFilter { get; set; } = 0;

    public int SelectionRow { get; set; } = 5;
    public int longestTextEntry { get; set; } = 0;



    public ConsoleUi(ListContainer listContainer)
    {
        _lc = listContainer;
        ViewTotalHeight = Console.WindowHeight;
        ViewTotalWidth = Console.WindowWidth;

        _lc = MockData.Load();

    }
    //------------------------------------
    public void Start()
    {
        Console.CursorVisible = false;

        Status = AppStatus.ShowSplashScreen; // start w splash screen
        // main loop here
        do { 
            UpdateWindowDimensions();
            Console.Clear();

            switch (Status) {
                case AppStatus.ShowSplashScreen:
                    ShowSplash();
                    break;
                case AppStatus.ShowAboutApp:
                    ShowAboutApp();
                    break;
                case AppStatus.ShowLists:
                    if (ListFilter != 0)
                        ShowLists(_lc.ListFilter(ListFilter));
                    else if (ListSearchString.Length != 0)
                        ShowLists(_lc.ListSearch(ListSearchString));
                    else
                        ShowLists(_lc);
                    break;
                case AppStatus.ShowTasks:
                    if (TaskFilter != 0)
                        ShowTasks(_lc[SelectedList].TaskFilter(TaskFilter));
                    else if (TaskSearchString.Length != 0)
                        ShowTasks(_lc[SelectedList].TaskSearch(TaskSearchString));
                    else
                        ShowTasks(_lc[SelectedList]);
                    break;
                case AppStatus.ShowUserInfo:
                    ShowUserInfo();
                    break;
                case AppStatus.EditUserInfo:
                    EditUserInfo();
                    break;

                case AppStatus.EditLists:
                    EditValue(TrueForListFalseForTask: true);
                    break;

                case AppStatus.EditTasks:
                    EditValue(TrueForListFalseForTask: false);
                    break;

                default:
                    break;
            }
            Status = HandleUserInput(Status); // assign user input to cmd
        } while (Status != AppStatus.Escape);

        Console.Clear();
        Console.CursorVisible = true;
        Console.WriteLine("\n\nGoodbye!\n\n");
        //===========================================================
    }
    //------------------------------------
    public void ShowHeader() 
    {
        Console.SetCursorPosition(0, 0);
        if ((Status == AppStatus.EditLists) || (Status == AppStatus.EditTasks))
        {
            Console.WriteLine("  ***** Editing Mode - Type in a new value and press ENTER *****                                                          " +
            "                                                                                                                  ");
        }
        else if (EditSearchString)
        {
            Console.WriteLine("  ***** Enter the text value you would like to search for *****                                                           " +
            "                                                                                                                  ");
        }
        else
        {
            Console.WriteLine("ESC - Exit   |   S - Search   |   F - Filter   |   PAGE UP/DOWN - Reorder   |   U - Show User Info   |   A - About App   |" +
            "   UP/DOWN/ENTER - Select   |   SPACE - Toggle Active   |   N - Create New   |   E - Edit Names   |   DEL - Delete");
        }
        Console.WriteLine();

        string searchActive = string.Empty;
        string searchString = string.Empty;
        string activeFilter = "None";
        if (Status == AppStatus.ShowLists)
        {
            searchActive = (ListSearchOrFilterEnabled ? "  [Search/Filter Active]  " : " [Search/Filter Inactive] ");
            if (ListFilter != 0)
                activeFilter = (ListFilter == 1 ? "Active Only" : "Inactive Only"); 
            else
                searchString = ListSearchString;
            
        }
        if (Status == AppStatus.ShowTasks)
        {
            searchActive = (TaskSearchOrFilterEnabled ? "  [Search/Filter Active]  " : " [Search/Filter Inactive] ");
            if (TaskFilter != 0)
                activeFilter = (TaskFilter == 1 ? "Complete Only" : "Incomplete Only");
            else
                searchString = TaskSearchString;
            
        }

        (int left, int top) = Console.GetCursorPosition();
        // print search string
        Console.SetCursorPosition(0, top);
        Console.Write($"{searchActive}     ");

        // (ShowLists AND ListEnabled) OR (ShowTasks AND TaskEnabled)
        if ((Status == AppStatus.ShowLists && ListSearchOrFilterEnabled) || (Status == AppStatus.ShowTasks && TaskSearchOrFilterEnabled))
        {
            // print out exactly what the search string or filter setting is... 
            //Console.SetCursorPosition(26, top); // go to after searchActive string...
            if (searchString.Length != 0)
                Console.WriteLine($"Search String: '{searchString}'");
            else
                Console.WriteLine($"Filter: {activeFilter}");
        } 
        else
        {
            Console.WriteLine();
        }

        //Console.WriteLine("total width 120 = " + ViewTotalWidth + "    total height 30 = " + ViewTotalHeight);
        Console.WriteLine(new string('_', ViewTotalWidth));
        Console.WriteLine();
    }
    //------------------------------------
    public string TrimAndFormat(string text, bool dashed, int totalLength) //
    {
        StringBuilder sb = new("   "); // start with two spaces

        sb.Append(text); // append the provided text

        int charsToAppend = totalLength - text.Length + 5; // determine how many characters remaining in width
        if (charsToAppend > ViewTotalWidth - 40 - text.Length) // excluding 40 used by other fields
            charsToAppend = ViewTotalWidth - 40 - text.Length;

        sb.Append(' ', charsToAppend); // append spaces up to to five chars after the totalLength

        if (dashed) // if not active, replace all spaces with dashes
            sb.Replace(' ', '-');

        return sb.ToString();
    }
    //------------------------------------
    public void ShowLists(List<ListObject> lists) 
    {
        longestTextEntry = 0;
        for (int i = 0; i < lists.Count; i++)
        {
            if (lists[i].Subject.Length > longestTextEntry)
                longestTextEntry = lists[i].Subject.Length;
        }
        
        ShowHeader();
        (int left, int top) = Console.GetCursorPosition();

        // print out each list subject
        for (int line = 0; line < lists.Count; line++)
        {
            int screenRow = line + top;
            Console.SetCursorPosition(0, screenRow);

            if (screenRow < ViewTotalHeight) // console limitation - screen height
            { 
                string SelectionArrow = "  ";
                if (line == SelectedList)
                {
                    SelectionArrow = "->";
                    SelectionRow = top + line; // save this for editing subjects
                }

                Console.Write($" {SelectionArrow} {line,2}. " + // adds 7 chars
                    $"{TrimAndFormat(lists[line].Subject, !lists[line].IsActive, longestTextEntry)}"); // + 
                    //$"     ({lists[line].LastAccess.ToString()})"); // adds 30 chars
            }
        }
    }
    //------------------------------------
    public void ShowTasks(List<TaskObject> tasks) 
    {
        longestTextEntry = 0;
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].Task.Length > longestTextEntry)
                longestTextEntry = tasks[i].Task.Length;
        }

        ShowHeader();
        (int left, int top) = Console.GetCursorPosition(); // use tuple construct here for simplicity

        for (int line = 0; line < tasks.Count; line++)
        {
            int screenRow = line + top;
            Console.SetCursorPosition(0, screenRow);

            if (screenRow < ViewTotalHeight)
            {
                string SelectionArrow = "  ";
                if (line == SelectedTask)
                {
                    SelectionArrow = "->";
                    SelectionRow = top + line; // save this for editing tasks
                }

                Console.Write($" {SelectionArrow} {line,2}. {TrimAndFormat(tasks[line].Task, tasks[line].Done, longestTextEntry)}");
            }
        }
    }
    //------------------------------------
    public void ShowUserInfo()
    {
        Console.WriteLine();
        Console.WriteLine(" User Information:  Press E to Edit");
        Console.WriteLine();
        Console.WriteLine(_lc.UserName);
        Console.WriteLine(_lc.EmailContact);
        Console.WriteLine(_lc.IfFoundContact);
        Console.WriteLine();
    }
    //------------------------------------
    public void EditUserInfo()
    {
        string? input = string.Empty;

        //Console.Clear();
        Console.WriteLine(); 
        Console.WriteLine(" ***** Editing User Information ***** ");
        Console.WriteLine();

        // collect User Name
        Console.Write("Enter your User Name: ");
        (int left, int top) = Console.GetCursorPosition();
        do
        {
            Console.SetCursorPosition(left, top);
            Console.Write(new string('_', 30));
            Console.SetCursorPosition(left, top);
            
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                Console.Beep();
        } while (string.IsNullOrWhiteSpace(input));
        _lc.UserName = "User Name: " + input;

        // collect Email Contact
        Console.Write("Enter your Email Contact: ");
        (left, top) = Console.GetCursorPosition();
        do
        {
            Console.SetCursorPosition(left, top);
            Console.Write(new string('_', 30));
            Console.SetCursorPosition(left, top);
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || !input.Contains("@") || !input.Contains("."))
                Console.Beep();
        } while (string.IsNullOrWhiteSpace(input) || !input.Contains("@") || !input.Contains("."));
        _lc.EmailContact = "Email Contact: " + input;

        // collect If Found Contact
        Console.Write("Enter your If Found Contact: ");
        (left, top) = Console.GetCursorPosition();
        do
        {
            Console.SetCursorPosition(left, top);
            Console.Write(new string('_', 30));
            Console.SetCursorPosition(left, top);
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                Console.Beep();
        } while (string.IsNullOrWhiteSpace(input));
        _lc.IfFoundContact = "If Found Contact: " + input;

        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------");
        //Console.Clear();
        ShowUserInfo();
        Console.Write(" Press ENTER to confirm user information.");
        ConsoleKeyInfo confirm = Console.ReadKey(intercept: true); // 'intercept' prevents input from echoing to screen

        if (confirm.Key == ConsoleKey.Enter) // if confirmed, proceed as if showUserInfo
        {
            Console.Clear();
            ShowUserInfo();
            Status = AppStatus.ShowUserInfo; 
        }
    }
    //------------------------------------
    public bool DeleteValue(bool TrueForListFalseForTask)
    {
        int Col = 11; // represents the column at which the values begin to be printed

        // print accepted input string and allow user to accept or reject changes
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("  ***** Confirm Edits - Press Enter to confirm your changes *****                                                         " +
            "                                                                                                                  ");
        Console.SetCursorPosition(Col, SelectionRow);
        if (TrueForListFalseForTask)
            Console.Write(_lc[SelectedList].Subject);
        else
            Console.Write(_lc[SelectedList][SelectedTask].Task);
        Console.Write(" <-- Press ENTER to confirm delete");
        ConsoleKeyInfo confirm = Console.ReadKey(intercept: true); // 'intercept' prevents input from echoing to screen

        if (confirm.Key == ConsoleKey.Enter)
            return true;
        return false;
    }
    //------------------------------------
    public void EditValue(bool TrueForListFalseForTask)
    {
        int Col = 11; // represents the column at which the values begin to be printed
        
        // print out header and lists like normal
        if (TrueForListFalseForTask)
            ShowLists(_lc);
        else
            ShowTasks(_lc[SelectedList]);

        // on the selected item line, replace the text with blanks till end of row
        Console.SetCursorPosition(Col, SelectionRow);
        string blanks = new string('_', longestTextEntry + 5 + 30);
        Console.Write(blanks);

        // move the cursor back to the start of that blank line and let the user enter a string, pressing ENTER when done
        Console.SetCursorPosition(Col, SelectionRow);
        Console.CursorVisible = true;
        string? changedValue = Console.ReadLine();
        Console.CursorVisible = false;

        // if nothing entered, make it an empty string
        if (string.IsNullOrWhiteSpace(changedValue))
            changedValue = string.Empty;

        // clear print out header and lists like normal, to confirm change
        Console.Clear();
        if (TrueForListFalseForTask)
            ShowLists(_lc);
        else
            ShowTasks(_lc[SelectedList]);

        // clear the line of blanks by printing spaces instead
        Console.SetCursorPosition(Col, SelectionRow);
        blanks = new string(' ', longestTextEntry + 5 + 30); // till end of row
        Console.Write(blanks);

        // print accepted input string and allow user to accept or reject changes
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("  ***** Confirm Edits - Press Enter to confirm your changes *****                                                         " +
            "                                                                                                                  ");
        Console.SetCursorPosition(Col, SelectionRow);
        Console.Write(changedValue);
        Console.Write(" <-- Press ENTER to confirm");
        ConsoleKeyInfo confirm = Console.ReadKey(intercept: true); // 'intercept' prevents input from echoing to screen
        
        if (confirm.Key == ConsoleKey.Enter)
            if (TrueForListFalseForTask)
                _lc[SelectedList].Subject = changedValue;
            else
                _lc[SelectedList][SelectedTask].Task = changedValue;

        // reset
        Console.Clear();
        if (TrueForListFalseForTask)
        {
            Status = AppStatus.ShowLists;
            ShowLists(_lc);
        }
        else
        {
            Status = AppStatus.ShowTasks;
            ShowTasks(_lc[SelectedList]);
        }
    }
    //------------------------------------
    public AppStatus HandleUserInput(AppStatus curStatus) 
    {
        // wait and read input from user
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // 'intercept' prevents input from echoing to screen

        AppStatus newStatus = curStatus; // default is stay same status

        switch (curStatus) {
            case AppStatus.ShowLists: // for LISTS //////////////////////////////////

                if (EditSearchString)
                {
                    //Console.SetCursorPosition(0, 10 + _lc.Count);
                    //Console.Write("Search: " + ListSearchString);
                    //Console.CursorVisible = true;
                    switch (keyInfo.KeyChar)
                    {
                        case >= 'a' and <= 'z':
                        case >= 'A' and <= 'Z':
                        case ' ':
                            ListSearchString += keyInfo.KeyChar;
                            break;
                    }
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Delete:
                        case ConsoleKey.Backspace:
                            if (ListSearchString.Length > 0)
                                ListSearchString = ListSearchString.Substring(0, ListSearchString.Length - 1);
                            break;
                        case ConsoleKey.Enter:
                            EditSearchString = false;
                            break;
                        case ConsoleKey.Escape:
                            ListSearchString = string.Empty;
                            EditSearchString = false;
                            ListSearchOrFilterEnabled = false;
                            break;
                    }
                    break;
                }
                // if not editSearchString...
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (SelectedList > 0)
                            SelectedList--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (SelectedList < _lc.Count - 1)
                            SelectedList++;
                        break;
                    case ConsoleKey.Enter:
                        newStatus = AppStatus.ShowTasks;
                        _lc[SelectedList].UpdateLastAccess();
                        break;
                    case ConsoleKey.Spacebar:
                        _lc[SelectedList].ToggleListIsActive();
                        break;
                    case ConsoleKey.N:
                        _lc.CreateNewList();
                        SelectedList = _lc.Count - 1; // Selected is last list, the new one
                        newStatus = AppStatus.EditLists;
                        break;
                    case ConsoleKey.E:
                        newStatus = AppStatus.EditLists;
                        break;
                    case ConsoleKey.PageUp:
                        _lc.ListReorder(SelectedList, true); // decrement List at _lc[SelectedList] by 1
                        if (SelectedList > 0)
                            SelectedList--; // also decrement integer SelectedList 
                        break;
                    case ConsoleKey.PageDown:
                        _lc.ListReorder(SelectedList, false); // increment List at _lc[SelectedList] by 1
                        if (SelectedList < _lc.Count - 1)
                            SelectedList++; // also increment integer SelectedList 
                        break;
                    case ConsoleKey.Delete:
                    case ConsoleKey.Backspace:
                        if (DeleteValue(TrueForListFalseForTask: true)) // if deletion confirmed
                        {
                            if (!_lc.DeleteList(SelectedList)) // attempt to delete row
                                Console.Beep(); // beep if error
                            if (SelectedList > 0)
                                SelectedList--;
                        }
                        break;
                    case ConsoleKey.S:
                        // turn off filter in order to do search - only one allowed at a time
                        ListFilter = 0; // clear filter if searching
                        ListSearchOrFilterEnabled = true;
                        EditSearchString = true;
                        break;
                    case ConsoleKey.F:
                        // turn off search in order to do filter - only one allowed at a time
                        EditSearchString = false;
                        ListSearchString = String.Empty;
                        ListSearchOrFilterEnabled = true;
                        // toggle filter setting
                        ListFilter++;
                        if (ListFilter > 2)
                        {
                            ListFilter = 0;
                            ListSearchOrFilterEnabled = false;
                        }
                        break;
                    case ConsoleKey.Escape:
                        if (ListSearchOrFilterEnabled)
                        {
                            ListSearchOrFilterEnabled= false;
                            ListSearchString = String.Empty;
                            ListFilter = 0;
                            newStatus = AppStatus.ShowLists;
                        }
                        else
                        {
                            newStatus = AppStatus.Escape;
                        }
                        break;
                    case ConsoleKey.A:
                        newStatus = AppStatus.ShowAboutApp;
                        break;
                    case ConsoleKey.U:
                        newStatus = AppStatus.ShowUserInfo;
                        break;
                    default:
                        Console.Beep(); // if key not appropriate, beep to indicate so to user
                        break;
                }
                break;
            case AppStatus.ShowTasks:  // for TASKS //////////////////////////////////
                if (EditSearchString)
                {
                    //Console.SetCursorPosition(0, 10 + _lc.Count);
                    //Console.Write("Search: " + TaskSearchString);
                    //Console.CursorVisible = true;
                    switch (keyInfo.KeyChar)
                    {
                        case >= 'a' and <= 'z':
                        case >= 'A' and <= 'Z':
                        case ' ':
                            TaskSearchString += keyInfo.KeyChar;
                            break;
                    }
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Delete:
                        case ConsoleKey.Backspace:
                            if (TaskSearchString.Length > 0)
                                TaskSearchString = TaskSearchString.Substring(0, TaskSearchString.Length - 1);
                            break;
                        case ConsoleKey.Enter:
                            EditSearchString = false;
                            break;
                        case ConsoleKey.Escape:
                            TaskSearchString = string.Empty;
                            EditSearchString = false;
                            TaskSearchOrFilterEnabled = false;
                            break;
                    }
                    break;
                }
                // if not editSearchString...

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (SelectedTask > 0)
                            SelectedTask--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (SelectedTask < _lc[SelectedList].Count - 1)
                            SelectedTask++;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        _lc[SelectedList].ToggleTaskDone(SelectedTask);
                        break;
                    case ConsoleKey.N:
                        _lc[SelectedList].CreateNewTask();
                        SelectedTask = _lc[SelectedList].Count - 1; // Selected is last task, the new one
                        newStatus = AppStatus.EditTasks;
                        break;
                    case ConsoleKey.E:
                        newStatus = AppStatus.EditTasks;
                        break;
                    case ConsoleKey.PageUp:
                        _lc[SelectedList].TaskReorder(SelectedTask, -1);
                        if (SelectedTask > 0)
                            SelectedTask--;
                        break;
                    case ConsoleKey.PageDown:
                        _lc[SelectedList].TaskReorder(SelectedTask, 1);
                        if (SelectedTask < _lc[SelectedList].Count - 1)
                            SelectedTask++;
                        break;
                    case ConsoleKey.Backspace:
                    case ConsoleKey.Delete:
                        if (DeleteValue(TrueForListFalseForTask: false)) // if deletion confirmed
                        {
                            _lc[SelectedList].DeleteTask(SelectedTask);
                            if (SelectedTask > 0)
                                SelectedTask--;
                        }
                        break;
                    case ConsoleKey.S:
                        // turn off filter in order to do search - only one allowed at a time
                        TaskFilter = 0; // clear filter if searching
                        TaskSearchOrFilterEnabled = true;
                        EditSearchString = true;
                        break;
                    case ConsoleKey.F:
                        // turn off search in order to do filter - only one allowed at a time
                        EditSearchString = false;
                        TaskSearchString = String.Empty;
                        TaskSearchOrFilterEnabled = true;
                        // toggle filter setting
                        TaskFilter++;
                        if (TaskFilter > 2)
                        {
                            TaskFilter = 0;
                            TaskSearchOrFilterEnabled = false;
                        }
                        break;
                    case ConsoleKey.Escape:
                        if (TaskSearchOrFilterEnabled)
                        {
                            TaskSearchOrFilterEnabled= false;
                            TaskSearchString = String.Empty;
                            TaskFilter = 0;
                            newStatus = AppStatus.ShowTasks;
                        }
                        else
                        {
                            newStatus = AppStatus.ShowLists;
                        }
                        break;
                    case ConsoleKey.A:
                        newStatus = AppStatus.ShowAboutApp;
                        break;
                    case ConsoleKey.U:
                        newStatus = AppStatus.ShowUserInfo;
                        break;
                    default:
                        Console.Beep(); // if key not appropriate, beep to indicate so to user
                        break;
                }
                break;
            case AppStatus.ShowAboutApp:
                newStatus = AppStatus.ShowLists; // any key to exit this screen
                break;
            case AppStatus.ShowSplashScreen: // input read another way in ShowSplashScreen
                newStatus = AppStatus.ShowLists;
                break;
            case AppStatus.ShowUserInfo:
                if (keyInfo.Key == ConsoleKey.E)
                    newStatus = AppStatus.EditUserInfo;
                else
                    newStatus = AppStatus.ShowLists;
                break;
            case AppStatus.Loading: // accept no input while in this status
            default:
                break; 
        }

        //Console.WriteLine($"\nKey Pressed: {keyInfo.Key}");
        //Console.WriteLine($"Character: {keyInfo.KeyChar}");
        //Console.WriteLine($"Modifiers: {keyInfo.Modifiers}");
        //Console.WriteLine("press any key to continue");
        //Console.ReadKey();
        return newStatus;
    }
    //------------------------------------
    public void ConsoleWriteCentered(int row, string text)
    {
        if (row >= ViewTotalHeight)
        {
            row = ViewTotalWidth - 1;
        }
        
        int ViewTotalCols = Console.WindowWidth;

        string trimmed = text;
        if (text.Length >= ViewTotalCols)
            trimmed = text.Substring(0, ViewTotalCols - 1); // trim to totalRows - 1 if too long
        
        int halfLengthOfText = trimmed.Length / 2;

        Console.SetCursorPosition((ViewTotalCols/2) - halfLengthOfText, row);
        Console.Write(trimmed);
    }
    //------------------------------------
    public void ShowSplash()
    {
        ConsoleWriteCentered(2, _lc.AppName);
        ConsoleWriteCentered(4, _lc.AppVersionDate);
        ConsoleWriteCentered(5, _lc.AppAuthor);
        ConsoleWriteCentered(8, _lc.UserName);
        ConsoleWriteCentered(9, _lc.EmailContact);
        ConsoleWriteCentered(10, _lc.IfFoundContact);
        ConsoleWriteCentered(13, "Press Any Key To Continue.");
    }
    //------------------------------------
    public void ShowAboutApp()
    {
        ShowSplash(); // show Splash screen 
    }
    //------------------------------------
    public void UpdateWindowDimensions()
    {
        ViewTotalWidth = Console.WindowWidth;
        ViewTotalHeight = Console.WindowHeight;
    }
    //------------------------------------
}