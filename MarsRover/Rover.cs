using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover
    {
        public Heading Orientation { get; set; }
        public (int x, int y) Position { get; set; }
        public (int x, int y) Plateau { get; set; }

        public Rover(params string[] args)
        {
            CreatePlateauEdge(args[0]);
            PlaceRoverOnPlateau(args[1]);
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
                        Move();
                        break;
                    default:
                        throw new ArgumentException($"Invalid value: {nameof(command)}");
                }
            }
        }

        private void PlaceRoverOnPlateau(string position)
        {
            var letters = position.ToCharArray();
            Position = (int.Parse(letters[0].ToString()), int.Parse(letters[1].ToString()));
            Orientation = (Heading) Enum.Parse(typeof(Heading), letters[2].ToString());
        }

        private void CreatePlateauEdge(string coordinates)
        {
            var edge = coordinates;
            if (edge.Length != 2)
            {
                throw new ArgumentException($"Upper-right coordinates {edge} are incorrect.");
            }
            var letters = edge.ToCharArray();
            if (letters.Any(letter => !char.IsDigit(letter)))
            {
                throw new ArgumentException($"Upper-right coordinates {edge} must be numbers.");
            }
            if (letters[0] != letters[1])
            {
                throw new ArgumentException($"Upper-right coordinates {edge} must be the same.");
            }
            Plateau = (int.Parse(letters[0].ToString()), int.Parse(letters[1].ToString()));
        }

        private void TurnLeft()
        {
            Orientation = (Orientation - 1) < Heading.N ? Heading.W : Orientation - 1;
        }

        private void TurnRight()
        {
            Orientation = (Orientation + 1) > Heading.W ? Heading.N : Orientation + 1;
        }

        private void Move()
        {
            var position = Position;
            if (Orientation == Heading.N)
            {
                if (position.y < Plateau.y)
                {
                    position.y++;
                }                
                Position = position;
            }
            else if (Orientation == Heading.E)
            {
                if (position.x < Plateau.x)
                {
                    position.x++; 
                }
                Position = position;
            }
            else if (Orientation == Heading.S)
            {
                if (position.y > 0)
                {
                    position.y--; 
                }
                Position = position;
            }
            else if (Orientation == Heading.W)
            {
                if (position.x > 0)
                {
                    position.x--; 
                }
                Position = position;
            }
        }

        public void PrintLocation()
        {
            Console.WriteLine($"{Position.x}{Position.y}{Orientation}");
        }
    }
}
