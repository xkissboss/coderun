using AppCore.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Run
{
     public interface IRunCommand
    {
        CmResult C(string path);

        CmResult CPlusPlus(string path);

        CmResult Java(string path);

        CmResult Python(string path);

        CmResult CSharp(string path);

        CmResult Nodejs(string path);
    }
}
