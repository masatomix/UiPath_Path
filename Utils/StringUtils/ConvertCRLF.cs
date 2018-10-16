using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Utils.StringUtils
{

    [Designer(typeof(ConvertCRLFDesigner))]
    public sealed class ConvertCRLF : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("処理したい文字データを指定します")]
        public InArgument<String> Target { get; set; }


        [Category("セットする改行コード")]
        [DefaultValue(false)]
        public Boolean CR { get; set; }


        [Category("セットする改行コード")]
        [DefaultValue(false)]
        public Boolean LF { get; set; }


        [Category("Output")]
        public OutArgument<String> Result { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var target = context.GetValue(this.Target);

            var trimData = target;

            // まず、LFだけにしてしまう
            trimData = trimData.Replace("\r\n", "\n");
            trimData = trimData.Replace("\r", "\n");

            if (CR)
            {
                trimData = trimData.Replace("\n", "\r\n");
            }
            else
            {
                // CRはすでに除去済みなのでなにもしない
            }

            if (LF)
            {
                // LFはすでに付与済みなのでなにもしない
            }
            else
            {
                trimData = trimData.Replace("\n", "");
            }

            Result.Set(context, trimData);
        }
    }


}
