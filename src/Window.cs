///Author : Kendy Song
///Date : 01.05.2022
///Summary : Manage window interaction and program running

using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace maze_astar
{
    class Window
    {
        //Attributes and properties
        private uint _width;
        private uint _height;
        private string _title;
        private RenderWindow _window;
        private List<Drawable> _drawableItems = new List<Drawable>();
        
        public Window(uint width, uint height, string title)
        {
            _width = width;
            _height = height;
            _title = title;
            _window = new RenderWindow(new VideoMode(width, height), title);
            _window.Closed += Close;    

            //Init maze
            Random random = new Random();
            Maze maze = new Maze(Settings.GetInstance().MazeWidth, Settings.GetInstance().MazeHeight, random.Next(0, int.MaxValue));
                    for (int y = 0; y <  Settings.GetInstance().MazeHeight; y++)
                        for (int x = 0; x <  Settings.GetInstance().MazeWidth; x++)  
                            _drawableItems.Add(maze.Labyrinth[y, x].Shape);

            //Main loop
            while (_window.IsOpen)
            {
                _window.DispatchEvents();

                //Render
                _window.Clear();
                for (int i = 0; i < _drawableItems.Count(); i++) 
                    _window.Draw(_drawableItems[i]);
                _window.Display();
            }        
        }

        private void Close(object sender, EventArgs args)
        {
            _window.Close();
        }
    }
}