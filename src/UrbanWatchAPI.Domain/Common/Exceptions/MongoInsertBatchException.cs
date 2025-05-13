namespace UrbanWatchAPI.Domain.Common.Exceptions;

public class MongoInsertBatchException(string message, Exception? innerException = null)
    : Exception(message, innerException);