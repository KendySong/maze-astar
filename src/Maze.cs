///Author : Kendy Song
///Date : 01.05.2022
///Summary : Generate the maze data

using SFML.System;
using SFML.Graphics;

namespace maze_astar
{
    class Maze
    {
        //Attriubtes and properties    
        public Case[,] Labyrinth;
        private Random _random;
        private bool _sameIndex;
        private List<Case> _destructiveWall = new List<Case>();

        public Maze(uint width, uint height, int seed)
        {
            Labyrinth = new Case[height, width];
            _random = new Random(seed);
            _sameIndex = false;

            //Init maze data
            int caseIndex = 0;
            float caseSizeX = Settings.GetInstance().WindowWidth / Settings.GetInstance().MazeWidth;
            float caseSizeY = Settings.GetInstance().WindowHeight / Settings.GetInstance().MazeHeight;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    RectangleShape wall = new RectangleShape(new Vector2f(caseSizeX, caseSizeY));

                    wall.Position = new Vector2f(x * caseSizeX, y * caseSizeY);
                    wall.FillColor = Color.White;
               
                    //Create new case of the labyrinth and set wall in border
                    Labyrinth[y, x] = new Case(wall, new Vector2i(x, y), State.Empty);
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        Labyrinth[y, x].SetWall();
                }
            }  
           
            //Init grid
            for (int y = 1; y < height - 1; y++)
            {
                //Set wall 1/2 case
                if (y % 2 != 0)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        if (x % 2 == 0)
                        {
                            Labyrinth[y, x].SetWall();
                            _destructiveWall.Add(Labyrinth[y, x]);
                        }
                        Labyrinth[y, x].Index = caseIndex;
                        caseIndex++;
                    }
                        
                }
                //Set line of wall
                else  
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        Labyrinth[y, x].SetWall();
                        
                        if(x % 2 != 0)
                        {
                            _destructiveWall.Add(Labyrinth[y, x]);
                            Labyrinth[y, x].Index = caseIndex;
                            caseIndex++;
                        }
                    }
                }
            }

            //Set input and output
            Labyrinth[1, 0].SetEmpty();
            Labyrinth[height - 2, width - 1].SetEmpty();

            //Generate maze
            while (!_sameIndex)
            {
                //Check case index
                _sameIndex = true;
                int tempIndex = Labyrinth[1, 1].Index;
                for (int y = 1; y < height - 1; y++)
                {  
                    for (int x = 1; x < width - 1; x++)
                    {                     
                        if (Labyrinth[y, x].Index != tempIndex && Labyrinth[y, x].Type == State.Empty)
                        {
                            //Check with undestroyable wall
                            if(!(y % 2 == 0 && x % 2 == 0))
                                _sameIndex = false;
                        }
                    }
                }

                //choice a wall to destroy
                int randomIndex = _random.Next(0, _destructiveWall.Count);
            
                //Full line
                if(_destructiveWall[randomIndex].Position.Y % 2 == 0)
                {   
                    //Check wall (we chose topID for default indexation)
                    int topID = Labyrinth[_destructiveWall[randomIndex].Position.Y - 1, _destructiveWall[randomIndex].Position.X].Index;
                    int botID = Labyrinth[_destructiveWall[randomIndex].Position.Y + 1, _destructiveWall[randomIndex].Position.X].Index;
                    if(topID != botID)
                    {
                        //Manage wall that we destroy
                        _destructiveWall[randomIndex].SetEmpty();
                        _destructiveWall[randomIndex].Index = topID;
                        _destructiveWall.RemoveAt(randomIndex);

                        //Fill botID with topID
                        for (int y = 1; y < height - 1; y++)
                            for (int x = 1; x < width - 1; x++)
                                if (Labyrinth[y, x].Index == botID)
                                    Labyrinth[y, x].Index = topID;
                    }
                }
                //1/2 line
                else
                {
                    //Check wall (we chose leftID for default indexation)
                    int leftID = Labyrinth[_destructiveWall[randomIndex].Position.Y, _destructiveWall[randomIndex].Position.X - 1].Index;
                    int rightID = Labyrinth[_destructiveWall[randomIndex].Position.Y, _destructiveWall[randomIndex].Position.X + 1].Index;

                    if(leftID != rightID)
                    {
                        //Manage wall that we destroy
                        _destructiveWall[randomIndex].SetEmpty();
                        _destructiveWall[randomIndex].Index = leftID;
                        _destructiveWall.RemoveAt(randomIndex);

                        //Fill botID with topID
                        for (int y = 1; y < height - 1; y++)
                            for (int x = 1; x < width - 1; x++)
                                if (Labyrinth[y, x].Index == rightID)
                                    Labyrinth[y, x].Index = leftID;
                    }   
                }
            }           
        }
    }
}