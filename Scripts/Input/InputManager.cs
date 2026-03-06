using Godot;

namespace ArcadeFootball.Scripts.Input;

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
    private StringName P1Left {get; set;} = "p1_left";
    private StringName P1Right {get; set;} = "p1_right";
    private StringName P1Up {get; set;} = "p1_up";

    private StringName P1Down {get; set;} = "p1_down";
    // P2 inputs
    private StringName P2Left {get; set;} = "p2_left";
    private StringName P2Right {get; set;} = "p2_right";
    private StringName P2Up {get; set;} = "p2_up";
    private StringName P2Down {get; set;} = "p2_down";
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
                _direction = Input.GetVector(P1Left, P1Right, P1Up, P1Down);
                return _direction;
            case EPlayerType.P2:
                _direction = Input.GetVector(P2Left, P2Right, P2Up, P2Down);
                return _direction;
            case EPlayerType.Cpu:
            default:
                return Vector2.Zero;
        }
    }
}
