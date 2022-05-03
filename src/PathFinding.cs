///Author : Kendy Song
///Date : 03.05.2022
///Summary : Note case distance and find a path A => B

using System.Collections.Generic;

namespace maze_astar
{
    static class PathFinding
    {
        public static List<Case> FindPath(Case[,] map, Case start, Case target)
        {
            Case currentCase = start;
            List<Case> path = new List<Case>();
            List<Case> neighbor = new List<Case>();

            //Note case far from the target
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

            //Problem is on the neighbor and revisted more than 1 time case
            for(int a = 0; a < 50; a++)
            {
                //Get neighbor of current case
                neighbor = GetNeighbor(map, currentCase);
                
                currentCase = neighbor[0];
                for (int i = 1; i < neighbor.Count; i++)
                {
                    if (currentCase.DistTarget > neighbor[i].DistTarget)
                    {
                        currentCase = neighbor[i];
                    }
                }
                path.Add(currentCase);
            }
            
            return path;
        }

        private static List<Case> GetNeighbor(Case[,] map, Case currentCase)
        {
            List<Case> neighbor = new List<Case>();

            if(currentCase.Position.Y - 1 > 0 && map[currentCase.Position.Y - 1, currentCase.Position.X].Type == State.Empty)
            {                    
                neighbor.Add(map[currentCase.Position.Y - 1, currentCase.Position.X]);
            }

            if(currentCase.Position.Y + 1 < map.GetLength(0) - 1 && map[currentCase.Position.Y + 1, currentCase.Position.X].Type == State.Empty)
            {
                neighbor.Add(map[currentCase.Position.Y + 1, currentCase.Position.X]);
            }

            if(currentCase.Position.X - 1 > 0 && map[currentCase.Position.Y, currentCase.Position.X - 1].Type == State.Empty)
            {
                neighbor.Add(map[currentCase.Position.Y, currentCase.Position.X - 1]);
            }

            if(currentCase.Position.X + 1 < map.GetLength(1) - 1 && map[currentCase.Position.Y, currentCase.Position.X + 1].Type == State.Empty)
            {
                neighbor.Add(map[currentCase.Position.Y, currentCase.Position.X + 1]);
            }

            return neighbor;
        }
    }
}