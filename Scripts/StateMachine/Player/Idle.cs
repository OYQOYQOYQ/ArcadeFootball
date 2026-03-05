using Godot;

namespace ArcadeFootball.Scripts.StateMachine.Player;

public partial class Idle : State
{
    public override void Enter()
    {
        AnimTree.Set("parameters/conditions/IsNotRunning", true);
        AnimTree.Set("parameters/conditions/IsRunning", false);
        Player.Velocity = Vector2.Zero;
    }

    public override void PhysicsProcess(double delta)
    {
        if (Player.Direction != Vector2.Zero)
        {
            EmitSignalStateTransition(this, "Run");
        }
    }
}
