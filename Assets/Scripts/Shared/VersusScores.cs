using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client.Unity;
using DarkRift;
using PartyMobileModels;

public class VersusScores
{
   public static void ReportScoreToServer(float currentScore, string minigameName, UnityClient client) {
        ReportedScore reportedScore = new ReportedScore {
            score = currentScore
        };
        
        using (Message message = Message.Create((ushort)ServerTags.Tag.REPORT_SCORE, reportedScore)) {
            client.SendMessage(message, SendMode.Reliable);
        }
   }
}
