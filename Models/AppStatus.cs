using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject_Working1.Models;

public enum AppStatus
{
    Loading = 0,
    ShowSplashScreen,
    ShowAboutApp,
    ShowUserInfo,
    EditUserInfo,
    ShowLists,
    EditLists,
    ShowTasks,
    EditTasks,
    Escape
};
