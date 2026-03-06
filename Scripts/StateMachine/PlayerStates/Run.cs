using Godot;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class Run : State
{
    public override void Enter()
    {
        AnimPlayer.Play("Run");
        GD.Print("Run state entered");
    }

    public override void PhysicsProcess(double delta)
    {
        if (Player.IsSlideTackle) 
        { 
            EmitSignalStateTransition(this, "SlideTackle");
        }

        if (Player.Direction == Vector2.Zero)
        {
            EmitSignalStateTransition(this, "Idle");
        }
        else
        {
            Player.Velocity = Player.Direction * Player.MoveSpeed;
        }
    }
}
