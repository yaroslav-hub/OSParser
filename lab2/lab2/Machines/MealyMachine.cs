using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab2.Machines
{
    public class MealyMachine
    {
        private List<string> _states;
        private List<List<string>> _stateChanges = new List<List<string>>();
        private List<string> _commands = new List<string>();

        private StreamReader _reader;
        private StreamWriter _writer;

        public MealyMachine(StreamReader reader, StreamWriter writer)
        {
            _reader = reader;
            _writer = writer;

            List<string> input = new List<string>();
            while (!_reader.EndOfStream)
            {
                input.Add(_reader.ReadLine());
            }

            _states = input[0].Split(';').Skip(1).Take(input[0].Split(';').Length - 1).ToList();
            for (int i = 1; i < input.Count; i++)
            {
                string[] values = input[i].Split(';');
                _commands.Add(values[0]);
                _stateChanges.Add(values.Skip(1).Take(values.Length - 1).ToList());
            }
        }

        private void PrintToOutput()
        {
            _writer.WriteLine($";{_states.Aggregate((total, part) => $"{total};{part}")};");

            for (int i = 0; i < _commands.Count; i++)
            {
                _writer.WriteLine($"{_commands[i]};{_stateChanges[i].Aggregate((total, part) => $"{total};{part}")};");
            }
        }

        public void Minimize()
        {
            RemoveUnreachableStates();
            Dictionary<string, HashSet<string>> previousMinimizedMachine = new Dictionary<string, HashSet<string>>();
            Dictionary<string, HashSet<string>> minimizedMachine = new Dictionary<string, HashSet<string>>();
            foreach (string state in _states)
            {
                minimizedMachine.Add(state, _states.ToHashSet());
            }
            List<List<string>> newStateChanges = _stateChanges;
            previousMinimizedMachine = minimizedMachine;
            minimizedMachine = MinimizeStates(previousMinimizedMachine, newStateChanges);
            newStateChanges = MinimizeStateChanges(minimizedMachine, _stateChanges);

            while (minimizedMachine.Count != previousMinimizedMachine.Count && _states.Count != minimizedMachine.Count)
            {
                previousMinimizedMachine = minimizedMachine;
                minimizedMachine = MinimizeStates(previousMinimizedMachine, newStateChanges);
                newStateChanges = MinimizeStateChanges(minimizedMachine, _stateChanges);
            }

            List<string> minimizedStates = minimizedMachine.Keys.ToList();
            List<List<string>> minimizedTransitionFunctions = BuildFinalStateChanges(minimizedMachine);
            _states = minimizedStates;
            _stateChanges = minimizedTransitionFunctions;
            PrintToOutput();
        }

        private void RemoveUnreachableStates()
        {
            HashSet<string> reachableStates = new HashSet<string>();

            foreach (List<string> row in _stateChanges)
            {
                foreach (string stateChange in row)
                {
                    if (stateChange.Split('/')[0] != _states[row.IndexOf(stateChange)] || row.IndexOf(stateChange) == 0)
                    {
                        reachableStates.Add(stateChange.Split('/')[0]);
                    }
                }
            }

            if (reachableStates.Count == _states.Count)
            {
                return;
            }

            foreach (string state in _states)
            {
                if (reachableStates.Contains(state))
                {
                    continue;
                }
                int index = _states.IndexOf(state);
                _states.Remove(state);
                foreach (var _stateChange in _stateChanges)
                {
                    _stateChange.RemoveAt(index);
                }
            }
        }

        private Dictionary<string, HashSet<string>> MinimizeStates(Dictionary<string, HashSet<string>> equivalentClasses, 
            List<List<string>> stateChanges)
        {
            Dictionary<string, HashSet<string>> newStatesByPreviousStates = new Dictionary<string, HashSet<string>>();
            Dictionary<string, List<string>> newStatesByStateChange = new Dictionary<string, List<string>>();

            for (int i = 0; i < _states.Count; i++)
            {
                List<string> newStateChanges = new List<string>();
                foreach (var stateChange in stateChanges)
                {
                    if (stateChange[i].Contains('/'))
                    {
                        newStateChanges.Add(stateChange[i].Split('/')[1]);
                    }
                    else
                    {
                        newStateChanges.Add(stateChange[i]);
                    }
                }
                bool minimizedStateExists = false;
                foreach (var newState in newStatesByStateChange)
                {
                    if (newState.Value.SequenceEqual(newStateChanges))
                    {
                        string equivalentClassFirstElem = newStatesByPreviousStates[newState.Key].First();
                        string firstEquivalentClass = string.Empty;
                        string secondEquivalentClass = string.Empty;
                        foreach (var equivalentClass in equivalentClasses)
                        {
                            if (equivalentClass.Value.Contains(equivalentClassFirstElem))
                            {
                                firstEquivalentClass = equivalentClass.Key;
                            }
                            if (equivalentClass.Value.Contains(_states[i]))
                            {
                                secondEquivalentClass = equivalentClass.Key;
                            }
                        }
                        if (firstEquivalentClass == secondEquivalentClass)
                        {
                            newStatesByPreviousStates[newState.Key].Add(_states[i]);
                            minimizedStateExists = true;
                            break;
                        }
                    }
                }
                if (!minimizedStateExists)
                {
                    string newState = $"A{newStatesByPreviousStates.Count}";
                    newStatesByPreviousStates.Add(newState, new HashSet<string>() { _states[i] });
                    newStatesByStateChange.Add(newState, newStateChanges);
                }
            }

            return newStatesByPreviousStates;
        }

        private List<List<string>> MinimizeStateChanges(Dictionary<string, HashSet<string>> newStatesByStates, 
            List<List<string>> sourceStateChanges)
        {
            List<List<string>> newStateChanges = new List<List<string>>();

            foreach (List<string> stateChangesRow in sourceStateChanges)
            {
                List<string> newStateChangesRow = new List<string>();
                foreach (string stateChange in stateChangesRow)
                {
                    string validStateChange = stateChange.Contains('/') ? stateChange.Split('/')[0] : stateChange;
                    foreach (var newState in newStatesByStates)
                    {
                        if (newState.Value.Contains(validStateChange))
                        {
                            newStateChangesRow.Add(newState.Key);
                        }
                    }
                }
                newStateChanges.Add(newStateChangesRow);
            }

            return newStateChanges;
        }

        private List<List<string>> BuildFinalStateChanges(Dictionary<string, HashSet<string>> minimizedStatesByStates)
        {
            List<List<string>> minimizedStateChanges = new List<List<string>>();

            foreach (var minimizedState in minimizedStatesByStates)
            {
                List<string> minimizedStateChangesRow = new List<string>();
                foreach (List<string> stateChangesRow in _stateChanges)
                {
                    foreach (var state in minimizedStatesByStates)
                    {
                        if (state.Value.Contains(stateChangesRow[_states.IndexOf(minimizedState.Value.First())].Split('/')[0]))
                        {
                            minimizedStateChangesRow.Add(
                                $"{state.Key}/{stateChangesRow[_states.IndexOf(minimizedState.Value.First())].Split('/')[1]}");
                        }
                    }
                }
                if (minimizedStateChanges.Count != minimizedStateChangesRow.Count)
                {
                    foreach (string stateChange in minimizedStateChangesRow)
                    {
                        minimizedStateChanges.Add(new List<string>() { stateChange });
                    }
                }
                else
                {
                    for (int i = 0; i < minimizedStateChanges.Count; i++)
                    {
                        minimizedStateChanges[i].Add(minimizedStateChangesRow[i]);
                    }
                }
            }

            return minimizedStateChanges;
        }
    }
}
