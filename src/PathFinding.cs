///Author : Kendy Song
///Date : 03.05.2022
///Summary : Note case distance and find a path A => B

using System;
using System.Collections.Generic;

namespace maze_astar
{
    static class PathFinding
    {
        static List<Case> path = new List<Case>();
        static List<Case> openedCase = new List<Case>();

        public static List<Case> FindPath(Case[,] map, Case start, Case target)
        {
            bool pathFind = false;
            Case currentCase = start;     
            List<Case> neighbor = new List<Case>();
            
            //Note case far from the target
            path.Add(currentCase);
            for (int y = 1; y < map.GetLength(0) - 1; y++)
            {
                for (int x = 1; x < map.GetLength(1) - 1; x++)
                {
                    if (map[y, x].Type == State.Empty)
                    {
                        int distX = target.Position.X - map[y, x].Position.X;
                        int distY = target.Position.Y - map[y, x].Position.Y;
                        map[y, x].DistTarget = Math.Sqrt(Math.Pow(Math.Abs(distX), 2) + Math.Pow(Math.Abs(distY), 2));
                    }
                }
            }

            //Search path
            while (!pathFind)
            {
                //Get neighbor of current case
                neighbor = GetNeighbor(map, currentCase);
                
                //Blocked
                if (neighbor.Count == 0)
                {                   
                    //Get the nearest opened case
                    currentCase = openedCase[0];
                    for (int i = 0; i < openedCase.Count; i++)                  
                        if (currentCase.DistTarget > openedCase[i].DistTarget && !path.Contains(openedCase[i]))                      
                            currentCase = openedCase[i];                          
                }
                //Choice of path or 1 choice
                else
                {                    
                    //Get nearest neighbor
                    currentCase = neighbor[0];
                    for (int i = 1; i < neighbor.Count; i++)                    
                        if (currentCase.DistTarget > neighbor[i].DistTarget)                        
                            currentCase = neighbor[i];       
                    
                    openedCase.Remove(currentCase);
                }   
          
                path.Add(currentCase);    

                //Check if the path is found
                if (currentCase == target)               
                    pathFind = true;                  
            }
            
            return path;
        }

        private static List<Case> GetNeighbor(Case[,] map, Case currentCase)
        {
            List<Case> neighbor = new List<Case>();

            //Add 4 directions neighbor if it's possible
            if(currentCase.Position.Y - 1 > 0 && map[currentCase.Position.Y - 1, currentCase.Position.X].Type == State.Empty && !path.Contains(map[currentCase.Position.Y - 1, currentCase.Position.X]))
            {                    
                neighbor.Add(map[currentCase.Position.Y - 1, currentCase.Position.X]);
                openedCase.Add(map[currentCase.Position.Y - 1, currentCase.Position.X]);
            }

            if(currentCase.Position.Y + 1 < map.GetLength(0) - 1 && map[currentCase.Position.Y + 1, currentCase.Position.X].Type == State.Empty && !path.Contains(map[currentCase.Position.Y + 1, currentCase.Position.X]))
            {
                neighbor.Add(map[currentCase.Position.Y + 1, currentCase.Position.X]);
                openedCase.Add(map[currentCase.Position.Y + 1, currentCase.Position.X]);
            }

            if(currentCase.Position.X - 1 > 0 && map[currentCase.Position.Y, currentCase.Position.X - 1].Type == State.Empty && !path.Contains(map[currentCase.Position.Y, currentCase.Position.X - 1]))
            {
                neighbor.Add(map[currentCase.Position.Y, currentCase.Position.X - 1]);
                openedCase.Add(map[currentCase.Position.Y, currentCase.Position.X - 1]);
            }

            if(currentCase.Position.X + 1 < map.GetLength(1) - 1 && map[currentCase.Position.Y, currentCase.Position.X + 1].Type == State.Empty && !path.Contains(map[currentCase.Position.Y, currentCase.Position.X + 1]))
            {
                neighbor.Add(map[currentCase.Position.Y, currentCase.Position.X + 1]);
                openedCase.Add(map[currentCase.Position.Y, currentCase.Position.X + 1]);
            }

            return neighbor;
        }
    }
}