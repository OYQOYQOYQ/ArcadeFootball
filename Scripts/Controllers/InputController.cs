using static ArcadeFootball.Scripts.Core.StringNames;
using ArcadeFootball.Scripts.Core;
using Godot;

namespace ArcadeFootball.Scripts.Controllers;

public partial class InputController : Node
{
    private Vector2 P1Direction { get; set; }
    private Vector2 P2Direction { get; set; }
    private bool P1SlideTackle { get; set; }
    private bool P2SlideTackle { get; set; }

    public static InputController Instance {get; private set;}

    public override void _Ready()
    {
        if (Instance != null && Instance != this)
        {
            #if DEBUG
            GD.PrintErr("检测到多个 InputController 实例！");
            #endif
            
            QueueFree();
            return;
        }
        Instance = this;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(P1.SlideTackle))
            P1SlideTackle = true;
        else if (@event.IsActionPressed(P2.SlideTackle))
            P2SlideTackle = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        P1Direction = Input.GetVector(P1.Left, P1.Right, P1.Up, P1.Down);
        P2Direction = Input.GetVector(P2.Left, P2.Right, P2.Up, P2.Down);
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
