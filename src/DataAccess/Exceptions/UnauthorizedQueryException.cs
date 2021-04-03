using System;

namespace HiddenLove.DataAccess.Exceptions
{
    [Serializable]
    public class UnauthorizedQueryException : Exception
    {
        public UnauthorizedQueryException(string queryType, string tableName) 
            : base($"Table \"{tableName}\" does not authorize \"{queryType.ToUpper()}\" queries") 
        { }
            
    }
}