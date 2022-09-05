using System;
using System.Collections.Generic;
using DarkRift;
using DarkRift.Server;
using PartyMobileServer.Models;
using PartyMobileModels;

namespace PartyMobileServer
{
    public class PartyMobileServer : Plugin
    {
        public override bool ThreadSafe => false;

        public override Version Version => new Version(1, 0, 0);
        public Dictionary<int, User> playersInGame = new Dictionary<int, User>();

        public PartyMobileServer(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            ClientManager.ClientConnected += OnClientConnected;
            ClientManager.ClientDisconnected += OnClientDisconnected;
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Console.WriteLine("Player Connected");
            e.Client.MessageReceived += OnMessageRecieved;

            if (!playersInGame.ContainsKey(e.Client.ID))
            {
                User user = new User
                {
                    Client = e.Client
                };
                playersInGame.Add(e.Client.ID, user);
                SendPlayerConnectedMessage(e.Client);
            }
        }
        private void SendPlayerConnectedMessage(IClient client)
        {
            User newUser = playersInGame[client.ID];
            Player newPlayer = new Player() { username = newUser.Username ?? $"Player {client.ID}" };
            using (Message message = Message.Create((ushort)ServerTags.Tag.NEW_PLAYER_CONNECTED, newPlayer))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }

        private void OnClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            if (playersInGame.ContainsKey(e.Client.ID))
            {
                playersInGame.Remove(e.Client.ID);
            }

            Console.WriteLine("Player Disconnected");
        }

        private void OnMessageRecieved(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage()) 
            {
                using (DarkRiftReader reader = message.GetReader())
                {
                    switch (message.Tag)
                    {
                        case (ushort)ServerTags.Tag.USER_LOGIN:
                            if (playersInGame.ContainsKey(e.Client.ID))
                            {
                                Player userLogin = reader.ReadSerializable<Player>();
                                Console.WriteLine($"{userLogin.username} has connected");
                                User user = playersInGame[e.Client.ID];
                                user.Username = userLogin.username;

                                SendPlayerConnectedMessage(e.Client);
                            }
                            break;
                        case (ushort)ServerTags.Tag.REPORT_SCORE:
                            if (playersInGame.ContainsKey(e.Client.ID))
                            {
                                ReportedScore userScore = reader.ReadSerializable<ReportedScore>();
                                User user = playersInGame[e.Client.ID];

                                Console.WriteLine($"{user.Client.ID} has scored {userScore.score}");
                            }
                            break;
                    }
                }
            }
        }
    }
}
