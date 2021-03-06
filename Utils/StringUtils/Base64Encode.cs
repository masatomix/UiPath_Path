﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using Utils.Common;
using Utils.Properties;

namespace Utils.StringUtils
{

    [Designer(typeof(Base64EncodeDesigner))]
    public sealed class Base64Encode : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> Target { get; set; }


        [Category("Output")]
        public OutArgument<String> Base64Data { get; set; }

        // アクティビティが値を返す場合は、CodeActivity<TResult> から派生して、
        // Execute メソッドから値を返します。
        protected override void Execute(CodeActivityContext context)
        {

            string text = context.GetValue(this.Target);
            var result = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(text));

            context.SetValue(Base64Data, result);

        }
    }
}
