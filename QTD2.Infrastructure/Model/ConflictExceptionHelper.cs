using System;
using System.Collections.Generic;
using System;

namespace QTD2.Infrastructure.Model
{
    public class ConflictExceptionHelper : Exception
    {
        public object ConflictValue { get; }
        public ConflictExceptionHelper(object dataValue) : base()
        {
            ConflictValue = dataValue;
        }
    }
}
