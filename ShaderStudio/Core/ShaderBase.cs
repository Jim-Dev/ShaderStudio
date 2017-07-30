using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderStudio.Core
{
    public class ShaderBase
    {
        protected string name;
        protected StringBuilder infoLog;
        protected int actionStatus;
        protected bool isDirty = true;
        protected uint objectId;

        public bool IsDirty
        {
            get { return this.isDirty; }
        }
        public string InfoLog
        {
            get { return this.infoLog.ToString(); }
        }
    }
}
