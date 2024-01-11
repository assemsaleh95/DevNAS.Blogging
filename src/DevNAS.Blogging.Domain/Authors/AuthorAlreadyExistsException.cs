using System;
using Volo.Abp;

namespace DevNAS.Blogging.Authors
{
    [Serializable]
    internal class AuthorAlreadyExistsException : BusinessException
    {

        public AuthorAlreadyExistsException(string name)
            : base(BloggingDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }

    }
}