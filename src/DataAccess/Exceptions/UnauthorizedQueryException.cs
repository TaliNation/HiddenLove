using System;

namespace HiddenLove.DataAccess.Exceptions
{
    /// <summary>
    /// Exception lorsqu'un requête non explicitement déclarée dans le Repository tente d'être exécutée
    /// </summary>
    [Serializable]
    public class UnauthorizedQueryException : Exception
    {
        public UnauthorizedQueryException(string queryType, string tableName) 
            : base($"Table \"{tableName}\" does not authorize \"{queryType.ToUpper()}\" queries") 
        { }
            
    }
}