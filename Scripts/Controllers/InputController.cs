using ArcadeFootball.Scripts.Enums;
using Godot;

namespace ArcadeFootball.Scripts.Controllers;

public partial class InputController : Node
{
    public static InputController Instance {get; private set;}
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

    public override void _Ready()
    {
        Instance = this;
    }

    public Vector2 GetInputPlayerDirection(EPlayerType ePlayerType)
    {
        return ePlayerType switch
        {
            EPlayerType.P1 => Input.GetVector(P1Left, P1Right, P1Up, P1Down),
            EPlayerType.P2 => Input.GetVector(P2Left, P2Right, P2Up, P2Down),
            _ => Vector2.Zero,
        };
    }

    public bool GetInputPlayerSlideTackle(EPlayerType ePlayerType) 
    {
        return ePlayerType switch
        {
            EPlayerType.P1 => Input.IsActionJustPressed("p1_slide_tackle"),
            EPlayerType.P2 => Input.IsActionJustPressed("p2_slide_tackle"),
            _ => false,
        };
    }
}
