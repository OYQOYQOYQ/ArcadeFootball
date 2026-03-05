using Godot;

namespace ArcadeFootball.Scripts;

public partial class Player : CharacterBody2D
{
    [Export] 
    public float MoveSpeed { get; private set; } = 80.0f;
    [Export] 
    public Node StateMachine { get; set; }
    public Vector2 Direction;
    
    [Export]
    private Sprite2D _sprite2D;

    public override void _PhysicsProcess(double delta)
    {
        Direction = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
        PlayerIsFlipH(Direction);
        StateMachine._PhysicsProcess(delta);
        MoveAndSlide();
    }

    private void PlayerIsFlipH(Vector2 direction)
    {
        if (direction == Vector2.Left)
        {
            _sprite2D.FlipH = true;
        }
        else if (direction == Vector2.Right)
        {
            _sprite2D.FlipH = false;
        }
    }
}