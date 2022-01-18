using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
            if(_clientIdCheck==5){
                ServerSend.SendAnotherServer(_fromClient);
                return;
            }
            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_username}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            if(_fromClient != 1){ServerSend.SendPlayerList(Database.Players,_fromClient);};
            Database.Players.Add(new Player(_fromClient,_username));
            ServerSend.SendPlayerName(_fromClient,_username);

            // TODO: send player into game
        }
        public static void cardTrowed(int _fromClient, Packet _packet){
            int card = _packet.ReadInt();
            if(Database.whoTurns != 4){
                Database.whoTurns+=1;
            }else{
                Database.whoTurns-=1;
            }
            ServerSend.SendCard(_fromClient,card);
            Database.cardsPlayed.Add(card);
            Console.WriteLine($"Player trowed card {card} ");
        }
        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received packet via UDP. Contains message: {_msg}");
        }
    }
}