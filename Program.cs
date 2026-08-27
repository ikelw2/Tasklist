using MiniProject_Working1.Models;
using MiniProject_Working1.Services;


class Program
{
    static void Main(string[] args)
    {

        ListContainer LC = new();

        IListUiInterface Ui = new ConsoleUi(LC);
        //IListUiInterface Ui = new SpectreUi(LC);

        Ui.Start();
    }
}

