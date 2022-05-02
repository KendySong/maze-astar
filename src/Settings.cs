///Author : Kendy Song
///Date : 01.05.2022
///Summary : Load data that contain program settings

namespace maze_astar
{
    class Settings
    {
        //Attributes and properties
        private static Settings _settings;
        public uint MazeWidth { get; set; }
        public uint MazeHeight { get; set; }

        public string Title { get; set; }
        public uint WindowWidth { get; set; }
        public uint WindowHeight { get; set; }

        private Settings()
        {
            //Windiw size
            Title = "Maze A*";
            WindowWidth = 1282;
            WindowHeight = 722;

            //Maze size => Impair number
            MazeWidth = 91;
            MazeHeight = 47;
        }

        public static Settings GetInstance()
        {
            if(_settings == null)           
                _settings = new Settings();
            return _settings;
        }
    }
}