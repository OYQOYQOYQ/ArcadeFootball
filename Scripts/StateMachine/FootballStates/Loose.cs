using static ArcadeFootball.Scripts.Core.StringNames;
using ArcadeFootball.Scripts.Characters;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine.FootballStates;

public partial class Loose : State
{
    public override void Enter()
    {
        FootballArea.BodyEntered += OnBodyEntered;
    }

    public override void PhysicsProcess(double delta)
    {
    }
    
    public override void Exit()
    {
        FootballArea.BodyEntered -= OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            Football.CurrentPlayer ??= player;
            EmitSignalStateTransition(this, DribbleState);
        }
    }
}
