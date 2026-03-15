using Godot;

namespace ArcadeFootball.Scripts.Characters;

public partial class Football : CharacterBody2D
{
    [Export] public float Speed { get; private set; } = 100.0f;
    [Export] public Player CurrentPlayer { get; set; }
    [Export] public AnimatedSprite2D FootballAnimatedSprite { get; private set; }
    [Export] public Area2D FootballArea { get; private set; }
}