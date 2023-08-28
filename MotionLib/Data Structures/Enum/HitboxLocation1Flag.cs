using System;
namespace MotionLib
{
    [Flags]
    public enum HitboxLocation1Flag : ushort
    {
        PART_1 = 1 << 0,
        LeftHand = 1 << 1,
        RightHand = 1 << 2,
        LeftElbow = 1 << 3,
        RightElbow = 1 << 4,
        PART_6 = 1 << 5,
        RightArm = 1 << 6,
        LeftFoot = 1 << 7,
        LeftArm = 1 << 8,
        PART_10 = 1 << 9,
        LeftKnee = 1 << 10,
        PART_12 = 1 << 11,
        PART_13 = 1 << 12,
        PART_14 = 1 << 13,
        LeftThigh = 1 << 14,
        PART_16 = 1 << 15,
    }
}
