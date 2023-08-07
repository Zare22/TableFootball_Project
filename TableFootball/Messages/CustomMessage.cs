using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFootball.Messages
{
    internal class CustomMessage
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public CustomMessage(bool isSuccess, string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
