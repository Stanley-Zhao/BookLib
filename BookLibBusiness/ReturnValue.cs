using System;
using BookLib.Interface;

namespace BookLib.Business
{
    public class ReturnValue<T> : IReturnValue<T>
    {
        public string DetailMessage { get; set; } = "";

        public bool HasError { get; set; } = false;

        public string Message { get; set; } = "";

        public T[] ReturnEntities { get; set; } = null;

        public int StatusCode { get; set; } = BookLib.Common.BookLibStatusCode.STATUS_OK;

        public T ReturnEntity { get; set; } = default(T);

        public bool SingleEntity { get; set; } = true;

        public int AffectEntitiesCount { get; set; } = 0;
    }
}
