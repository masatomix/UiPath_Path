using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Utils.PathUtils
{

    [Designer(typeof(CombineDesigner))]
    public sealed class Combine : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String[]> PathArray { get; set; }

        [Category("Output")]
        public OutArgument<String> Result { get; set; }

        // アクティビティが値を返す場合は、CodeActivity<TResult> から派生して、
        // Execute メソッドから値を返します。
        protected override void Execute(CodeActivityContext context)
        {
            var paths = PathArray.Get(context);
            var combine = System.IO.Path.Combine(paths);
            Result.Set(context, combine);
        }
    }
}
