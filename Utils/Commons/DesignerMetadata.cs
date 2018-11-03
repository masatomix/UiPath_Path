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
            addCustomAttributes_PathUtils_categories(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        private void addCustomAttributes_PathUtils_categories(AttributeTableBuilder builder)
        {
            // 1.Activitiesペイン上のツリー構造を構築する。
            {
                string PathUtils_categories = "ツリー1.ツリー2.Utilities";
                builder.AddCustomAttributes(typeof(Combine), new CategoryAttribute(PathUtils_categories));
            }

            // 2.Activitiesペイン上のアクティビティ名
            {
                builder.AddCustomAttributes(typeof(Combine), new DisplayNameAttribute("パス連結アクティビティ"));
            }

            // 3.Activitiesペイン上のアクティビティをポイントしたときに表示されるツールチップ文言
            {
                builder.AddCustomAttributes(typeof(Combine), new DescriptionAttribute("ツールチップに説明文を書こう"));
            }

            // 4.プロパティペイン内のプロパティの説明文言
            {
                builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new DescriptionAttribute("このプロパティの説明を書く"));
            }

            // 5.プロパティペインの、カテゴリ名
            {
                builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new CategoryAttribute("インプット"));
            }

            // 6.プロパティペイン内のプロパティの変数名
            {
                builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new DisplayNameAttribute("パス配列"));
            }
        }
    }
}
