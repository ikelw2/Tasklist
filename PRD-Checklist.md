﻿
# Checklist PRD

---

## Purpose / Problem Statement

Allow users to manage repeating and non-repeating lists, to help improve personal productivity and efficiency. For creating a shopping list based on a previous one quickly, or tracking a todo list of important tasks over several days. Going for easy but functional interface, compact but legible on a mobile device, with adjustable font size and brightness settings, eventually if not on first version.

Just the basics. A list of checklists with ordering/marking lists open & closed and tasks complete/incomplete, and poss UI adjustments. No fancy topical separators, subject tags, database similarities etc. Not planning to implement logon/security. May implement a sharing-by-text/filesave/email function if that is doable later on.

Intention to create a different final project.

---

## Language / Framework Version / App Type

C#, .NET 10, Console app (poss target MAUI/XAML/Blazor later)

---

## Design of Custom Data Types (incl any parent types)

### ListContainer class (inherited from List), may be renamed to appname

	Properties:

	• AppName               - string
	• AppVersionDate        - string
	• AppAuthor             - string
	• UserName              - string
	• EmailContact          - string
	• IfFoundContact        - string
	• IsContainerInEditMode - bool

	Methods:

	• SearchChecklist       - filter all Checklists that don't contain search string
	
	• CreateNewChecklist    - creates new Checklist, then enters edit/delete mode

	◦ ToggleChecklistIsOpen
	• ToggleEditMode        - enters edit/delete mode
	◦ ReorderChecklist      - shift up/down in ListContainer order; default order by IsOpen, LastAccessedDate
	• DeleteChecklist       - after confirmation, removes Checklist entirely
	
	• ShowUserContactInfo   - display UserName, EmailContact, IfFoundContact
	• ShowAboutApp
	
	UI Notes: 

	Toggling Checklist status, altering display order of Checklists, and searching 
	are meant to be the easiest processes to complete on the ListContainer page. 
	
	Editing Checklist name, deleting Checklists, and creating new Checklists are 
	considered to be more multi-step processes. No separators allowed in the 
	ListContainer, but closed Checklists appear in darker color. Closed Checklists 
	are not disabled, and tasks can still be edited or marked complete. The 
	Checklist is just marked as closed for UI purposes.

	Edit/Delete mode is a UI-centric status that will allow the user to change 
	Checklist/Task names, reorder elements, and delete elements without having to 
	go into edit mode for each change, saving user extra steps.

### Checklist class (also inherited from List)
	
	Properties:

	• ChecklistName         - string
	• IsOpen (status)       - bool
	• IsChecklistInEditMode - bool
	• LastAccessedDate      - DateTime
	• LastModifiedDate      - DateTime
	• ID                    - int (for internal mgmt)
		
	Methods:

	• BringChecklistUp      - shift up in ListContainer order; default order by IsOpen, LastAccessedDate
	• BringChecklistDown    - shift down in ListContainer order
	• ToggleListIsOpen      - IsCompleted unchanged for Tasks, IsOpen toggles true/false
	
	• EditChecklistName     - applies name changes
	
	• SearchTasks           - hides all Tasks that don't contain search text string
	• ScrollTasks           - scrolls Checklist up or down to see others Tasks
	• FilterTasks           - filter by completion status (Off, IsOpen, !IsOpen)
	• ShowAllTasks          - clears all active searching/filtering
	◦ ToggleTaskCompletion  
	
	• CreateNewTask         - creates new Task, enters edit/delete mode
	• ToggleEditMode        - enters edit/delete mode
	• BringTaskHigher       - shift up in order
	• BringTaskLower        - shift down in order; no default order
	• DeleteTask            - removes Task from Checklist permanently
	• AddSeparatorTask      - creates UI-separator Task with optional name
	
	UI Notes: 

	Toggling Task completion, searching and scrolling through Tasks are
	meant to be the easiest processes to complete on each Checklist page.
	Editing Task names, changing their order, and creating new Tasks are 
	more multi-step processes. Separators are allowed in the Checklists, 
	with optional name.
	
### Task class (used as Tasks and Separators in Checklists)

	Properties:

	• Name                  - string
	• Done                  - bool    /// renamed from isComplete to Done for shorter JSON output
	//• IsSeparator           - bool    // scratch this, keep is SUPER simple, a task without name is a separator
	
	Methods:

	• ToggleDone            - Done change to true/false
	
	• RenameTask            - applies name changes

	UI Notes:

	Tasks are the bottom leaf element of this, they will not spawn
	additional lists/tasks.

---

## External Resources Required

No external resources are required at this stage.  

Some difficulty expected in implementing simple font-size change and screen-darkening, although I may just skip that step.

Once basic functionality is fulfilled and tested, I may attempt to incorporate serializing/exporting of checklists/items for sharing purposes, which will require additional research.

---

## Planned Development Time in Hours

8-10 hours anticipated

---

## Pseudocode Implementation

1. Create Project
2. Create Folders and Files
3. Outline App Structure as Comments in Source code files
4. Divide functionality up into responsible sourcecode files

---

## Mock Data source

- Create mock checklist/tasks to test functionality if I develop the UI before the ability to add/delete tasks/checklists
- Serializable/exported data later on, maybe plain text is best for simplicity

---

## Additional Notes:

---

<!-- Extra Icons:   🪙 working on something     🚩 needs work      ✅ completed     🔄️ for later   -->

---

Please add any comments here if desired: