using ContentLibrary;
using System.Text;
using Zorro.Core;
using Zorro.Core.Serizalization;

namespace KeypressEvent.Content
{
    internal class KeypressContentEvent : ContentEvent
    {
        public KeypressContentEvent()
        {

        }

        public KeypressContentEvent(string keybindPressed)
        {
            keybind = keybindPressed;
        }
        public override float GetContentValue()
        {
            return 2f;
        }

        public override ushort GetID()
        {
            return ContentHandler.GetEventID(nameof(KeypressContentEvent));
        }

        public override string GetName()
        {
            return "Player pressed a key";
        }

        public override Comment GenerateComment()
        {
            return new Comment(COMMENTS.GetRandom<string>().Replace("<keybind>", keybind));
        }

        public override void Serialize(BinarySerializer serializer)
        {
            serializer.WriteString(keybind, Encoding.UTF8);
        }

        public override void Deserialize(BinaryDeserializer deserializer)
        {
            keybind = deserializer.ReadString(Encoding.UTF8);
        }

        string? keybind;

        public string[] COMMENTS =
        {
        "woah did they just press <keybind>??"
        };
    }
}
