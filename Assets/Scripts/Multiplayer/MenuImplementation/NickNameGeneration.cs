using TMPro;
using UnityEngine;

public class NickNameGeneration : MonoBehaviour
{
    private void Awake()
    {
        var nickNameInputField = GetComponentInChildren<TextMeshProUGUI>();
        nickNameInputField.text = PlayerData.GetRandomNickName();
    }
}
