using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers.FileServices;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers.LogLevels;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger;
using Itmo.ObjectOrientedProgramming.Lab3.Priorities;
using Itmo.ObjectOrientedProgramming.Lab3.Recipient;
using Itmo.ObjectOrientedProgramming.Lab3.Topics;
using Itmo.ObjectOrientedProgramming.Lab3.Users;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class MyTests
{
    [Fact]

    public void UserShouldGetMessageWithUnreadStatus_WhenUserGetsNewMessage_ShouldReturnSuccess()
    {
        var user = new User("Vlad");
        Message.MessageBuilder messageBuilder = Message.Builder();
        IMessage message = messageBuilder.SetTitle("OOP").AddPartToBody("Patterns").SetPriority(new PriorityLevel.High()).Build();
        Topic.TopicBuilder topicBuilder = Topic.Builder();
        var recipientSimple = new UserRecipient(user, new PriorityLevel.Low());
        var fileService = new FileService();
        var logger = new Logger(fileService);
        logger.ClearLogs();
        var recipientLogger = new RecipientLogger(logger, recipientSimple);
        ITopic topic = topicBuilder.SetName("Lecture").AddMessageToSend(message).AddRecipient(recipientLogger).Build();
        var topicLogger = new TopicLogger(logger, topic);
        topicLogger.SendToRecipient();

        bool expectedResult = false;
        bool actualResult = user.Messages[0].IsRead;

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]

    public void ShouldChangeReadStatus_WhenUserTryToReadUnreadMessage_ShouldReturnSuccess()
    {
        var user = new User("Vlad");
        Message.MessageBuilder messageBuilder = Message.Builder();
        IMessage message = messageBuilder.SetTitle("OOP").AddPartToBody("Patterns").SetPriority(new PriorityLevel.High()).Build();
        Topic.TopicBuilder topicBuilder = Topic.Builder();
        var recipientSimple = new UserRecipient(user, new PriorityLevel.Low());
        var fileService = new FileService();
        var logger = new Logger(fileService);
        var recipientLogger = new RecipientLogger(logger, recipientSimple);
        ITopic topic = topicBuilder.SetName("Lecture").AddMessageToSend(message).AddRecipient(recipientLogger).Build();
        var topicLogger = new TopicLogger(logger, topic);
        topicLogger.SendToRecipient();
        user.TryMarkAsRead(message.Id);

        bool expectedResult = true;
        bool actualResult = user.Messages[0].IsRead;

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]

    public void ShouldReturnMessageIsAlreadyRead_WhenUserTryToReadUReadMessage_ShouldReturnSuccess()
    {
        var user = new User("Vlad");
        Message.MessageBuilder messageBuilder = Message.Builder();
        IMessage message = messageBuilder.SetTitle("OOP").AddPartToBody("Patterns").SetPriority(new PriorityLevel.High()).Build();
        Topic.TopicBuilder topicBuilder = Topic.Builder();
        var recipientSimple = new UserRecipient(user, new PriorityLevel.Low());
        var fileService = new FileService();
        var logger = new Logger(fileService);
        logger.ClearLogs();
        var recipientLogger = new RecipientLogger(logger, recipientSimple);
        ITopic topic = topicBuilder.SetName("Lecture").AddMessageToSend(message).AddRecipient(recipientLogger).Build();
        var topicLogger = new TopicLogger(logger, topic);
        topicLogger.SendToRecipient();
        user.TryMarkAsRead(message.Id);

        var expectedResult = new ReadMessageResult.MessageIsAlreadyRead();
        ReadMessageResult actualResult = user.TryMarkAsRead(message.Id);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]

    public void ShouldReturnThatMessageIsNotDeliveredToRecipient_WhenRecipientHasHigherPriority_ShouldReturnSuccess()
    {
        var user = new User("Vlad");
        Message.MessageBuilder messageBuilder = Message.Builder();
        IMessage message = messageBuilder.SetTitle("OOP").AddPartToBody("Patterns").SetPriority(new PriorityLevel.High()).Build();
        Topic.TopicBuilder topicBuilder = Topic.Builder();

        var mockRecipient = new Mock<IRecipient>();
        mockRecipient.Setup(r => r.Send(It.IsAny<IMessage>())).Verifiable();

        var recipientSimple = new UserRecipient(user, new PriorityLevel.Max());
        var fileService = new FileService();
        var logger = new Logger(fileService);
        logger.ClearLogs();
        var recipientLogger = new RecipientLogger(logger, recipientSimple);
        ITopic topic = topicBuilder.SetName("Lecture").AddMessageToSend(message).AddRecipient(recipientLogger).Build();
        var topicLogger = new TopicLogger(logger, topic);
        topicLogger.SendToRecipient();

        mockRecipient.Verify(r => r.Send(It.IsAny<IMessage>()), Times.Never());
    }

    [Fact]

    public void ShouldWriteTwoLogs_WhenLoggingRecipientSendMessage_ShouldReturnSuccess()
    {
        var user = new User("Vlad");
        Message.MessageBuilder messageBuilder = Message.Builder();
        IMessage message = messageBuilder.SetTitle("OOP").AddPartToBody("Patterns").SetPriority(new PriorityLevel.High()).Build();
        Topic.TopicBuilder topicBuilder = Topic.Builder();

        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(r => r.Log(It.IsAny<string>(), LogLevel.Info, null)).Verifiable();

        var recipientSimple = new UserRecipient(user, new PriorityLevel.Low());
        var fileService = new FileService();
        var logger = new Logger(fileService);
        logger.ClearLogs();
        var recipientLogger = new RecipientLogger(mockLogger.Object, recipientSimple);
        ITopic topic = topicBuilder.SetName("Lecture").AddMessageToSend(message).AddRecipient(recipientLogger).Build();
        var topicLogger = new TopicLogger(mockLogger.Object, topic);
        topicLogger.SendToRecipient();

        mockLogger.Verify(r => r.Log(It.IsAny<string>(), LogLevel.Info, null), Times.Exactly(2));
    }

    [Fact]

    public void ShouldShowTextFromMessenger_WhenMessengerGetsMessage_ShouldReturnSuccess()
    {
        var mockMessenger = new Mock<IMessenger>();
        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        Message.MessageBuilder messageBuilder = Message.Builder();
        Message message = messageBuilder.SetTitle("OOP").AddPartToBody("Patterns").SetPriority(new PriorityLevel.High()).Build();
        Topic.TopicBuilder topicBuilder = Topic.Builder();

        mockMessenger.Setup(r => r.ShowMessage(It.IsAny<string>())).Verifiable();

        var recipientSimple = new MessengerRecipient(mockMessenger.Object, new PriorityLevel.Low());
        ITopic topic = topicBuilder.SetName("Lecture").AddMessageToSend(message).AddRecipient(recipientSimple).Build();
        topic.SendToRecipient();

        mockMessenger.Verify(r => r.ShowMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]

    public void ShouldGetOneMessage_WhenAddsTwoRecipientsWithSameUser_ShouldReturnSuccess()
    {
        var user = new User("Vlad");
        Message.MessageBuilder messageBuilder = Message.Builder();
        IMessage message = messageBuilder.SetTitle("OOP").AddPartToBody("Patterns").SetPriority(new PriorityLevel.High()).Build();
        Topic.TopicBuilder topicBuilder = Topic.Builder();
        var recipientSimple = new UserRecipient(user, new PriorityLevel.Low());
        var recipientHigh = new UserRecipient(user, new PriorityLevel.Max());
        ITopic topic = topicBuilder.SetName("Lecture").AddMessageToSend(message).AddRecipient(recipientSimple).AddRecipient(recipientHigh).Build();
        topic.SendToRecipient();

        int expectedResult = 1;
        int actualResult = user.Messages.Count;

        Assert.Equal(expectedResult, actualResult);
    }
}