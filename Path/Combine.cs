﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Path
{

    public sealed class Combine : CodeActivity
    {
        // 文字列型のアクティビティ入力引数を定義します
        public InArgument<string> Text { get; set; }

        public OutArgument<String> result { get; set; }  // 追記

        // アクティビティが値を返す場合は、CodeActivity<TResult> から派生して、
        // Execute メソッドから値を返します。
        protected override void Execute(CodeActivityContext context)
        {
            // テキスト型の入力引数のランタイム値を取得します
            string text = context.GetValue(this.Text);
            result.Set(context, text + " Hello world.");  // 追記
        }
    }
}
