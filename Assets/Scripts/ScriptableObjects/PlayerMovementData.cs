using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Game/PlayerMovementData")]
    public class PlayerMovementData : ScriptableObject
    {
        public float verticalSpeed;
        public float horizontalSpeed;
        public float minX;
        public float maxX;
    }
}