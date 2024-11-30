using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotionLibrary
{
    public enum OEPropertyType : byte
    {
        [Description("Actor Audio")]
        VoiceAudio = 1,
        [Description("Effect Audio")]
        SEAudio = 2,
        [Description("Follow Up Window")]
        FollowupWindow = 3,
        [Description("Control Window")]
        ControlLock = 4,
        [Description("Hitbox")]
        Hitbox = 5,
        [Description("Self-Damage")]
        SelfContainedHitbox = 10,
        [Description("Camera Shake")]
        CameraShake = 18,
        [Description("Invincibility")]
        Muteki = 30
    }
}
