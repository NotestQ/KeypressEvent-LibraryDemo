using ContentLibrary;
using KeypressEvent.Content;
using MyceliumNetworking;
using Steamworks;
using System;
using UnityEngine;

namespace KeypressEvent
{
    internal class KeypressHandler : MonoBehaviour
    {
        void Start()
        {
            MyceliumNetwork.RegisterNetworkObject(this, KeypressEvent.modID);
        }

        void Update()
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    var player = ContentHandler.GetPlayerWithCamera();
                    if (player == null) return;

                    var componentInParent = new KeypressContentProvider(keyCode.ToString());
                    ContentPolling.contentProviders.Add(componentInParent, 1);

                    if (player.IsLocal) return;

                    CSteamID steamID;
                    bool idSuccess = SteamAvatarHandler.TryGetSteamIDForPlayer(player, out steamID);
                    if (idSuccess == false) return;

                    MyceliumNetwork.RPCTarget(KeypressEvent.modID, nameof(RPCHandleEvent), steamID, ReliableType.Reliable, keyCode.ToString());
                }
            }
        }

        [CustomRPC]
        private static void RPCHandleEvent(string eventType)
        {
            var componentInParent = new KeypressContentProvider(eventType);
            ContentPolling.contentProviders.Add(componentInParent, 1);
        }
    }
}
