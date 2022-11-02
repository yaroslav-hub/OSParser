using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab2.Machines
{
    public class MooreMachine
    {
        private List<string> _states;
        private List<List<string>> _stateChanges = new List<List<string>>();
        private List<string> _outputSignals;
        private List<string> _commands = new List<string>();

        private StreamReader _reader;
        private StreamWriter _writer;
        private char _statePrefix;

        public MooreMachine(StreamReader reader, StreamWriter writer)
        {
            _reader = reader;
            _writer = writer;

            List<string> input = new List<string>();
            while (!_reader.EndOfStream)
            {
                input.Add(_reader.ReadLine());
            }

            _outputSignals = input[0].Split(';').Skip(1).Take(input[0].Split(';').Length - 1).ToList();
            _states = input[1].Split(';').Skip(1).Take(input[1].Split(';').Length - 1).ToList();
            for (int i = 2; i < input.Count; i++)
            {
                string[] values = input[i].Split(';');
                _commands.Add(values[0]);
                _stateChanges.Add(values.Skip(1).Take(values.Length - 1).ToList());
            }
            _statePrefix = _states[0][0];
        }        

        public void Minimize()
        {
            RemoveUnreachableStates();
            Dictionary<string, HashSet<string>> previousMinimizedMachine = new Dictionary<string, HashSet<string>>();
            Dictionary<string, HashSet<string>> minimizedMachine = new Dictionary<string, HashSet<string>>();
            foreach (string outputSignal in _outputSignals.ToHashSet())
            {
                HashSet<string> states = new HashSet<string>();
                for (int i = 0; i < _outputSignals.Count; i++)
                {
                    if (_outputSignals[i] == outputSignal)
                    {
                        states.Add(_states[i]);
                    }
                }
                minimizedMachine.Add(outputSignal, states);
            }
            List<List<string>> newStateChanges = MinimizeStateChanges(minimizedMachine, _stateChanges);

            minimizedMachine = MinimizeStates(previousMinimizedMachine, newStateChanges);
            while (minimizedMachine.Count != previousMinimizedMachine.Count && _states.Count != minimizedMachine.Count)
            {
                previousMinimizedMachine = minimizedMachine;
                minimizedMachine = MinimizeStates(previousMinimizedMachine, newStateChanges);
                newStateChanges = MinimizeStateChanges(minimizedMachine, _stateChanges);
            }

            _states = minimizedMachine.Keys.ToList();
            _stateChanges = BuildFinalStateChanges(minimizedMachine);
            _outputSignals = GetNewOutputSignals(minimizedMachine);
            PrintToOutput();
        }

        private void RemoveUnreachableStates()
        {
            HashSet<string> reachableStates = new HashSet<string>();

            foreach (List<string> row in _stateChanges)
            {
                foreach (string stateChange in row)
                {
                    if (stateChange != _states[row.IndexOf(stateChange)] || row.IndexOf(stateChange) == 0)
                    {
                        reachableStates.Add(stateChange);
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
                _outputSignals.RemoveAt(index);
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
            Dictionary<string, List<string>> newStatesByStateChanges = new Dictionary<string, List<string>>();

            for (int i = 0; i < _states.Count; i++)
            {
                List<string> newStateChanges = new List<string>();
                foreach (var stateChange in stateChanges)
                {
                    newStateChanges.Add(stateChange[i]);
                }
                bool minimizedStateExists = false;
                foreach (var newState in newStatesByStateChanges)
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
                    string newState = $"{_statePrefix}{newStatesByPreviousStates.Count}";
                    newStatesByPreviousStates.Add(newState, new HashSet<string>() { _states[i] });
                    newStatesByStateChanges.Add(newState, newStateChanges);
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
                    foreach (var newState in newStatesByStates)
                    {
                        if (newState.Value.Contains(stateChange))
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
                foreach (List<string> stateChangeRow in _stateChanges)
                {
                    foreach (var state in minimizedStatesByStates)
                    {
                        if (state.Value.Contains(stateChangeRow[_states.IndexOf(minimizedState.Value.First())]))
                        {
                            minimizedStateChangesRow.Add(state.Key);
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

        private List<string> GetNewOutputSignals(Dictionary<string, HashSet<string>> minimizedStatesByStates)
        {
            List<string> minimizedOutputSignals = new List<string>();

            foreach (var minimizedState in minimizedStatesByStates)
            {
                minimizedOutputSignals.Add(_outputSignals[_states.IndexOf(minimizedState.Value.First())]);
            }

            return minimizedOutputSignals;
        }

        private void PrintToOutput()
        {
            _writer.WriteLine($";{_outputSignals.Aggregate((total, part) => $"{total};{part}")};");
            _writer.WriteLine($";{_states.Aggregate((total, part) => $"{total};{part}")};");
            for (int i = 0; i < _commands.Count; i++)
            {
                _writer.WriteLine($"{_commands[i]};{_stateChanges[i].Aggregate((total, part) => $"{total};{part}")};");
            }
        }
    }
}
