using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Models;


internal class ListContainer : List<ListObject>
{
    public string AppName { get; } = "Tasklist";
    public string AppVersionDate { get; } = "Version 0.2  Build 2026.08.25";
    public string AppAuthor { get; } = "By Michael Wood";

    public string UserName { get; set; }
    public string EmailContact { get; set; }
    public string IfFoundContact { get; set; }

    public ListContainer()
    {
        UserName = string.Empty;
        EmailContact = string.Empty;
        IfFoundContact = string.Empty;
    }

    //------------------------------------
    public List<ListObject> ListSearch(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return new List<ListObject>(this);

        return this.FindAll(listObj =>
            listObj.Subject.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
    //------------------------------------
    public List<ListObject> ListShowAll()
    {
        return new List<ListObject>(this);
    }
    //------------------------------------
    public List<ListObject> ListFilter(int filterType)
    {
        return filterType switch
        {
            0 => new List<ListObject>(this),                 // show all lists
            1 => this.FindAll(taskObj => !taskObj.IsActive), // inactive lists
            2 => this.FindAll(taskObj => taskObj.IsActive),  // active lists
            _ => new List<ListObject>(this)                  // default, show all
        };
    }
    //------------------------------------
    public void ListReorder(int index, int direction)
    {
        if (index < 0 || index >= this.Count)
            return;

        int newIndex = index + direction;

        if (newIndex >= 0 && newIndex < this.Count)
        {
            var ListObject = this[index];
            this.RemoveAt(index);
            this.Insert(newIndex, ListObject);
            ListObject.LastAccess = DateTime.Now;
        }
    }
    //------------------------------------
    public void ToggleListActive(int index)
    {
        if (index >= 0 && index < this.Count)
        {
            this[index].ToggleListIsActive();
        }
    }
    //------------------------------------
    public ListObject CreateNewList(string Subject = "")
    {
        var newList = new ListObject
        {
            Subject = Subject,
            IsActive = true,
            LastAccess = DateTime.Now
        };

        this.Add(newList);
        return newList;
    }
    //------------------------------------
    public bool DeleteList(int index)
    {
        if (index >= 0 && index < this.Count)
        {
            this.RemoveAt(index);
            return true;
        }
        return false;
    }
    //------------------------------------
    public string ShowUserInfo()
    {
        var info = new StringBuilder();
        info.AppendLine("User Contact Information:");
        info.AppendLine($"User Name: {UserName}");
        info.AppendLine($"Email Contact: {EmailContact}");
        info.AppendLine($"If Found Contact: {IfFoundContact}");

        Console.WriteLine(info.ToString());
        Console.ReadLine(); Console.Clear();
        return info.ToString();
    }
    //------------------------------------
    public string EditUserInfo()
    {
        // get contact info from user
        Console.WriteLine("Enter User Contact Information:");
        string input = AnsiConsole.Prompt(
            new TextPrompt<string>("User Name: ")
            );
        UserName = input;
        var emailPrompt = new TextPrompt<string>("Email Address: ")
        .Validate(input =>
            input.Contains("@") && input.Contains("."),
            "[red]Please enter a valid email address[/]");
        EmailContact = AnsiConsole.Prompt(emailPrompt);
        input = AnsiConsole.Prompt(
            new TextPrompt<string>("If Found Contact: ")
            );
        IfFoundContact = input;
        
        // show changes
        var info = new StringBuilder();
        info.AppendLine("\nEdited User Contact Information:");
        info.AppendLine($"User Name: {UserName}");
        info.AppendLine($"Email Contact: {EmailContact}");
        info.AppendLine($"If Found Contact: {IfFoundContact}");

        Console.WriteLine(info.ToString());
        Console.ReadLine(); Console.Clear();
        return info.ToString();
    }
    //------------------------------------
    public string ShowAboutApp()
    {
        var about = new StringBuilder();
        about.AppendLine($"App Name: {AppName}");
        about.AppendLine($"Version Date: {AppVersionDate}");
        about.AppendLine($"Author: {AppAuthor}");
        about.AppendLine();
        about.AppendLine("Purpose: Manage your checklists and todos to");
        about.AppendLine("improve personal productivity and efficiency.");

        Console.WriteLine(about.ToString());
        Console.ReadLine(); Console.Clear();
        return about.ToString();
    }
    //------------------------------------
    public void SortChecklistsByDefault()
    {
        this.Sort((a, b) =>
        {
            // first sort by IsActive (true before false)
            int openCompare = b.IsActive.CompareTo(a.IsActive);
            if (openCompare != 0)
                return openCompare;

            // then sort by LastAccess (most recent first)
            return b.LastAccess.CompareTo(a.LastAccess);
        });
    }
    //------------------------------------
}
