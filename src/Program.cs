///Author : Kendy Song
///Date : 01.05.2022
///Summary : Launch program

namespace maze_astar
{
    class Program
    {
        public static void Main()
        {
            Window window = new Window(Settings.GetInstance().WindowWidth, Settings.GetInstance().WindowHeight, Settings.GetInstance().Title);
        }
    }
}