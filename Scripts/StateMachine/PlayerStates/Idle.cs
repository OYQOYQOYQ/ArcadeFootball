using Godot;
using static ArcadeFootball.Scripts.Core.StringNames;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class Idle : State
{
    public override void Enter()
    {
        AnimPlayer.Play(IdleState);
        Player.Velocity = Vector2.Zero;

        #if DEBUG
        GD.Print($"{Player.PlayerType} 进入了 Idle 状态");
        #endif
    }

    public override void PhysicsProcess(double delta)
    {
        if (Player.Direction != Vector2.Zero)
        {
            EmitSignalStateTransition(this, RunState);
        }
    }
}
