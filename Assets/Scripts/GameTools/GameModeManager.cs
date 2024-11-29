using UnityEngine;

namespace GameTools
{
    public class GameModeManager : MonoBehaviour
    {
        public enum GameMode
        {
            SinglePlayer,
            Multiplayer
        }
        public static GameMode CurrentGameMode { get; set; }
    }
}
