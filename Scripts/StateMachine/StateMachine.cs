using System.Collections.Generic;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine;

public partial class StateMachine : Node
{
    [Export]
    public State CurrentState { get; private set; }
    private readonly Dictionary<StringName, State> _states = [];

    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is not State state) continue;
            _states.Add(child.Name, state);
            state.StateTransition += OnStateTransition;
        }
    }

    public override void _Process(double delta)
    {
        CurrentState?.Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        CurrentState?.PhysicsProcess(delta);
    }

    public override void _ExitTree()
    {
        foreach (var state in _states.Values) 
        { 
            state.StateTransition -= OnStateTransition;
        }
        _states.Clear();
    }

    private void OnStateTransition(State fromState, StringName toState)
    {
        if (! _states.TryGetValue(toState, out var newState)) return;
        if (CurrentState == newState) return;
        CurrentState.Exit();
        newState.Enter();
        CurrentState = newState;
    }
}
