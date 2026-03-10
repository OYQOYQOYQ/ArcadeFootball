using ArcadeFootball.Scripts.Characters;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine;

[GlobalClass]
public partial class State : Node
{
    [Signal] public delegate void StateTransitionEventHandler(State fromState, StringName toState);
    protected Player Player { get; private set; } 
    protected AnimationPlayer AnimPlayer { get; private set; }
    
    public override void _Ready()
    {
        Player = Owner as Player;
        AnimPlayer = Player?.GetNode<AnimationPlayer>("%AnimationPlayer");
    }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void Process(double delta){}
    public virtual void PhysicsProcess(double delta){}
}
