using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Utils.StringUtils
{

    [Designer(typeof(ToJSONStringDesigner))]
    public sealed class ToJSONString : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<Object> Input { get; set; }


        [Category("Output")]
        public OutArgument<String> Result { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            var obj = context.GetValue(this.Input);
            String jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented);
            context.SetValue(this.Result, jsonStr);
        }
    }
}
