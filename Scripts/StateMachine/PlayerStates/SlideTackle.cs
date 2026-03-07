using ArcadeFootball.Scripts.Controllers;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class SlideTackle : State
{
	[Export]
	private float _slideTackleSpeed = 100.0f;
	private float _timer;

	public override void Enter()
	{
		Player.SlideTackleTimer.Timeout += OnSlideTackleTimerTimeout;
		Player.IsSlideTackleAvailable = false;
		Player.PlayerCollisionShape.Shape.Set("height", 18.0f);
		AnimPlayer.Play("SlideTackle");
		_timer = Player.SlideTackleDuration;
		Player.Velocity = Player.Direction * _slideTackleSpeed;

		GD.Print("SlideTackle state entered");
		GD.Print($" 持续{_timer}s");
	}

	public override void PhysicsProcess(double delta)
	{
		// 平方减速：更有"摩擦刹停"的真实感
		float progress = 1.0f - (_timer / Player.SlideTackleDuration);
		float currentSpeed = _slideTackleSpeed * (1.0f - progress * progress * 0.8f);

		Player.Velocity = Player.Direction * currentSpeed;
		_timer -= (float)delta;

		if (_timer > 0) return;
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
		Player.IsSlideTackleAvailable = true;
		Player.SlideTackleTimer.Stop();
		Player.SlideTackleTimer.Timeout -= OnSlideTackleTimerTimeout;
		GD.Print("冷却结束");
	}
}
