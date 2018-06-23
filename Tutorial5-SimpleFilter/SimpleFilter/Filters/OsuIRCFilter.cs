using Sync.MessageFilter;
using Sync.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFilter.Filters
{
    /*
     * 过滤器必须实现IFilter接口，而且还要再从ISourceClient/ISourceDanmaku两者选一个实现:
     * ISourceClient 会拦截从Client来的消息，比如osu!irc
     * ISourceDanmaku 会拦截从Source来的消息，比如B站直播间弹幕
     * 
     * FilterPriority可选，优先级越高，越先被调用
     */
    [FilterPriority(Priority =FilterPriority.Normal)]
    class OsuIRCFilter : IFilter, ISourceClient
    {
        const string COMMAND = "?test";

        public void onMsg(ref IMessageBase msg)
        {
            if (msg.Message.RawText.Trim()== COMMAND)
            {
                //标记这个消息已被处理,后面的过滤器将会跳过并取消发送
                msg.Cancel = true;

                IO.CurrentIO.WriteColor($"Message from user {msg.User.RawText}", ConsoleColor.Cyan);
            }
        }
    }
}
