using MiniProject_Working1.Models;
using MiniProject_Working1.Services;


class Program
{
    static void Main(string[] args)
    {

        ListContainer LC = new();

        IListUiInterface sUi = new SpectreUi(LC);

        sUi.Start();
    }
}

