using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using Utils.Common;

namespace Utils.StringUtils
{


    [Designer(typeof(Base64DecodeDesigner))]
    public sealed class Base64Decode : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [LocalizedDescription("Base64Decode_Description")]
        public InArgument<String> Target { get; set; }


        [Category("Output")]
        public OutArgument<String> Base64DecodeData { get; set; }


        // アクティビティが値を返す場合は、CodeActivity<TResult> から派生して、
        // Execute メソッドから値を返します。
        protected override void Execute(CodeActivityContext context)
        {
            // テキスト型の入力引数のランタイム値を取得します
            string text = context.GetValue(this.Target);
            var resut = Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(text));
            context.SetValue(Base64DecodeData, resut);


        }
    }
}
