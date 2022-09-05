using UnityEngine;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using PartyMobileModels;
using UnityEngine.UI;

public class VersusGameManager : GameManager
{
    public UnityClient playerClient;
    private const string PLAYERLIST_TAG = "PlayerList";

    public override void EndGame(float score, MiniGame minigame) {
        this.minigame = minigame;
        VersusScores.ReportScoreToServer(score, minigame.Name, playerClient);

        SceneManager.LoadScene(SceneManager.HighScoreMenu);
    }

    public void Awake() {
        playerClient.MessageReceived += OnMessageRecieved;
    }
    public void OnDestroy() {
        playerClient.MessageReceived -= OnMessageRecieved;
    }

    private void OnMessageRecieved(object sender, MessageReceivedEventArgs e) {
        using (Message message = e.GetMessage()) 
        {
            using (DarkRiftReader reader = message.GetReader())
            {
                switch (message.Tag)
                {
                    case (ushort)ServerTags.Tag.NEW_PLAYER_CONNECTED:
                        Player newPlayer = reader.ReadSerializable<Player>();
                        GameObject playerList = GameObject.FindWithTag(PLAYERLIST_TAG);
                        if (playerList != null) {
                            playerList.GetComponent<LoadPlayers>().AddPlayerToList(newPlayer.username);
                        }
                        Debug.Log(newPlayer.username);
                        break;
                }
            }
        }
    }
}
