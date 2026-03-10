using static ArcadeFootball.Scripts.Core.StringNames;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine.PlayerStates;

public partial class Recovery : State
{
	public override void Enter()
	{
		#if DEBUG
		GD.Print($"{Player.PlayerType} 进入了 Recovery 状态");
		#endif
		
		Player.RecoveryTimer.Start();
		Player.CanSlideTackle = false;
		Player.RecoveryTimer.Timeout += OnTimeout;
		AnimPlayer.Play(RecoveryState);
	}

	public override void PhysicsProcess(double delta)
	{
		Player.Velocity *= 0.95f;
	}

	public override void Exit()
	{
		Player.CanSlideTackle = true;
		Player.RecoveryTimer.Timeout -= OnTimeout;
	}

	private void OnTimeout()
	{
		Player.RecoveryTimer.Stop();
		EmitSignalStateTransition(this, Player.Direction == Vector2.Zero ? IdleState : RunState);
	}
}
