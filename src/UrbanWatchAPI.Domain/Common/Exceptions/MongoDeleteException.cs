namespace UrbanWatchAPI.Domain.Common.Exceptions;

public class MongoDeleteException(string message, Exception? innerException = null)
    : Exception(message, innerException)
{ }