using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Threading;
using System.Activities.Presentation.Metadata;
using Utils.PathUtils;
using Utils.Properties;

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
                builder.AddCustomAttributes(typeof(Combine), new CategoryAttribute(Resources.Category1_Combine));
            }

            // 2.Activitiesペイン上のアクティビティ名
            {
                builder.AddCustomAttributes(typeof(Combine), new DisplayNameAttribute(Resources.DisplayName2_Combine));
            }

            // 3.Activitiesペイン上のアクティビティをポイントしたときに表示されるツールチップ文言
            {
                builder.AddCustomAttributes(typeof(Combine), new DescriptionAttribute(Resources.Description3_Combine));
            }

            // 4.プロパティペイン内のプロパティの説明文言
            {
                builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new DescriptionAttribute(Resources.Description4_Combine_PathArray));
            }

            // 5.プロパティペインの、カテゴリ名
            {
                builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new CategoryAttribute(Resources.Category5_Combine_PathArray));
            }

            // 6.プロパティペイン内のプロパティの変数名
            {
                builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new DisplayNameAttribute(Resources.DisplayName6_Combine_PathArray));
            }
        }
    }
}
