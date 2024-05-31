using System;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class MarsRover
{
    private int x; 
    private int y; 
    private Direction direction;

    private const int gridSize = 100;
    private const int maxCommands = 5;

    public MarsRover()
    {
        x = 0;
        y = 0;
        direction = Direction.South; 
    }

    public void ExecuteCommands(string[] commands)
    {
        foreach (string command in commands)
        {
            if (!IsValidCommand(command))
            {
                Console.WriteLine("Invalid command: " + command);
                return;
            }

            switch (command)
            {
                case "Left":
                    TurnLeft();
                    break;
                case "Right":
                    TurnRight();
                    break;
                default: 
                    Move(int.Parse(command.Replace("m", "")));
                    break;
            }

            Console.WriteLine($"Current position: ({x},{y}) {direction}");
        }
    }

    private bool IsValidCommand(string command)
    {
        if (command == "Left" || command == "Right")
            return true;
        else if (command.EndsWith("m") && int.TryParse(command.Replace("m", ""), out int distance))
            return true;
        else
            return false;
    }

    private void TurnLeft()
    {
        direction = (Direction)(((int)direction + 3) % 4);
    }

    private void TurnRight()
    {
        direction = (Direction)(((int)direction + 1) % 4);
    }

    private void Move(int distance)
    {
        switch (direction)
        {
            case Direction.North:
                y = Math.Min(y + distance, gridSize - 1);
                break;
            case Direction.East:
                x = Math.Min(x + distance, gridSize - 1);
                break;
            case Direction.South:
                y = Math.Max(y - distance, 0);
                break;
            case Direction.West:
                x = Math.Max(x - distance, 0);
                break;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MarsRover rover = new MarsRover();
        string[] commands = { "50m", "Left", "23m", "Left", "4m" };
        rover.ExecuteCommands(commands);
    }
}

