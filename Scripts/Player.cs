using Godot;
using Scripts.StateMachine;

public partial class Player : CharacterBody2D
{
    [Export] 
    public float MoveSpeed { get; private set; } = 80.0f;
    [Export]
    private StateMachine _stateMachine;
    [Export]
    private Sprite2D sprite2D;

    private Vector2 initDirection = Vector2.Right;

    public override void _PhysicsProcess(double delta)
    {
        _stateMachine._PhysicsProcess(delta);
    }
}
