using Godot;

namespace ArcadeFootball.Scripts.StateMachine;


[GlobalClass]
public partial class State : Node
{
    [Signal]
    public delegate void StateTransitionEventHandler(State fromState, StringName toState);
    [Export]
    public Scripts.Player Player { get; set; }
    [Export]
    protected AnimationTree AnimTree { get; set; }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void Process(double delta){}
    public virtual void PhysicsProcess(double delta){}
}
