using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Models
{
    public class Bone
    {
        public Bone Parent { get; set; }
        public Matrix4x3 Matrix { get; set; }

        public Matrix4x3 Resolve()
        {
            return Matrix;
            //if (Parent == null)
            //    return Matrix;
            //else
            //    return Matrix;//Matrix4x3.Combine(Parent.Resolve(), Matrix);
        }
    }
}
