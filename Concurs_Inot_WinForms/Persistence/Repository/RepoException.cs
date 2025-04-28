using System;

namespace Persistence.Repository
{

    public class RepoException : Exception
    {
        public RepoException() { }

        public RepoException(string message) : base(message) { }

        public RepoException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}