using System;
using System.Linq;
using UnityEngine;

namespace TogglePlayers
{
    public class TogglePlayers : MonoBehaviour
    {
        public static TogglePlayers Instance;
        public static Entities Entities => Entities.Instance;

        public static bool Toggled = false;
        
        public static void Load()
        {
            new GameObject().AddComponent<TogglePlayers>().name = "TogglePlayers";
        }

        private void Awake()
        {
            Instance = this;
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Toggled = !Toggled;
                Chat.Notify(Toggled ? "Players have been hidden." : "Players are no longer being hidden.");
            }
            
            Toggle();
        }

        public static void Toggle()
        {
            foreach (var player in Entities.PlayerList)
            {
                if (player.isMe) continue;
                if (player.IsInGuild && Session.MyPlayerData.Guild != null)
                    if (player.guildID == Session.MyPlayerData.Guild.guildID) continue;
                if (Session.MyPlayerData.IsFriendsWith(player.name)) continue;

                if (player.wrapper.activeSelf && Toggled)
                {
                    player.wrapper.SetActive(false);
                    player.namePlate.gameObject.SetActive(false);
                    if (player.petGO != null) player.petGO.SetActive(false);
                }

                else if (!player.wrapper.activeSelf && !Toggled)
                {
                    player.wrapper.SetActive(true);
                    player.namePlate.gameObject.SetActive(true);
                    if (player.petGO != null) player.petGO.SetActive(true);
                }
            }
        }
    }
}