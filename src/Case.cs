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
        public Vector2f Position { get; set; }
        public State Type { get; set; }
        public RectangleShape Shape { get; set; }
        public int Index { get; set; }

        public Case(RectangleShape shape, Vector2f position, State type, int index)
        {
            Shape = shape;
            Position = position;
            Type = type;
            Index = index;
        }

        public void SetWall()
        {
            Type = State.Wall;
            Shape.FillColor = Color.Black;
        }
    }
}
