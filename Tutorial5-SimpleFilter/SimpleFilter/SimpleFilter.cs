using SimpleFilter.Filters;
using Sync.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFilter
{
    public class SimpleFilter : Plugin
    {
        public SimpleFilter() : base("SimpleFilter", "MikiraSora")
        {

        }

        public override void OnEnable()
        {
            base.OnEnable();

            //注册一个过滤器初始事件
            EventBus.BindEvent<PluginEvents.InitFilterEvent>(OnInitFilter);
        }

        private void OnInitFilter(PluginEvents.InitFilterEvent e)
        {
            /*
             * 向过滤管理器添加自己的过滤器，管理器会根据过滤器实现的ISourceClient/ISourceDanmaku接口过滤不同的消息
             */
            e.Filters.AddFilter(new OsuIRCFilter());
        }
    }
}
