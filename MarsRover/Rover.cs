using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    class Rover
    {
        public Heading Orientation { get; set; }
        public (int x, int y) Position { get; set; }
        public (int x, int y) Plateau { get; set; }

        public bool IsOnPlateau => (Position.x >= 0 && Position.x <= Plateau.x) && (Position.y >= 0 && Position.y <= 5);

        public Rover((int x, int y) plateauEdge, (int x, int y) position, Heading orientation)
        {
            Position = position;
            Plateau = plateauEdge;
            Orientation = orientation;
        }

        public void ExecuteCommands(string commands)
        {
            foreach (var command in commands)
            {
                switch (command)
                {
                    case ('L'):
                        TurnLeft();
                        break;
                    case ('R'):
                        TurnRight();
                        break;
                    case ('M'):
                        if (IsOnPlateau)
                        {
                            Move();
                        }                     
                        break;
                    default:
                        throw new ArgumentException($"Invalid value: {nameof(command)}");
                }
            }
        }

        private void TurnLeft()
        {
            Orientation = (Orientation - 1) < Heading.N ? Heading.W : Orientation - 1;
        }

        private void TurnRight()
        {
            Orientation = (Orientation + 1) < Heading.W ? Heading.N : Orientation + 1;
        }

        private void Move()
        {
            var position = Position;
            if (Orientation == Heading.N)
            {
                position.y++;
            }
            else if (Orientation == Heading.E)
            {
                position.x++;
            }
            else if (Orientation == Heading.S)
            {
                position.y--;
            }
            else if (Orientation == Heading.W)
            {
                position.x--;
            }
        }

        public override string ToString()
        {
            return $"{Position.x}{Position.y}{Orientation}";
        }
    }
}
