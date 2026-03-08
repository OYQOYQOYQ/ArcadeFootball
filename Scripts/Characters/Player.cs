// 玩家脚本
// 

using System;
using ArcadeFootball.Scripts.Controllers;
using ArcadeFootball.Scripts.Core;
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

    public InputController GameInput { get; private set; }
    private float _lastDirectionX;

    public override void _Ready()
	{
		PlayerSprite ??= GetNode<Sprite2D>("%Sprite2D");
		PlayerCollisionShape ??= GetNode<CollisionShape2D>("%CollisionShape2D");
		SlideTackleTimer ??= GetNode<Timer>("%SlideTackleTimer");

		SlideTackleTimer.WaitTime = SlideTackleCooldown;
		GameInput = InputController.Instance;
		if (GameInput == null)
		{
			#if DEBUG
			GD.PrintErr("InputController 未初始化！请确保它在场景树种先于 Player 节点加载！");
			#endif
			SetPhysicsProcess(false);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Direction = GameInput.GetInputPlayerDirection(PlayerType);
		if (IsSlideTackleAvailable) 
		{ 
			IsSlideTackle = GameInput.GetInputPlayerSlideTackle(PlayerType);
		}
		else 
		{ 
			IsSlideTackle = false;
			GameInput.ResetSlideTackle(PlayerType);
		}
		PlayerIsFlipH(Direction);
		MoveAndSlide();
	}

	// 根据角色当前的方向 判断是否水平翻转
	private void PlayerIsFlipH(Vector2 direction)
	{
		if (direction.X == 0) return;
		if (Math.Abs(direction.X - _lastDirectionX) < 0.01f) return;
		
		_lastDirectionX = direction.X;
		PlayerSprite.FlipH = direction.X < 0;
	}
}
