using Godot;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class SlideTackle : State
{
    [Export]
    private float _slideTackleSpeed = 100.0f;
    private float _timer;

    public override void Enter()
    {
        Player.PlayerCollisionShape.Set("height", 18.0f);
        AnimPlayer.Play("SlideTackle");
        _timer = Player.SlideTackleDuration;
        Player.Velocity = Player.Direction * _slideTackleSpeed;

        GD.Print("SlideTackle state entered");
        GD.Print($" 持续{_timer}s");
    }

    public override void PhysicsProcess(double delta)
    {
        Player.Velocity = Player.Direction * _slideTackleSpeed;
        _timer -= (float)delta;
        if (_timer <= 0)
        {
            if (Player.Direction != Vector2.Zero)
            {
                EmitSignalStateTransition(this, "Run");
            }
            else 
            { 
                EmitSignalStateTransition(this, "Idle");
            }
        }
    }

    public override void Exit()
    {
        Player.PlayerCollisionShape.Set("height", 15.0f);
    }
}