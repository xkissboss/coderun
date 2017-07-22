using AppCore.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Build
{
    public interface IBuildCommand
    {
        CmResult C(string path);

        CmResult CPlusPlus(string path);

        CmResult Java(string path);
    }
}
