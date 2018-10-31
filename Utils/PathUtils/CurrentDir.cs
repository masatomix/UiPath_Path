using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.IO;
using Utils.Common;

namespace Utils.PathUtils
{

    [Designer(typeof(CurrentDirDesigner))]
    public sealed class CurrentDir : CodeActivity
    {
        [Category("Output")]
        [LocalizedDescription("CurrentDir_Description")]
        public OutArgument<String> FullPath { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            context.SetValue(FullPath, Directory.GetCurrentDirectory());
        }
    }
}
