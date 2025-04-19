using UnityEngine;

namespace GameDesign
{
    public class PlayerReusableData
    {
        public float PlayerAtk { get; set; } = 1f;
        public float PlayerMaxHealth { get; set; } = 100f;
        public float PlayerCurHealth { get; set; } = 100f;
        public float PlayerMoveSpeed { get; set; } = 1.0f;
        public bool WeaponShow {  get; set; } = false;
        public bool UseMouse { get; set; } = false;


        public bool OnJumping { get; set; } = false;
        public bool OnAtk { get; set; } = false;
        public int AtkCount { get; set; } = 0;





    }
}
