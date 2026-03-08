using ArcadeFootball.Scripts.Characters;
using ArcadeFootball.Scripts.Controllers;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class SlideTackle : State
{
	private float _slideTackleSpeed;  // 缓存滑铲速度
    private float _duration;  // 缓存滑铲持续时间
	private float _remainingTime;  // 记录滑铲剩余时间

    public override void Enter()
	{
		CacheProperties();
		StartCooldown();

		Player.PlayerCollisionShape.Shape.Set("height", 18.0f);
		AnimPlayer.Play("SlideTackle");

		Player.Velocity = Player.Direction * _slideTackleSpeed;
	}

	public override void PhysicsProcess(double delta)
	{
		float currentSpeed = DecelerationAlgorithm();
		Player.Velocity = Player.Direction * currentSpeed;

		_remainingTime -= (float)delta;
		if (_remainingTime > 0) return;

		if (Player.Direction != Vector2.Zero)
			EmitSignalStateTransition(this, "Run");
		else
			EmitSignalStateTransition(this, "Idle");
	}

	public override void Exit()
	{
		Player.SlideTackleTimer.Start();
		Player.PlayerCollisionShape.Shape.Set("height", 15.0f);
		InputController.Instance.ResetSlideTackle(Player.PlayerType);
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
        GD.Print("滑铲冷却结束");
    }

	// 平方减速：更有"摩擦刹停"的真实感
    private float DecelerationAlgorithm() 
	{ 
		float progress = 1.0f - (_remainingTime / _duration);
		return _slideTackleSpeed * (1.0f - progress * progress * 0.8f);
	}
}
