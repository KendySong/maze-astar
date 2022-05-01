///Author : Kendy Song
///Date : 01.05.2022
///Summary : Generate the maze data

using System;
using SFML.System;
using SFML.Graphics;

namespace maze_astar
{
    class Maze
    {
        //Attriubtes and properties
        public Case[,] Labyrinth;

        public Maze(uint width, uint height)
        {
            Labyrinth = new Case[height, width];

            //Init maze data
            int caseIndex = 0;
            float caseSizeX = Settings.GetInstance().WindowWidth / Settings.GetInstance().MazeWidth;
            float caseSizeY = Settings.GetInstance().WindowHeight / Settings.GetInstance().MazeHeight;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    RectangleShape wall = new RectangleShape(new Vector2f(caseSizeX, caseSizeY));

                    wall.OutlineColor = Color.Black;
                    wall.OutlineThickness = 1;

                    wall.Position = new Vector2f(x * caseSizeX + 1, y * caseSizeY + 2);
                    wall.FillColor = Color.White;
               
                    //Create new case of the labyrinth and set wall in border
                    Labyrinth[y, x] = new Case(wall, new Vector2f(x, y), State.Empty, caseIndex);
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
                        if (x % 2 == 0)
                            Labyrinth[y, x].SetWall();  
                }
                //Set line of wall
                else  
                {
                    for (int x = 1; x < width - 1; x++)
                        Labyrinth[y, x].SetWall();
                }              
            }


        }
    }
}