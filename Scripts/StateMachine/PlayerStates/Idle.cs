using Godot;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class Idle : State
{
    public override void Enter()
    {
        AnimPlayer.Play("Idle");
        Player.Velocity = Vector2.Zero;
        GD.Print("Idle state entered");
    }

    public override void PhysicsProcess(double delta)
    {
        if (Player.Direction != Vector2.Zero)
        {
            EmitSignalStateTransition(this, "Run");
        }
    }
}
