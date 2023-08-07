using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFootball.Messages
{
    internal class GameSavedMessage
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public GameSavedMessage(bool isSuccess, string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
