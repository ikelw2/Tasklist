using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Models;

public class TaskObject // use this object containing two elements
{
    public string Task { get; set; } = string.Empty;
    public bool Done { get; set; } = false; // Done replaced isComplete

    public TaskObject()
    {
        Task = string.Empty;
        Done = false;       // if done is true and string is empty, this becomes a separator
    }

    //------------------------------------
    public void ToggleDone()
    {
        Done = !Done;
    }
    //------------------------------------
    public void RenameTask(string newName)
    {
        Task = newName;
    }
    //------------------------------------
}
