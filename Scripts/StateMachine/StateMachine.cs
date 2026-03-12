using System.Collections.Generic;
using System.Linq;
using ArcadeFootball.Scripts.Characters;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine;

public partial class StateMachine : Node
{
	[Export]
	public State CurrentState { get; private set; }
	private readonly Dictionary<StringName, State> _states = [];

	public Player Player { get; private set; }
	public AnimationPlayer AnimPlayer { get; private set; }
	public Football Football { get; private set; }
	public Area2D FootballArea { get; private set; }

	public override void _Ready()
	{
		Player = Owner as Player;
		if (Player != null)
		{
			AnimPlayer = Player.GetNode<AnimationPlayer>("%AnimationPlayer");
			GD.Print(Player.Name);
		}

		Football = Owner as Football;
		if (Football != null)
		{
			FootballArea = Football.GetNode<Area2D>("Area2D");
			GD.Print(Football.Name);
		}

		foreach (var child in GetChildren())
		{
			if (child is not State state) continue;
			_states.Add(child.Name, state);
			state.StateTransition += OnStateTransition;
			state.Initialize(this);
		}

		if (CurrentState == null && _states.Count > 0)
		{
			CurrentState = _states.Values.First();
			CurrentState.Enter();
		}
		else
		{
			CurrentState?.Enter();
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
