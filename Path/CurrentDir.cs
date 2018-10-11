using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.IO;

namespace Path
{

    [Designer(typeof(CurrentDirDesigner))]
    public sealed class CurrentDir : CodeActivity
    {
        [Category("Output")]
        [Description("現在のディレクトリがフルパスで返ります")]
        public OutArgument<String> FullPath { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            context.SetValue(FullPath, Directory.GetCurrentDirectory());
        }
    }
}
