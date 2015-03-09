using Cirrious.MvvmCross.Plugins.Messenger;

namespace Spotmap.Core.Messages
{
    public class SpotChangedMessage : MvxMessage
    {
        public enum ChangeType
        {
            Added,
            Deleted,
            Updated,
        }

        public SpotChangedMessage(object sender, ChangeType type) : base(sender)
        {
            ChangedType = type;
        }

        public ChangeType ChangedType { get; private set; }
    }
}