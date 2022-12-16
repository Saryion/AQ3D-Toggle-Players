using System;
using System.Linq;
using UnityEngine;

namespace TogglePlayers
{
    public class TogglePlayers : MonoBehaviour
    {
        public static TogglePlayers Instance;
        public static Entities Entities => Entities.Instance;
        public static Entity Me => Entities.me;

        public static bool Toggled;
        
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
            }
            
            if (Toggled)
            {
                Toggle();
            }
            else
            {
                UnToggle();
            }
        }

        public static void Toggle()
        {
            var players = Entities.PlayerList;

            foreach (var player in players)
            {
                if (player.isMe) continue;
                if (player.IsInGuild && Session.MyPlayerData.Guild != null)
                    if (player.guildID == Session.MyPlayerData.Guild.guildID) continue;
                if (Session.MyPlayerData.IsFriendsWith(player.name)) continue;

                if (player.wrapper.activeSelf)
                {
                    player.wrapper.SetActive(false);
                    player.namePlate.gameObject.SetActive(false);
                    if (player.petGO != null) player.petGO.SetActive(false);
                }
            }
        }
        
        public static void UnToggle()
        {
            var players = Entities.PlayerList;

            foreach (var player in players)
            {
                if (player.isMe) continue;
                if (player.IsInGuild && Session.MyPlayerData.Guild != null)
                    if (player.guildID == Session.MyPlayerData.Guild.guildID) continue;
                if (Session.MyPlayerData.IsFriendsWith(player.name)) continue;

                if (!player.wrapper.activeSelf)
                {
                    player.wrapper.SetActive(true);
                    player.namePlate.gameObject.SetActive(true);
                    if (player.petGO != null) player.petGO.SetActive(true);
                }
            }
        }
    }
}