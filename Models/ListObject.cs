using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Models;


public class ListObject : List<TaskObject>
{
    public string Subject { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime LastAccess { get; set; } = DateTime.Now;
    public bool IsTaskSearchFilterActive { get; set; } = false;

    public ListObject(string subject, bool isActive = true)
    {
        Subject = subject;
        IsActive = isActive;
        LastAccess = DateTime.Now;
    }
    public ListObject()
    {
        Subject = string.Empty;
        IsActive = true;
        LastAccess = DateTime.Now;
    }
    //------------------------------------
    public void ToggleListIsActive()
    {
        IsActive = !IsActive;
        LastAccess = DateTime.Now;
    }
    //------------------------------------
    public void ActivateList()
    {
        IsActive = true;
        LastAccess = DateTime.Now;
    }
    //------------------------------------
    public void DeactivateList()
    {
        IsActive = false;
        LastAccess = DateTime.Now;
    }
    //------------------------------------
    public void RenameList(string newName)
    {
        Subject = newName;
        LastAccess = DateTime.Now;
    }
    //------------------------------------
    public void UpdateLastAccess ()
    {
        LastAccess = DateTime.Now;
    }
    //------------------------------------
    public List<TaskObject> TaskSearch(string searchText)
    {
        LastAccess = DateTime.Now;
        if (string.IsNullOrWhiteSpace(searchText))
            return new List<TaskObject>(this);
        return this.FindAll(taskObj =>
            taskObj.Task.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
    //------------------------------------
    public List<TaskObject> ShowAllTasks()
    {
        return new List<TaskObject>(this);
    }
    //------------------------------------
    public List<TaskObject> TaskFilter(int filterType)
    {
        LastAccess = DateTime.Now;
        return filterType switch
        {
            0 => new List<TaskObject>(this),       // show all tasks
            1 => this.FindAll(taskObj => !taskObj.Done), // incomplete tasks
            2 => this.FindAll(taskObj => taskObj.Done),  // complete tasks
            _ => new List<TaskObject>(this)        // default, show all
        };
    }
    //------------------------------------
    public void ToggleTaskDone(int index)
    {
        if (index >= 0 && index < this.Count)
        {
            this[index].ToggleDone();
            LastAccess = DateTime.Now;
        }
    }
    //------------------------------------
    public int CreateNewTask(string task = "", int index = -1)
    {
        if (index < 0 || index >= this.Count) // if no index provided, set insertion point at last index
            index = this.Count - 1;

        var newTaskObj = new TaskObject
        {
            Task = task,
            Done = false
        };

        int newIndex = index + 1;

        this.Insert(index + 1, newTaskObj);
        IsActive = true;
        LastAccess = DateTime.Now;
        return newIndex;
    }
    //------------------------------------
    public void TaskReorder(int index, int direction)
    {
        // if index out of range
        if (index < 0 || index >= this.Count)
            return;

        // add direction (+1 or -1) to index to get newIndex
        int newIndex = index + direction;

        // if newIndex is not too small or too large, out of range
        if (newIndex >= 0 && newIndex < this.Count)
        {
            // move item at index to newIndex, and update LastAccess
            var task = this[index];
            this.RemoveAt(index);
            this.Insert(newIndex, task);
            LastAccess = DateTime.Now;
        }
    }
    //------------------------------------
    //public void MoveTaskUp(int index)
    //{
    //    if (index > 0 && index < this.Count)
    //    {
    //        var task = this[index];
    //        this.RemoveAt(index);
    //        this.Insert(index - 1, task);
    //        LastAccess = DateTime.Now;
    //    }
    //}
    ////------------------------------------
    //public void MoveTaskDown(int index)
    //{
    //    if (index >= 0 && index < this.Count - 1)
    //    {
    //        var task = this[index];
    //        this.RemoveAt(index);
    //        this.Insert(index + 1, task);
    //        LastAccess = DateTime.Now;
    //    }
    //}
    //------------------------------------
    public void DeleteTask(int index)
    {
        if (index >= 0 && index < this.Count)
        {
            this.RemoveAt(index);
            LastAccess = DateTime.Now;
        }
    }
    //------------------------------------
    
    
    
    //------------------------------------
    //------------------------------------
    //------------------------------------
    public void utilPrintList()
    {
        foreach (TaskObject taskObj in this)
        {
            string done = taskObj.Done ? "o" : "x";
            Console.Write($"{done} {taskObj.Task}");
        }
    }
    //------------------------------------
}
