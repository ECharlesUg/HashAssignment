using System;
using System.Collections.Generic;
using System.IO;

namespace PanMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the txt file
            string input = @"C:\Users\Admin\source\repos\HashAssignment\HashAssignment\maze1.txt";
            Maze(input);
        }

        // Function to solve the maze
        public static void Maze(string input)
        {
            // Read the contents from the text file
            string[] text = File.ReadAllLines(input);
            int rows = text.Length;
            int cols = text[0].Split(',').Length;
            int[,] maze = new int[rows, cols];
            Dictionary<(int, int), (int, int)> path = new Dictionary<(int, int), (int, int)>();

            // turns content in file into a 2D array
            for (int i = 0; i < rows; i++)
            {
                string[] row = text[i].Split(',');
                for (int j = 0; j < cols; j++)
                {
                    maze[i, j] = int.Parse(row[j]);
                }
            }

            bool[,] visited = new bool[rows, cols];
            Stack<(int, int)> stack = new Stack<(int, int)>();
            // pushes the first path cell into stack
            stack.Push((0, 0));

            //searches contents in stack until the stack is empty
            while (stack.Count > 0)
            {
                (int x, int y) = stack.Pop();
                // marks the current cell as visited
                visited[x, y] = true;
                // Check if the current cell is (N,M)
                if (x == rows - 1 && y == cols - 1)
                {
                    // once destination is reached, message prints and is returned to stop the function from running
                    Console.WriteLine("Ophelia, I found you a path!");
                    PrintPath(path, rows - 1, cols - 1);
                    return;
                }

                foreach ((int row, int col) in Directions(x, y))
                {
                    //gets the directions of the cells
                    int newX = x + row;
                    int newY = y + col;

                    // Check if the cell is within the bounds of the maze and if it is not already visited
                    if (newX >= 0 && newX < rows && newY < cols && newY >= 0 && maze[newX, newY] == 1 && visited[newX, newY] == false)
                    {
                        stack.Push((newX, newY));
                        path[(newX, newY)] = (x, y);
                    }
                }
            }
            Console.WriteLine("Ophelia, I couldn't find you a path :'(");
        }
        // function that prints path used from the cells stored in dictionary
        public static void PrintPath(Dictionary<(int, int), (int, int)> path, int x, int y)
        {
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((x, y));
            while (path.ContainsKey((x, y)))
            {
                // re-initializes path variables into a new variable to add to stack
                (x, y) = path[(x, y)];
                stack.Push((x, y));
            }

            Console.Write("The path: ");
            while (stack.Count > 0)
            {
                var (row, col) = stack.Pop();
                Console.Write($"({row}, {col})");
                if(stack.Count > 0)
                {
                    Console.Write(" -> ");
                }
              
                
            }
            Console.WriteLine(" -> Tadaaa!");
        }

        // Function to get the possible directions of the cells
        public static List<(int, int)> Directions(int x, int y)
        {
            List<(int, int)> list = new List<(int, int)>();
            // Adds possible directions
            list.Add((1, 0));
            list.Add((-1, 0));
            list.Add((0, 1));
            list.Add((0, -1));
            return list;
        }
    }
}
