using MiniProject_Working1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Services;

internal static class MockData
{
   
    public static ListContainer Load()
    {
        ListContainer demo = new ListContainer();

        demo.SetUserInfo("UserName: John Smith", "Email: johnsmith@outlook.com", "If Found Contact: 555-555-1212");

        ListObject checklist1 = demo.CreateNewList("Commissary");
        checklist1.CreateNewTask("bananas");
        checklist1.CreateNewTask("milk");
        checklist1.CreateNewTask("eggs");
        checklist1.CreateNewTask("yogurt");
        checklist1.CreateNewTask("crackers");

        ListObject checklist2 = demo.CreateNewList("");

        ListObject checklist3 = demo.CreateNewList("Lidl");
        checklist3.CreateNewTask("cereal");
        checklist3.CreateNewTask("bread");
        checklist3.CreateNewTask("meat");
        checklist3.CreateNewTask("cheese");
        checklist3.CreateNewTask("water");

        ListObject checklist4 = demo.CreateNewList("Career");
        checklist4.CreateNewTask("practice interview questions");
        checklist4.CreateNewTask("apply to more places in MRY");
        checklist4.CreateNewTask("refine resume");
        checklist4.CreateNewTask("refine portfolio website");

        ListObject checklist5 = demo.CreateNewList("Sell stuff");
        checklist5.CreateNewTask("clean and list backpack");
        checklist5.CreateNewTask("misc mil gear");
        checklist5.CreateNewTask("reflective belts");
        checklist5.CreateNewTask("ling books");

        return demo;
    }
}
