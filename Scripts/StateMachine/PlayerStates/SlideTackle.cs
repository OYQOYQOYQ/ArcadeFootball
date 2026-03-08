using ArcadeFootball.Scripts.Characters;
using Godot;
using static ArcadeFootball.Scripts.Core.StringNames;
using static ArcadeFootball.Scripts.Core.DataConstants;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class SlideTackle : State
{
	private float _slideTackleSpeed;  // 缓存滑铲速度
    private float _duration;  // 缓存滑铲持续时间
	private float _remainingTime;  // 记录滑铲剩余时间

    public override void Enter()
	{
        #if DEBUG
        GD.Print($"{Player.PlayerType} 进入了 SlideTackle 状态");
        #endif
		
		CacheProperties();
		StartCooldown();

		Player.PlayerCollisionShape.Shape.Set(HeightProp, HEIGHT_INCREASE);
		AnimPlayer.Play(SlideTackleState);

		Player.Velocity = Player.Direction * _slideTackleSpeed;
	}

	public override void PhysicsProcess(double delta)
	{
		float currentSpeed = DecelerationAlgorithm();
		Player.Velocity = Player.Direction * currentSpeed;

		_remainingTime -= (float)delta;
		if (_remainingTime > 0) return;

		EmitSignalStateTransition(this, Player.Direction != Vector2.Zero ? RunState : IdleState);
	}

	public override void Exit()
	{
		Player.SlideTackleTimer.Start();
		Player.PlayerCollisionShape.Shape.Set(HeightProp, HEIGHT_RESTORE);
		Player.GameInput.ResetSlideTackle(Player.PlayerType);
	}

	private void OnSlideTackleTimerTimeout()
	{
		StopCooldown();
	}

	// 存储滑铲属性
    private void CacheProperties() 
	{ 
		_slideTackleSpeed = Player.SlideTackleSpeed;
		_duration = Player.SlideTackleDuration;
		_remainingTime = Player.SlideTackleDuration;
	}

    // 启动滑铲冷却
    private void StartCooldown() 
	{ 
		Player.IsSlideTackleAvailable = false;
		Player.SlideTackleTimer.Timeout += OnSlideTackleTimerTimeout;
	}

	// 停止滑铲冷却
	private void StopCooldown() 
	{
        Player.SlideTackleTimer.Stop();
        Player.IsSlideTackleAvailable = true;
		Player.SlideTackleTimer.Timeout -= OnSlideTackleTimerTimeout;

		#if DEBUG
        GD.Print("滑铲冷却结束");
		#endif
    }

	// 平方减速：更有"摩擦刹停"的真实感
    private float DecelerationAlgorithm() 
	{ 
		if (_duration <= 0) return 0;
		float progress = 1.0f - (_remainingTime / _duration);
		return _slideTackleSpeed * (1.0f - progress * progress * 0.8f);
	}
}
