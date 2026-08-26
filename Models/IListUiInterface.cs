using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Models;

public interface IListUiInterface // will hopefully make transition to MAUI or other target easier
{
    public List<TaskObject> SelectedList { get; set; }
    public TaskObject SelectedTask { get; set; }
    public ViewStatus ViewMode { get; set; }

    public void ShowSplash(string text);
    public void ShowHeader(string text);
    public void ShowLists(List<ListObject> lists);
    public void ShowItems(List<TaskObject> tasks);
    public void ShowAboutApp();
    public void ShowUserInfo();
    public void EditUserInfo();
    public ListUiCommand GetUserInput();

}
public enum ViewStatus
{
    ShowSplashScreen,
    ShowAboutApp,
    ShowUserInfo,
    EditUserInfo,
    ShowLists,
    EditLists,
    ShowItems,
    EditItems
};
public enum ListUiCommand
{
    NoCommand = 0, 
    ListArrowUp,
    ListArrowDown,
    ListOpen,
    ListActiveToggle,
    ListCreateNew,
    ListEditSubject,
    ListMoveUp,
    ListMoveDown,
    ListDelete,

    ListSearch,
    ListShowAll,
    ListFilter,


    ShowAppInfo,
    ShowUserInfo,
    SetUserInfo,

    TaskArrowUp,
    TaskArrowDown,
    TaskDoneToggle,
    TaskCreateNew,
    TaskEditName,
    TaskMoveUp,
    TaskMoveDown,
    TaskDelete
};



//AnsiConsole.Cursor.Show();
//Console.SetCursorPosition(0, (maxRowReached* 2) + 3);
//Console.SetCursorPosition(branchX, screenRow + 1);
//AnsiConsole.Markup("[yellow]\\[/]")
//AnsiConsole.Cursor.Hide();
//int maxRowReached = 0;