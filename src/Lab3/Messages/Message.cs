using Itmo.ObjectOrientedProgramming.Lab3.Priorities;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class Message : IMessage
{
    private readonly string _title;

    private readonly IReadOnlyList<string> _body;

    private Message(string title, IReadOnlyList<string> body, PriorityLevel priorityLevel)
    {
        Id = Guid.NewGuid();
        _title = title;
        _body = body;
        PriorityLevel = priorityLevel;
    }

    public PriorityLevel PriorityLevel { get;  }

    public Guid Id { get; }

    public static MessageBuilder Builder()
    {
        return new MessageBuilder();
    }

    public string Render()
    {
        var builder = new StringBuilder();

        builder.Append("\n");
        builder.Append(_title);
        builder.Append("\n");
        builder.AppendJoin("\n", _body.Select(part => part));

        return builder.ToString();
    }

    public class MessageBuilder
    {
        private readonly List<string> _body = [];

        private string? _title;

        private PriorityLevel? _priority;

        public MessageBuilder SetTitle(string title)
        {
            _title = title;

            return this;
        }

        public MessageBuilder AddPartToBody(string text)
        {
            _body.Add(text);

            return this;
        }

        public MessageBuilder SetPriority(PriorityLevel priorityLevel)
        {
            _priority = priorityLevel;

            return this;
        }

        public Message Build()
        {
            return new Message(
                _title ?? throw new ArgumentNullException(),
                _body,
                _priority ?? throw new ArgumentNullException());
        }
    }
}