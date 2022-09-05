using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadPlayers : MonoBehaviour
{
    public GameObject player_prefab;

    public void AddPlayerToList(string username) {
        GameObject new_player_object = (GameObject)Instantiate(player_prefab, transform);
        new_player_object.GetComponentInChildren<TextMeshProUGUI>().text = username;
    }
}
