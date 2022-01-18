using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }
        public static void SendPlayerList(List<Player> playerList,int _toClient){
            using (Packet _packet = new Packet((int)ServerPackets.PlayerInRoom))
            {
                _packet.Write(playerList.ToArray());
                SendTCPData(_toClient, _packet);
            }
        }
        public static void SendAnotherServer(int _toClient){
            using (Packet _packet = new Packet((int)ServerPackets.serverFull))
            {
                _packet.Write("tryAnotherServer");
                SendTCPData(_toClient, _packet);
            }
        }
        public static void SendWhoTurn(){
            using (Packet _packet = new Packet((int)ServerPackets.WhoTurns))
            {
                _packet.Write(Database.whoTurns);

                SendUDPDataToAll(_packet);
            }
        }
        public static void SendPlayerName(int _fromClient, string playerName){
            using (Packet _packet = new Packet((int)ServerPackets.PlayerEnter))
            {
                _packet.Write(_fromClient);
                _packet.Write(playerName);

                SendTCPDataToAll(_fromClient, _packet);
            }
        }
        public static void SendCard(int _fromClient, int cardType){
            using (Packet _packet = new Packet((int)ServerPackets.cardTrowed))
            {
                _packet.Write(cardType);

                SendTCPDataToAll(_fromClient, _packet);
            }
        }
        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void UDPTest(int _toClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.udpTest))
            {
                _packet.Write("A test packet for UDP.");

                SendUDPData(_toClient, _packet);
            }
        }
        #endregion
    }
}