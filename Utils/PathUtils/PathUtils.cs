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

    [Designer(typeof(PathUtilsDesigner))]
    public sealed class PathUtils : CodeActivity
    {

        [Category("Input")]
        [RequiredArgument]
        [LocalizedDescription("PathUtils_Path_Desc")]
        public InArgument<String> Path { get; set; }

        [Category("Output")]
        [LocalizedDescription("PathUtils_FullPath_Desc")]
        public OutArgument<String> FullPath { get; set; }


        [Category("Output")]
        [LocalizedDescription("PathUtils_DirectoryName_Desc")]
        public OutArgument<String> DirectoryName { get; set; }


        [Category("Output")]
        [LocalizedDescription("PathUtils_DirExists_Desc")]
        public OutArgument<Boolean> DirExists { get; set; }


        [Category("Output")]
        [LocalizedDescription("PathUtils_FileExists_Desc")]
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
            var dirExists = Directory.Exists(fullPathStr);
            context.SetValue(DirExists, dirExists);


            // FileExists セクション
            var fileExists = File.Exists(fullPathStr);
            context.SetValue(FileExists, fileExists);


            if (dirExists)
            {
                // fullPathStrの元になっている pathStrの末尾が\で終わる場合、終わらない場合があり
                // \で終了していない場合、GetDirectoryName はその上のパスを返してしまう
                // そのディレクトリが存在するときくらい、そのパス自体を返してあげるべきでは、という対応
                var tmp = System.IO.Path.Combine(fullPathStr, "dummy.txt");

                //  DirectoryName セクション
                context.SetValue(DirectoryName, System.IO.Path.GetDirectoryName(tmp));
            }
            else
            {
                //  DirectoryName セクション
                context.SetValue(DirectoryName, System.IO.Path.GetDirectoryName(fullPathStr));
            }






        }
    }
}
