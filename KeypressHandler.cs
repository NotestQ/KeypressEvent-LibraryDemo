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
                    ContentHandler.PollAndReplicateProvider(new KeypressContentProvider(), 400, keyCode.ToString());
                }
            }
        }
    }
}
