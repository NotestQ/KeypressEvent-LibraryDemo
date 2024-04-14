using System.Collections.Generic;
using UnityEngine;

namespace KeypressEvent.Content
{
    internal class KeypressContentProvider : ContentProvider
    {
        public KeypressContentProvider()
        {

        }

        public KeypressContentProvider(string type)
        {
            keybindPressed = type;
        }

        public override void GetContent(List<ContentEventFrame> contentEvents, float seenAmount, Camera camera, float time)
        {
            contentEvents.Add(new ContentEventFrame(new KeypressContentEvent(keybindPressed!), seenAmount, time));
            return;
        }

        string? keybindPressed;
    }
}
