using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets {
    public class ClientManager : MonoBehaviour
    {
        private List<PlayerTest> playersList = new List<PlayerTest>();
        private PlayerTest _hostPlayer;
        public PlayerTest HostPlayer
        {
            get => _hostPlayer;
            set => _hostPlayer = value;
        }

        private PlayerTest _localPlayer;
        public PlayerTest LocalPlayer
        {
            get => _localPlayer;
            set => _localPlayer = value;
        }

        public static ClientManager inst;
        

        void Awake()
        {
            if (inst == null)
            {
                inst = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddToPlayersList(PlayerTest player)
        {
            playersList.Add(player);
        }

        public PlayerTest GetPlayerByInstrument(Musicians instr)
        {


            for (int i = 0; i < playersList.Count; i++)
            {
                if (playersList[i].instrument == instr)
                {
                    return playersList[i];
                }   
            }
            //var iList = playersList.Where(i => i.instrument == instr).ToList();
            return null;
        }

   
    }
}
