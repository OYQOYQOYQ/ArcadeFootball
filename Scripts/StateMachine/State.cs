using Godot;

namespace Scripts.StateMachine;


[GlobalClass]
public partial class State : Node
{
    [Signal]
    public delegate void StateTransitionEventHandler(State fromState, StringName toState);
    [Export]
    public AnimationTree Animtree { get; set; }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void Process(double delta){}
    public virtual void PhysicsProcess(double delta){}
}
