using Godot;
using static ArcadeFootball.Scripts.Core.StringNames;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class Run : State
{
    public override void Enter()
    {
        AnimPlayer.Play(RunState);

        #if DEBUG
        GD.Print($"{Player.PlayerType} 进入了 Run 状态");
        #endif
    }

    public override void PhysicsProcess(double delta)
    {
        if(Player.IsSlideTackle)
        {
            EmitSignalStateTransition(this, SlideTackleState);
            return;
        }

        if (Player.Direction == Vector2.Zero)
        {
            EmitSignalStateTransition(this, IdleState);
        }
        else
        {
            Player.Velocity = Player.Direction * Player.MoveSpeed;
        }
    }
}
