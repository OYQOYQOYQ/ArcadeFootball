using ArcadeFootball.Scripts.Characters;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine;

[GlobalClass]
public partial class State : Node
{
    [Signal]
    public delegate void StateTransitionEventHandler(State fromState, StringName toState);
    [Export]
    public Player Player { get; set; }
    [Export]
    protected AnimationPlayer AnimPlayer { get; set; }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void Process(double delta){}
    public virtual void PhysicsProcess(double delta){}
}
