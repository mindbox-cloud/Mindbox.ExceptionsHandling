using System;
using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling
{
    public class ClientChangeConflictExceptionCategory : ExceptionCategory
    {
        public ClientChangeConflictExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
        {
        }

        public override string Name => ExceptionCategoryNames.ClientChangeConflict;
        public override LogLevel LogLevel => LogLevel.Error;
    }
}