using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Threading;
using Utils.Properties;
using Utils.Common;
using System.Activities.Presentation.Metadata;
using Utils.PathUtils;
using Utils.StringUtils;

namespace Utils
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            AttributeTableBuilder builder = new AttributeTableBuilder();

            addCustomAttributes_PathUtils_categories(builder);
            addCustomAttributes_StringUtils_categoriess(builder);

            MetadataStore.AddAttributeTable(builder.CreateTable());

            //ShowPropertyInOutlineViewAttribute hideFromOutlineAttribute = new ShowPropertyInOutlineViewAttribute() { CurrentPropertyVisible = false, DuplicatedChildNodesVisible = false };
            //builder.AddCustomAttributes(typeof(WithFtpSession), nameof(WithFtpSession.Body), hideFromOutlineAttribute);

        }

        private void addCustomAttributes_PathUtils_categories(AttributeTableBuilder builder)
        {
            // 1.Activitiesペイン上のツリー構造を構築する。
            // ドットで階層を表現する
            {
                builder.AddCustomAttributes(typeof(Combine), new CategoryAttribute(Resources.Category1_PathUtils));
                builder.AddCustomAttributes(typeof(CurrentDir), new CategoryAttribute(Resources.Category1_PathUtils));
                builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), new CategoryAttribute(Resources.Category1_PathUtils));
            }

            // 2.Activitiesペイン上のアクティビティ名
            {
                builder.AddCustomAttributes(typeof(Combine), new DisplayNameAttribute(Resources.DisplayName2_Combine));
                builder.AddCustomAttributes(typeof(CurrentDir), new DisplayNameAttribute(Resources.DisplayName2_CurrentDir));
                builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), new DisplayNameAttribute(Resources.DisplayName2_PathUtils));
            }

            // 3.Activitiesペイン上のアクティビティをポイントしたときに表示されるツールチップ文言
            {
                builder.AddCustomAttributes(typeof(Combine), new DescriptionAttribute(Resources.Description3_Combine));
                builder.AddCustomAttributes(typeof(CurrentDir), new DescriptionAttribute(Resources.Description3_CurrentDir));
                builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), new DescriptionAttribute(Resources.Description3_PathUtils));
            }

            // 4.プロパティペイン内のプロパティの説明文言
            {
                builder.AddCustomAttributes(typeof(Combine), nameof(Combine.PathArray), new DescriptionAttribute(Resources.Description4_Combine_PathArray));
                builder.AddCustomAttributes(typeof(CurrentDir), nameof(CurrentDir.FullPath), new DescriptionAttribute(Resources.Description4_CurrentDir_FullPath));
                {
                    builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), nameof(Utils.PathUtils.PathUtils.Path), new DescriptionAttribute(Resources.Description4_PathUtils_Path));
                    builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), nameof(Utils.PathUtils.PathUtils.FullPath), new DescriptionAttribute(Resources.Description4_PathUtils_FullPath));
                    builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), nameof(Utils.PathUtils.PathUtils.DirectoryName), new DescriptionAttribute(Resources.Description4_PathUtils_DirectoryName));
                    builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), nameof(Utils.PathUtils.PathUtils.DirExists), new DescriptionAttribute(Resources.Description4_PathUtils_DirExists));
                    builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), nameof(Utils.PathUtils.PathUtils.FileExists), new DescriptionAttribute(Resources.Description4_PathUtils_FileExists));
                }

            }

            // 5.プロパティペインの、カテゴリ名
            //{
            //builder.AddCustomAttributes(typeof(ConvertCRLF), nameof(ConvertCRLF.CR), new CategoryAttribute(nameof(Resources.ConvertCRLF_Category)));
            //builder.AddCustomAttributes(typeof(ConvertCRLF), nameof(ConvertCRLF.LF), new CategoryAttribute(nameof(Resources.ConvertCRLF_Category)));
            //}

            // 6.プロパティペイン内のプロパティの変数名
            {
                builder.AddCustomAttributes(typeof(Utils.PathUtils.PathUtils), nameof(Utils.PathUtils.PathUtils.DirectoryName), new DisplayNameAttribute(Resources.DisplayName6_PathUtils_DirectoryName));
            }
        }

        private void addCustomAttributes_StringUtils_categoriess(AttributeTableBuilder builder)
        {

            // 1.Activitiesペイン上のツリー構造を構築する。
            // ドットで階層を表現する
            {
                builder.AddCustomAttributes(typeof(Base64Encode), new CategoryAttribute(Resources.Category1_StringUtils));
                builder.AddCustomAttributes(typeof(Base64Decode), new CategoryAttribute(Resources.Category1_StringUtils));
                builder.AddCustomAttributes(typeof(Base64EncodeFromFile), new CategoryAttribute(Resources.Category1_StringUtils));
                builder.AddCustomAttributes(typeof(ConvertCRLF), new CategoryAttribute(Resources.Category1_StringUtils));
                builder.AddCustomAttributes(typeof(ToJSONString), new CategoryAttribute(Resources.Category1_StringUtils));
            }

            // 2.Activitiesペイン上のアクティビティ名
            {
                builder.AddCustomAttributes(typeof(Base64Encode), new DisplayNameAttribute(Resources.DisplayName2_Base64Encode));
                builder.AddCustomAttributes(typeof(Base64Decode), new DisplayNameAttribute(Resources.DisplayName2_Base64Decode));
                builder.AddCustomAttributes(typeof(Base64EncodeFromFile), new DisplayNameAttribute(Resources.DisplayName2_Base64EncodeFromFile));
                builder.AddCustomAttributes(typeof(ConvertCRLF), new DisplayNameAttribute(Resources.DisplayName2_ConvertCRLF));
                builder.AddCustomAttributes(typeof(ToJSONString), new DisplayNameAttribute(Resources.DisplayName2_ToJSONString));
            }

            // 3.Activitiesペイン上のアクティビティをポイントしたときに表示されるツールチップ文言
            {
                builder.AddCustomAttributes(typeof(Base64Encode), new DescriptionAttribute(Resources.Description3_Base64Encode));
                builder.AddCustomAttributes(typeof(Base64Decode), new DescriptionAttribute(Resources.Description3_Base64Decode));
                builder.AddCustomAttributes(typeof(Base64EncodeFromFile), new DescriptionAttribute(Resources.Description3_Base64EncodeFromFile));
                builder.AddCustomAttributes(typeof(ConvertCRLF), new DescriptionAttribute(Resources.Description3_ConvertCRLF));
                builder.AddCustomAttributes(typeof(ToJSONString), new DescriptionAttribute(Resources.Description3_ToJSONString));

            }

            // 4.プロパティペイン内のプロパティの説明文言
            {
                builder.AddCustomAttributes(typeof(Base64Encode), nameof(Base64Encode.Target), new DescriptionAttribute(Resources.Description4_Base64Encode_Target));
                builder.AddCustomAttributes(typeof(Base64Decode), nameof(Base64Decode.Target), new DescriptionAttribute(Resources.Description4_Base64Decode_Target));
                builder.AddCustomAttributes(typeof(Base64EncodeFromFile), nameof(Base64EncodeFromFile.Path), new DescriptionAttribute(Resources.Description4_Base64EncodeFromFile_Path));
                builder.AddCustomAttributes(typeof(ConvertCRLF), nameof(ConvertCRLF.Target), new DescriptionAttribute(Resources.Description4_ConvertCRLF_Target));
            }

            // 5.プロパティペインの、カテゴリ名
            {
                builder.AddCustomAttributes(typeof(ConvertCRLF), nameof(ConvertCRLF.CR), new CategoryAttribute(Resources.Category5_ConvertCRLF_CRLF));
                builder.AddCustomAttributes(typeof(ConvertCRLF), nameof(ConvertCRLF.LF), new CategoryAttribute(Resources.Category5_ConvertCRLF_CRLF));
            }

            // 6.プロパティペイン内のプロパティの変数名
            { }


        }
    }
}
