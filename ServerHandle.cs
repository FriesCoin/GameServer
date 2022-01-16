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

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            // TODO: send player into game
        }

        public static void addPlayerCards(int _fromClient, Packet _packet){
            int _clientIdCheck = _packet.ReadInt();
            string _ards = _packet.ReadString();
            string[] _cards = _ards.Split(',');
            Server.clients[_fromClient].cards = _cards;
            Console.WriteLine($"{_ards}");
            

        }
        public static void getPlayerCards(int _fromClient, Packet _packet){
            int _clientIdCheck = _packet.ReadInt();
            int _ards = _packet.ReadInt();
            ServerSend.SendPlayerCards(_fromClient,_ards);
            
        }
        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received packet via UDP. Contains message: {_msg}");
        }
    }
}