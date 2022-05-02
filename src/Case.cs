///Author : Kendy Song
///Date : 01.05.2022
///Summary : State of a wall and position

using SFML.System;
using SFML.Graphics;

namespace maze_astar
{
    class Case
    {
        //Attributes and properties
        public Vector2i Position { get; set; }
        public State Type { get; set; }
        public RectangleShape Shape { get; set; }
        public int Index { get; set; }

        public Case(RectangleShape shape, Vector2i position, State type)
        {
            Shape = shape;
            Position = position;
            Type = type;
            Index = 0;
        }

        public void SetWall()
        {
            Type = State.Wall;
            Shape.FillColor = Color.Black;
        }

        public void SetEmpty()
        {
            Type = State.Empty;
            Shape.FillColor = Color.White;
        }
    }
}
