using UnityEngine;

namespace PhantomEngine
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Phantom/PlayerData", order = int.MaxValue)]
    public class PlayerData : ScriptableObject
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private float walkValue = 1f;

        public float WalkValue => walkValue;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private float runValue = 2f;
        
        public float RunValue => runValue;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private float jumpValue = 2f;
        
        public float JumpValue => jumpValue;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private float gravityValue = -9.81f;
        
        public float GravityValue => gravityValue;
    }
}