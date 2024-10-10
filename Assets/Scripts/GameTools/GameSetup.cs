using UnityEngine;

namespace GameTools
{
    public class GameSetup : MonoBehaviour
    {
        [Header("Time Limit Setup")]
        public float setTimeOnLevel;

        private void Start()
        {
            Coordinator.Instance.RegisterGameSetup(this);
        }
    }
}