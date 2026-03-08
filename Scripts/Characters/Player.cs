// 玩家脚本
// 

using ArcadeFootball.Scripts.Controllers;
using ArcadeFootball.Scripts.Enums;
using Godot;

namespace ArcadeFootball.Scripts.Characters;

public partial class Player : CharacterBody2D
{
	#region Export
	[Export] public EPlayerType PlayerType { get; private set; }  // 玩家类型
	[Export] public float MoveSpeed { get; private set; } = 80.0f;  // 玩家移动速度

	[ExportGroup("Slide Tackle")]
	[Export] public float SlideTackleSpeed { get; private set; } = 100.0f;  // 滑铲速度 
	[Export(PropertyHint.Range, "0, 0.5, 0.1")] 
	public float SlideTackleDuration { get; private set; } = 0.2f;  // 滑铲持续时间
	[Export(PropertyHint.Range, "0, 1, 0.1")]
	public float SlideTackleCooldown { get; private set; } = 0.5f;  // 滑铲冷却时间

	[ExportGroup("Node Reference")]
	[Export] public Sprite2D PlayerSprite { get; private set; }  // 玩家精灵
	[Export] public CollisionShape2D PlayerCollisionShape { get; set; }  // 玩家碰撞体
	[Export] public Timer SlideTackleTimer { get; private set; }  // 滑铲计时器
	#endregion
	
	public Vector2 Direction { get; private set; }  // 玩家输入的移动方向
	public bool IsSlideTackle { get; private set; }  // 是否处于滑铲状态
	public bool IsSlideTackleAvailable { get; set; } = true;  // 滑铲是否可用

	public override void _Ready()
	{
		PlayerSprite ??= GetNode<Sprite2D>("%Sprite2D");
		PlayerCollisionShape ??= GetNode<CollisionShape2D>("%CollisionShape2D");
		SlideTackleTimer ??= GetNode<Timer>("%SlideTackleTimer");
		SlideTackleTimer.WaitTime = SlideTackleCooldown;
	}

	public override void _PhysicsProcess(double delta)
	{
		Direction = InputController.Instance.GetInputPlayerDirection(PlayerType);
		if (IsSlideTackleAvailable) 
		{ 
			IsSlideTackle = InputController.Instance.GetInputPlayerSlideTackle(PlayerType);
		}
		else 
		{ 
			IsSlideTackle = false;
			InputController.Instance.ResetSlideTackle(PlayerType);
		}
		PlayerIsFlipH(Direction);
		MoveAndSlide();
	}

	// 根据角色当前的方向 判断是否水平翻转
	private void PlayerIsFlipH(Vector2 direction)
	{
		PlayerSprite.FlipH = direction.X switch
		{
			< 0 => true,
			> 0 => false,
			_ => PlayerSprite.FlipH
		};
	}
}
