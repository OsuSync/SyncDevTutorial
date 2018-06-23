using Sync.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleI18n
{
    //装载I18n元素的类必须实现I18nProvider
    class Language : I18nProvider
    {
        private Language() { }

        //要装载的文本变量必须是LanguageElement类型的,其值相当于缺省文本
        public LanguageElement LANG_CommandDescription = "这是一个命令";
        
        public LanguageElement LANG_Text = "这是一个文本";

        public static Language Instance { get; set; } = new Language();
    }
}
