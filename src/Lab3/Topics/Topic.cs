using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipient;

namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public class Topic : ITopic
{
    private readonly List<IRecipient> _recipients;

    public string Name { get; }

    public IMessage Message { get; }

    private Topic(string name, IList<IRecipient> recipients, IMessage message)
    {
        Name = name;
        _recipients = recipients.ToList();
        Message = message;
    }

    public static TopicBuilder Builder()
    {
        return new TopicBuilder();
    }

    public TopicDeliverResult SendToRecipient()
    {
        bool wasDelivered = false;
        foreach (IRecipient recipient in _recipients)
        {
            if (Message.PriorityLevel.PriorityNumber >= recipient.PriorityLevel.PriorityNumber)
            {
                RecipientDeliverResult currentResult = recipient.Send(Message);
                if (currentResult is RecipientDeliverResult.SuccessDelivered)
                {
                    wasDelivered = true;
                }
            }
        }

        return wasDelivered ?
            new TopicDeliverResult.SuccessDelivered() :
            new TopicDeliverResult.NoOneGetMessage();
    }

    public class TopicBuilder
    {
        private readonly List<IRecipient> _recipients = [];
        private string? _name;
        private IMessage? _message;

        public TopicBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public TopicBuilder AddRecipient(IRecipient recipient)
        {
            _recipients.Add(recipient);
            return this;
        }

        public TopicBuilder AddMessageToSend(IMessage message)
        {
            _message = message;
            return this;
        }

        public ITopic Build()
        {
            return new Topic(
                _name ?? throw new ArgumentNullException(),
                _recipients ?? throw new ArgumentNullException(),
                _message ?? throw new ArgumentNullException());
        }
    }
}