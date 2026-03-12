using ArcadeFootball.Scripts.Characters;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine;

[GlobalClass]
public partial class State : Node
{
    [Signal] public delegate void StateTransitionEventHandler(State fromState, StringName toState);
    protected Player Player { get; private set; } 
    protected AnimationPlayer AnimPlayer { get; private set; }
    protected Football Football { get; private set; }
    protected Area2D FootballArea { get; private set; }

    public void Initialize(StateMachine machine)
    {
        Player = machine.Player;
        AnimPlayer = machine.AnimPlayer;
        Football = machine.Football;
        FootballArea = machine.FootballArea;
    }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void Process(double delta){}
    public virtual void PhysicsProcess(double delta){}
}
