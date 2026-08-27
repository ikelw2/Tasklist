using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Models;

public interface IListUiInterface // will hopefully make transition to MAUI or other target easier
{
    //public List<TaskObject> SelectedList { get; set; } // may not be required, but helped me
    //public TaskObject SelectedTask { get; set; } // may not be required, but helped me
    //public ViewStatus Status { get; set; } // may not be required, but helped me

    public void Start();
    public void ShowSplash();
    public void ShowHeader();
    public void ShowLists(List<ListObject> lists);
    public void ShowItems(List<TaskObject> tasks);
    public void ShowAboutApp();
    public void ShowUserInfo();
    public void EditUserInfo();
    public ListUiCommand GetUserInput();

}

public enum ListUiCommand
{
    NoCommand = 0, 
    Escape,

    ArrowUp,
    ArrowDown,
    Open,
    Toggle,
    CreateNew,
    EditName,
    MoveUp,
    MoveDown,
    Delete,
    Search,
    Filter,
    ShowAll,


    ShowAppInfo,
    ShowUserInfo,
    EditUserInfo
};
