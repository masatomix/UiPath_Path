using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Path
{

    [Designer(typeof(CombineDesigner))]
    public sealed class Combine : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("stringの配列で、連結したいファイルパスを記述します。")]
        public InArgument<String[]> pathArray { get; set; }

        [Category("Output")]
        public OutArgument<String> result { get; set; }

        // アクティビティが値を返す場合は、CodeActivity<TResult> から派生して、
        // Execute メソッドから値を返します。
        protected override void Execute(CodeActivityContext context)
        {
            var paths = pathArray.Get(context);
            var combine = System.IO.Path.Combine(paths);
            result.Set(context, combine);
        }
    }
}
