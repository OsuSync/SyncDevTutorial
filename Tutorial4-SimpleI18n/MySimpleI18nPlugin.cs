using Sync.Plugins;
using Sync.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleI18n
{
    public class MySimpleI18nPlugin : Plugin
    {
        public MySimpleI18nPlugin() : base("MySimpleI18nPlugin", "MikiraSora")
        {
            /*
             * 加载I18n文本文件,会将加载Language\{区域代码}\{I18n类所属的命名空间}.{I18n类名}.lang
             * 比如当前Sync钦定en-US语言环境，那么Sync将会加载Language\en-US\SimpleI18n.Language.lang的内容并覆盖LanguageElement同名字段
             * 如果不存在的话会将生成一个默认内容的文件
             */
            I18n.Instance.ApplyLanguage(Language.Instance);
        }

        public override void OnEnable()
        {
            base.OnEnable();

            EventBus.BindEvent<PluginEvents.InitCommandEvent>(e => e.Commands.Dispatch.bind("show_text",arg => {
                IO.CurrentIO.WriteColor(Language.Instance.LANG_Text, ConsoleColor.Green);
                return true;
            }, Language.Instance.LANG_CommandDescription));
        }
    }
}
