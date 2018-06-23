using Sync.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePlugin
{
    public class MySimpleEvent : IPluginEvent
    {
        public MySimpleEvent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
