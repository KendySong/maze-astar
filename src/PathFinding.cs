///Author : Kendy Song
///Date : 03.05.2022
///Summary : Custom implementation of A* algorithm

using System;
using System.Collections.Generic;

namespace maze_astar
{
    static class PathFinding
    {
        private static List<Case> _path = new List<Case>();
        private static List<Case> _openedCase = new List<Case>();

        public static List<Case> FindPath(Case[,] map, Case start, Case target)
        {
            bool pathFind = false;
            Case currentCase = start;     
            List<Case> neighbor = new List<Case>();
            _path.Add(currentCase);

            //Search path
            while (!pathFind)
            {
                //Get neighbor of current case
                neighbor = GetNeighbor(map, currentCase);

                //Calculate distance between neighbor and target case
                for (int i = 0; i < neighbor.Count; i++)
                {
                    int distX = target.Position.X - neighbor[i].Position.X;
                    int distY = target.Position.Y - neighbor[i].Position.Y;
                    neighbor[i].DistTarget = Math.Sqrt(Math.Pow(Math.Abs(distX), 2) + Math.Pow(Math.Abs(distY), 2));
                }
                
                //Blocked
                if (neighbor.Count == 0)
                {
                    //Get the nearest opened case
                    currentCase = _openedCase[0];
                    for (int i = 0; i < _openedCase.Count; i++)                  
                        if (currentCase.DistTarget > _openedCase[i].DistTarget && !_path.Contains(_openedCase[i]))                      
                            currentCase = _openedCase[i];
                }
                //Choice of path or 1 choice
                else
                {
                    //Get nearest neighbor
                    currentCase = neighbor[0];
                    for (int i = 1; i < neighbor.Count; i++)                    
                        if (currentCase.DistTarget > neighbor[i].DistTarget)                        
                            currentCase = neighbor[i];       
                    
                    _openedCase.Remove(currentCase);
                }   
          
                _path.Add(currentCase);    

                //Check if the path is found
                if (currentCase == target)               
                    pathFind = true;                  
            }
            
            return _path;
        }

        private static List<Case> GetNeighbor(Case[,] map, Case currentCase)
        {
            List<Case> neighbor = new List<Case>();

            //Add 4 directions neighbor if it's possible
            if(currentCase.Position.Y - 1 > 0 && map[currentCase.Position.Y - 1, currentCase.Position.X].Type == State.Empty && !_path.Contains(map[currentCase.Position.Y - 1, currentCase.Position.X]))
            {                    
                neighbor.Add(map[currentCase.Position.Y - 1, currentCase.Position.X]);
                _openedCase.Add(map[currentCase.Position.Y - 1, currentCase.Position.X]);
            }

            if(currentCase.Position.Y + 1 < map.GetLength(0) - 1 && map[currentCase.Position.Y + 1, currentCase.Position.X].Type == State.Empty && !_path.Contains(map[currentCase.Position.Y + 1, currentCase.Position.X]))
            {
                neighbor.Add(map[currentCase.Position.Y + 1, currentCase.Position.X]);
                _openedCase.Add(map[currentCase.Position.Y + 1, currentCase.Position.X]);
            }

            if(currentCase.Position.X - 1 > 0 && map[currentCase.Position.Y, currentCase.Position.X - 1].Type == State.Empty && !_path.Contains(map[currentCase.Position.Y, currentCase.Position.X - 1]))
            {
                neighbor.Add(map[currentCase.Position.Y, currentCase.Position.X - 1]);
                _openedCase.Add(map[currentCase.Position.Y, currentCase.Position.X - 1]);
            }

            if(currentCase.Position.X + 1 < map.GetLength(1) - 1 && map[currentCase.Position.Y, currentCase.Position.X + 1].Type == State.Empty && !_path.Contains(map[currentCase.Position.Y, currentCase.Position.X + 1]))
            {
                neighbor.Add(map[currentCase.Position.Y, currentCase.Position.X + 1]);
                _openedCase.Add(map[currentCase.Position.Y, currentCase.Position.X + 1]);
            }

            return neighbor;
        }
    }
}