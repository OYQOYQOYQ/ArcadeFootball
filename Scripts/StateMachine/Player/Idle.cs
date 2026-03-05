using Godot;

namespace Scripts.StateMachine.Player;

public partial class Idle : State
{
    public override void Enter()
    {
        Animtree.Set("parameters/conditions/IsNotRunning", true);
        Animtree.Set("parameters/conditions/IsRunning", false);
    }

    public override void PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
        if (direction != Vector2.Zero)
        {
            EmitSignalStateTransition(this, "Run");
        }
    }
}
