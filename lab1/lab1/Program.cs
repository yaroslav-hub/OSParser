using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string mode = args[0];
            string input = args[1];
            string output = args[2];

            using (StreamReader reader = new StreamReader(input))
            {
                using (StreamWriter writer = new StreamWriter(output))
                {
                    if (mode.Equals("mealy-to-moore"))
                    {
                        MealyToMoore(reader, writer);
                    }
                    else if (mode.Equals("moore-to-mealy"))
                    {
                        MooreToMealy(reader, writer);
                    }
                    else
                    {
                        Console.WriteLine("Unknown mode");
                    }
                }
            }
        }

        private static void MooreToMealy(StreamReader reader, StreamWriter writer)
        {
            string[] outputSignals = reader.ReadLine().Split(';');
            
            string states = reader.ReadLine();    
            writer.Write(states);

            while (!reader.EndOfStream)
            {
                writer.WriteLine();
                string[] stateChanges = reader.ReadLine().Split(';');
                writer.Write(stateChanges[0]);
                for (int i = 1; i < stateChanges.Length; i++)
                {
                    string outputSignalColumnName = stateChanges[i];
                    int outputSymbolColumnIndex = states.Split(';').ToList().FindIndex(x => x.Equals(outputSignalColumnName));
                    writer.Write($";{stateChanges[i]}/{outputSignals[outputSymbolColumnIndex]}");
                }                
            }
        }

        private static void MealyToMoore(StreamReader reader, StreamWriter writer)
        {
            string[] states = reader.ReadLine().Split(';');
            List<List<string>> stateChangeLines = new List<List<string>>();
            while (!reader.EndOfStream)
            {
                stateChangeLines.Add(reader.ReadLine().Split(';').ToList());
            }
            List<string> commands = new List<string>();
            foreach(var line in stateChangeLines)
            {
                commands.Add(line[0]);
            }

            List<string> newStates = new List<string>();
            for (int i = 0; i < stateChangeLines.Count; i++)
            {
                for (int j = 1; j < stateChangeLines[0].Count; j++)
                {
                    if (!newStates.Contains(stateChangeLines[i][j]))
                    {
                        newStates.Add(stateChangeLines[i][j]);
                    }
                }
            }

            List<List<int>> replacedStatesMatrix = new List<List<int>>();
            for (int i = 0; i < stateChangeLines.Count; i++)
            {
                List<int> line = new List<int>();
                for (int j = 1; j < stateChangeLines[0].Count; j++)
                {
                    int newStateIndex = newStates.FindIndex(x => x.Equals(stateChangeLines[i][j]));
                    line.Add(newStateIndex);
                }
                replacedStatesMatrix.Add(line);
            }

            List<int> sourceColumnLinesForNewStates = new List<int>();
            foreach (var newState in newStates)
            {
                string oldState = newState.Split('/')[0];
                int index = states.ToList().FindIndex(x => x.Equals(oldState));
                sourceColumnLinesForNewStates.Add(index);
            }

            char signalName = ' ';
            foreach (var newState in newStates)
            {
                string signal = newState.Split('/')[1];
                signalName = signal[0];
                writer.Write($";{signal}");
            }
            writer.WriteLine();

            char commandName = commands[0][0];
            char stateName = states[1][0];
            List<char> newStateNameVariants = new List<char> { 'a', 'b', 'c', 'd' };
            newStateNameVariants.Remove(stateName);
            newStateNameVariants.Remove(commandName);
            newStateNameVariants.Remove(signalName);
            char newStateName = newStateNameVariants[0];
            
            for (int i = 0; i < newStates.Count; i++)
            {
                writer.Write($";{newStateName}{i}");
            }
            writer.WriteLine();

            for (int i = 0; i < commands.Count; i++)
            {
                writer.Write(commands[i]);
                for (int j = 0; j < newStates.Count; j++)
                {
                    int sourceColumnLine = sourceColumnLinesForNewStates[j];
                    writer.Write($";{newStateName}{replacedStatesMatrix[i][sourceColumnLine - 1]}");
                }
                writer.WriteLine();
            }
        }
    }
}
