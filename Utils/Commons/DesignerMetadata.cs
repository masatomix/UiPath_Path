using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Threading;
using System.Activities.Presentation.Metadata;
using Utils.PathUtils;

namespace Utils
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            AttributeTableBuilder builder = new AttributeTableBuilder();

            string PathUtils_categories = "Utils.Path Utilities";

            // Activitiesペイン上のツリー構造を構築する。
            // ドットで階層を表現する
            builder.AddCustomAttributes(typeof(Combine), new CategoryAttribute(PathUtils_categories));

            // Activitiesペイン上のアクティビティをポイントしたときに表示されるTool Tips
            builder.AddCustomAttributes(typeof(Combine), new DescriptionAttribute("Combines an array of strings into a path."));

            // プロパティペイン内のプロパティの説明
            builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new DescriptionAttribute("test desc"));  //クラスのプロパティに直接 Desc書いたのと意味おなじ。


            // プロパティペインの、カテゴリ名
            builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new CategoryAttribute("test category"));  //クラスのプロパティに直接 Categoryを書いたのと意味おなじ。


            // Activitiesペイン上のアクティビティ名
            builder.AddCustomAttributes(typeof(Combine), new DisplayNameAttribute("test display"));  //クラスに直接 DisplayNameを書いたのと意味おなじ。

            // プロパティペイン内のプロパティの変数名
            builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new DisplayNameAttribute("test display1"));  //クラスのプロパティに直接 DisplayNameを書いたのと意味おなじ。

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
