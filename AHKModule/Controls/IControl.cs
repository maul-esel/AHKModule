using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AhkModule.Controls
{
    interface IControl
    {
        string Create(int x, int y, string name);
    }
}
