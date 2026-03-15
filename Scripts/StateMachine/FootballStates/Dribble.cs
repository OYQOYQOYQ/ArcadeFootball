using ArcadeFootball.Scripts.Characters;
using static ArcadeFootball.Scripts.Core.StringNames;
using Godot;

namespace ArcadeFootball.Scripts.StateMachine.FootballStates;

public partial class Dribble : State
{
    private float _followSpeed = 200;
    private Vector2 _targetPos;
    private Vector2 _direction;
    private Vector2 _lastDirection;
    private bool _playerIsBelow;

    public override void Enter()
    {
        Football.CurrentPlayer.IsHoldFootball = true;
        DribbleDirection();
    }

    public override void PhysicsProcess(double delta)
    {
        if (Football.CurrentPlayer.Direction == Vector2.Zero)
        {
            Football.FootballAnimatedSprite.Stop();
        }
        else
        {
            DribbleDirection();
        }

        // 平滑移动足球到球员脚下
        _targetPos = Football.CurrentPlayer.DribblePoint.GlobalPosition;
        Football.GlobalPosition = Football.GlobalPosition.MoveToward(_targetPos, _followSpeed * (float)delta);

        bool ballInHeadArea = IsBallInAnyHeadArea();
        if (! ballInHeadArea)
        {
            float distance = Football.GlobalPosition.DistanceTo(_targetPos);
            Football.FootballAnimatedSprite.ZIndex = distance > 5f ? 1 : 0;
        }
        else
        {
            Football.FootballAnimatedSprite.ZIndex = 0;
        }
    }

    private void DribbleDirection()
    {
        if (Football.CurrentPlayer.PlayerSprite.FlipH)
        {
            Football.FootballAnimatedSprite.Play(LeftDribble);
        }
        else
        {
            Football.FootballAnimatedSprite.Play(RightDribble);
        }
    }
    
    private bool IsBallInAnyHeadArea()
    {
        var players = Football.GetTree().GetNodesInGroup("Players");
        foreach (var node in players)
        {
            if (node is not Player player) continue;
            if (player.HeadArea.OverlapsArea(Football.FootballArea)) return true;
        }
        return false;
    }
}