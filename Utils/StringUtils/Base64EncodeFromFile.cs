using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.IO;
using Utils.Common;
using Utils.Properties;

namespace Utils.StringUtils
{

    [Designer(typeof(Base64EncodeFromFileDesigner))]
    public sealed class Base64EncodeFromFile : CodeActivity
    {

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> Path { get; set; }


        [Category("Output")]
        public OutArgument<String> Base64Data { get; set; }

        protected override void Execute(CodeActivityContext context)
        {

            var path = context.GetValue(this.Path);

            var bytes = ReadFile(path);
            var result = Convert.ToBase64String(bytes);

            context.SetValue(Base64Data, result);

        }

        public byte[] ReadFile(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }
}
