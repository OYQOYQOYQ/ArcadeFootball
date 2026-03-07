using ArcadeFootball.Scripts.Characters;
using ArcadeFootball.Scripts.Enums;
using Godot;

namespace ArcadeFootball.Scripts.Controllers;

public partial class InputController : Node
{
    # region P1 InputMap
    private StringName P1Left {get; set;} = "p1_left";
    private StringName P1Right {get; set;} = "p1_right";
    private StringName P1Up {get; set;} = "p1_up";
    private StringName P1Down {get; set;} = "p1_down";
    private StringName P1SlideTackleKey {get; set;} = "p1_slide_tackle";
    #endregion

    #region P2 InputMap
    private StringName P2Left {get; set;} = "p2_left";
    private StringName P2Right {get; set;} = "p2_right";
    private StringName P2Up {get; set;} = "p2_up";
    private StringName P2Down {get; set;} = "p2_down";
    private StringName P2SlideTackleKey {get; set;} = "p2_slide_tackle";
    #endregion

    #region 输入状态（只读）
    public Vector2 P1Direction { get; private set; }
    public Vector2 P2Direction { get; private set; }
    public bool P1SlideTackle { get; set; }
    public bool P2SlideTackle { get; set; }
    #endregion

    public static InputController Instance {get; private set;}

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(P1SlideTackleKey))
            P1SlideTackle = true;
        else if (@event.IsActionPressed(P2SlideTackleKey))
            P2SlideTackle = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        P1Direction = Input.GetVector(P1Left, P1Right, P1Up, P1Down);
        P2Direction = Input.GetVector(P2Left, P2Right, P2Up, P2Down);
    }

    public void ResetSlideTackle(EPlayerType ePlayerType)
    {
        if (ePlayerType == EPlayerType.P1) P1SlideTackle = false;
        else if (ePlayerType == EPlayerType.P2) P2SlideTackle = false;
    }

    public Vector2 GetInputPlayerDirection(EPlayerType ePlayerType)
    {
        return ePlayerType switch
        {
            EPlayerType.P1 => P1Direction,
            EPlayerType.P2 => P2Direction,
            _ => Vector2.Zero,
        };
    }

    public bool GetInputPlayerSlideTackle(EPlayerType ePlayerType) 
    {
        return ePlayerType switch
        {
            EPlayerType.P1 => P1SlideTackle,
            EPlayerType.P2 => P2SlideTackle,
            _ => false,
        };
    }
}
