using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Threading;
using Utils.Properties;

namespace Utils.Common
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string resourceName;
        public LocalizedDescriptionAttribute(string resourceName)
            : base()
        {
            this.resourceName = resourceName;
        }

        public override string Description
        {
            get
            {
                return Resources.ResourceManager.GetString(this.resourceName);
            }
        }
    }
}