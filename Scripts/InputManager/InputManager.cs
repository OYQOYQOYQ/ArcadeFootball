using Godot;

namespace ArcadeFootball.Scripts;

public enum EPlayerType
{
    P1,
    P2,
    Cpu,
}

public partial class InputManager : Node
{
    public static InputManager Instance {get; private set;}
    // P1 inputs
    public StringName P1_Left {get; private set;} = "p1_left";
    public StringName P1_Right {get; private set;} = "p1_right";
    public StringName P1_Up {get; private set;} = "p1_up";
    public StringName P1_Down {get; private set;} = "p1_down";
    // P2 inputs
    public StringName P2_Left {get; private set;} = "p2_left";
    public StringName P2_Right {get; private set;} = "p2_right";
    public StringName P2_Up {get; private set;} = "p2_up";
    public StringName P2_Down {get; private set;} = "p2_down";
    private Vector2 _direction;

    public override void _Ready()
    {
        Instance = this;
    }

    public Vector2 GetInputPlayer(EPlayerType ePlayerType)
    {
        switch (ePlayerType)
        {
            case EPlayerType.P1:
                _direction = Input.GetVector(P1_Left, P1_Right, P1_Up, P1_Down);
                return _direction;
            case EPlayerType.P2:
                _direction = Input.GetVector(P2_Left, P2_Right, P2_Up, P2_Down);
                return _direction;
            default:
                return Vector2.Zero;
        }
    }
}
