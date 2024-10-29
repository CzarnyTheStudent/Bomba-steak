using UnityEngine;

[CreateAssetMenu(fileName = "GameMusicSettings", menuName = "Music/GameMusicSettings")]
public class GameMusicSettings : ScriptableObject
{
    [System.Serializable]
    public class LevelMusic
    {
        public string levelName;     
        public AudioClip startLevelMusic; 
        public AudioClip gameMusic; 
        public AudioClip transitionMusic; 
    }

    public LevelMusic[] levelsMusic;
}
