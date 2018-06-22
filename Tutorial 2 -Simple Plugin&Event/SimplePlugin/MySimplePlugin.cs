using Sync.Command;
using Sync.Plugins;
using Sync.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePlugin
{
    //插件类必须继承Plugin
    public class MySimplePlugin : Plugin
    {
        Logger<MySimplePlugin> logger = new Logger<MySimplePlugin>();

        //构造函数必须为空，然后按着作者名和插件名调用父类构造
        public MySimplePlugin() : base("MySimplePlugin", "MikiraSora")
        {

        }

        public override void OnEnable()
        {
            base.OnEnable();

            //绑定各种事件并注册各种功能,详情 https://github.com/OsuSync/Sync/blob/master/Sync/Plugins/PluginManager.cs#L23-L135
            EventBus.BindEvent<PluginEvents.InitPluginEvent>(OnPluginInit);

            //注册命令
            EventBus.BindEvent<PluginEvents.InitCommandEvent>(OnCommandInit);
            
            EventBus.BindEvent<PluginEvents.LoadCompleteEvent>(OnAllPluginLoadedFinish);

            //绑定自定义事件
            EventBus.BindEvent<MySimpleEvent>(OnMySimpleEvent);
        }

        private void OnPluginInit(PluginEvents.InitPluginEvent e)
        {
            logger.LogInfomation("MySimplePlugin初始化~");
        }

        private void OnAllPluginLoadedFinish(PluginEvents.LoadCompleteEvent e)
        {
            logger.LogInfomation("Sync插件已经全部加载完毕，这里可以引用其他插件来进行操作");
        }

        private void OnCommandInit(PluginEvents.InitCommandEvent e)
        {
            //绑定一个命令
            e.Commands.Dispatch.bind("mhnb",HandleCommands, "这是一个命令");
        }

        private bool HandleCommands(Arguments args)
        {
            foreach (string param in args)
            {
                logger.LogInfomation("param:"+param);
            }

            //发送MySimpleEvent事件，将调用各个绑定到此事件的回调
            EventBus.RaiseEvent/*EventBus.RaiseEventAsync*/(new MySimpleEvent("mhnbbbbbbbbbbbbbbb"));

            return true;
        }

        private void OnMySimpleEvent(MySimpleEvent e)
        {
            logger.LogInfomation(e.Message);
        }
    }
}
