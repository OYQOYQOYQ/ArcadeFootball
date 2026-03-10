using Godot;

namespace ArcadeFootball.Scripts.Core;

public static class StringNames
{
    public static class P1
    {
        public static readonly StringName Left = "p1_left";
        public static readonly StringName Right = "p1_right";
        public static readonly StringName Up = "p1_up";
        public static readonly StringName Down = "p1_down";
        public static readonly StringName SlideTackle = "p1_slide_tackle";
    }

    public static class P2
    {
        public static readonly StringName Left = "p2_left";
        public static readonly StringName Right = "p2_right";
        public static readonly StringName Up = "p2_up";
        public static readonly StringName Down = "p2_down";
        public static readonly StringName SlideTackle = "p2_slide_tackle";
    }

    public static readonly StringName HeightProp = "height";

    public static readonly StringName IdleState = "Idle";
    public static readonly StringName RunState = "Run";
    public static readonly StringName SlideTackleState = "SlideTackle";
    public static readonly StringName RecoveryState = "Recovery";
}