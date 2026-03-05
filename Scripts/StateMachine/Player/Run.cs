using Godot;

namespace ArcadeFootball.Scripts.StateMachine.Player;

public partial class Run : State
{
    public override void Enter()
    {
        AnimTree.Set("parameters/conditions/IsNotRunning", false);
        AnimTree.Set("parameters/conditions/IsRunning", true);
    }

    public override void PhysicsProcess(double delta)
    {
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
