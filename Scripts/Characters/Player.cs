using ArcadeFootball.Scripts.Controllers;
using ArcadeFootball.Scripts.Enums;
using Godot;

namespace ArcadeFootball.Scripts.Characters;

public partial class Player : CharacterBody2D
{
    [Export] public float MoveSpeed { get; private set; } = 80.0f;  // 玩家移动速度
    [Export] public float SlideTackleDuration = 0.5f;
    [Export] private EPlayerType _ePlayerType;
    [Export] private Sprite2D _playerSprite;
    [Export] public CollisionShape2D PlayerCollisionShape { get; set; }

    public Vector2 Direction { get; private set; }
    public bool IsSlideTackle { get; set; } = false;

    public override void _PhysicsProcess(double delta)
    {
        Direction = InputController.Instance.GetInputPlayerDirection(_ePlayerType);
        IsSlideTackle = InputController.Instance.GetInputPlayerSlideTackle(_ePlayerType);
        GD.Print(IsSlideTackle);
        PlayerIsFlipH(Direction);
        MoveAndSlide();
    }

    /// <summary>
    /// 根据角色当前的方向, 判断是否水平翻转.
    /// </summary>
    /// <param name="direction"> 角色当前的方向. </param>
    private void PlayerIsFlipH(Vector2 direction)
    {
        _playerSprite.FlipH = direction.X switch
        {
            < 0 => true,
            > 0 => false,
            _ => _playerSprite.FlipH
        };
    }
}
