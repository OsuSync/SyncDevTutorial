using Sync.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConfiguration
{
    //必须实现IConfigurable
    public class Setting : IConfigurable
    {
        //配置项必须是ConfigurationElement类型的属性，此类型可和string相互转换
        public ConfigurationElement MyOption { get; set; } = "Mono";

        public ConfigurationElement MyOption2 { get; set; } = "2857";

        //当PluginConfigurationManager.AddItem()钦定此实例时候会读取config.ini的配置文件,加载后会调用此方法
        public void onConfigurationLoad()
        {
            IO.CurrentIO.WriteColor($"[MySimpleConfiguration.Setting] Config is reloaded! Now MyOption is \"{MyOption}\" and MyOption2 is \"{MyOption2}\"", ConsoleColor.Green);
        }

        //当Sync还在运行时，编辑config.ini并保存会调用此方法,此时配置已重新加载完毕
        public void onConfigurationReload()
        {
            IO.CurrentIO.WriteColor($"[MySimpleConfiguration.Setting] Config is reloaded! Now MyOption is \"{MyOption}\" and MyOption2 is \"{MyOption2}\"", ConsoleColor.Green);
        }

        //当Sync即将关闭或者其他原因，配置值保存前会调用此方法
        public void onConfigurationSave()
        {
            IO.CurrentIO.WriteColor("[MySimpleConfiguration.Setting] Config will be saved!", ConsoleColor.Green);
        }
    }
}
