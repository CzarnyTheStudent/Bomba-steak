using TMPro;
using UnityEngine;

public class NickNameGeneration : MonoBehaviour
{
    private void Awake()
    {
        var playerData = FindObjectOfType<PlayerData>();
        var nickNameInputField = GetComponentInChildren<TextMeshProUGUI>();
        if (!playerData)
        {
            nickNameInputField.text = PlayerData.GetRandomNickName();
        }
        else if (string.IsNullOrWhiteSpace(playerData.GetNickName()))
        {
            nickNameInputField.text = PlayerData.GetRandomNickName();
        }
        else
        {
            if (!playerData) return;
            nickNameInputField.text = playerData.GetNickName();
        }
        
    }
}
