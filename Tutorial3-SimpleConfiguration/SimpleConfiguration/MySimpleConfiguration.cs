using Sync.Plugins;
using Sync.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConfiguration
{
    public class MySimpleConfiguration : Plugin
    {
        public Setting setting { get; set; } = new Setting();

        //new一个插件配置管理器
        private readonly PluginConfigurationManager config_manager;

        private readonly Logger<MySimpleConfiguration> logger = new Logger<MySimpleConfiguration>();

        public MySimpleConfiguration() : base("MySimpleConfiguration","DarkProjector")
        {
            config_manager = new PluginConfigurationManager(this);

            //将IConfigurable实现类实例提交给配置管理器，并立即加载配置给实例
            config_manager.AddItem(setting);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            EventBus.BindEvent<PluginEvents.InitCommandEvent>(e => {
                e.Commands.Dispatch.bind("show_config", args => {
                    logger.LogInfomation($"MyOption is \"{setting.MyOption}\" , MyOption2 is \"{setting.MyOption2}\"");
                    return true;
                }, "Show MySimpleConfiguration configs");
            });
        }
    }
}
