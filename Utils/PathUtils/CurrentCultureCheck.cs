using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Threading;

namespace Utils.PathUtils
{
    public sealed class CurrentCultureCheck : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            Console.WriteLine(Thread.CurrentThread.CurrentCulture);
            Console.WriteLine(Thread.CurrentThread.CurrentUICulture);
        }
    }
}
