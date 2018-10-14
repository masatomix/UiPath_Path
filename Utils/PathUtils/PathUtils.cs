using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.IO;

namespace Utils.PathUtils
{

    [Designer(typeof(PathUtilsDesigner))]
    public sealed class PathUtils : CodeActivity
    {

        [Category("Input")]
        [RequiredArgument]
        [Description("処理したいファイルパスを記述します。")]
        public InArgument<String> Path { get; set; }

        [Category("Output")]
        [Description("フルパスが返ります")]
        public OutArgument<String> FullPath { get; set; }


        [Category("Output")]
        [Description("指定したディレクトリが存在すればTrue(ファイルだったら存在してもFalse)")]
        public OutArgument<Boolean> DirExists { get; set; }


        [Category("Output")]
        [Description("指定したファイルが存在すればTrue(ディレクトリだったら存在してもFalse)")]
        public OutArgument<Boolean> FileExists { get; set; }


        // アクティビティが値を返す場合は、CodeActivity<TResult> から派生して、
        // Execute メソッドから値を返します。
        protected override void Execute(CodeActivityContext context)
        {
            // テキスト型の入力引数のランタイム値を取得します
            string pathStr = context.GetValue(this.Path);

            //  FullPath セクション
            string fullPathStr = System.IO.Path.GetFullPath(pathStr).Replace("/", "\\");
            context.SetValue(FullPath, fullPathStr);


            // DirExist セクション
            context.SetValue(DirExists, Directory.Exists(fullPathStr));
            // FileExists セクション
            context.SetValue(FileExists, File.Exists(fullPathStr));


        }
    }
}
