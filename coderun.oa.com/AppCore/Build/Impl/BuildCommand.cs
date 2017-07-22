using AppCore.Utils;
using System;
using System.IO;

namespace AppCore.Build.Impl
{
    public class BuildCommand : IBuildCommand
    {
        public CmResult C(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            string dir = FileHelper.GetDir(path);
            string guid = Guid.NewGuid().ToString("N");
            string runsh = Path.Combine(dir, guid);
            CmResult cr = BuildHelper.BuildCmm($"gcc {path} -o {runsh}");
            if (cr.Success)
                return CmResult.BuildSuccess(Path.Combine(dir, $"./{guid}"));
            return cr;
        }

        public CmResult CPlusPlus(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            string dir = FileHelper.GetDir(path);
            string guid = Guid.NewGuid().ToString("N");
            string runsh = Path.Combine(dir, guid);
            CmResult cr = BuildHelper.BuildCmm($"g++ {path} -o {runsh}");
            if (cr.Success)
                return CmResult.BuildSuccess(Path.Combine(dir, $"./{guid}"));
            return cr;
        }

        public CmResult Java(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            if (!path.EndsWith(".java"))
                return CmResult.BuildFail("", "文件类型不正确");
            CmResult cr = BuildHelper.BuildCmm($"javac -encoding utf8 {path}");
            if (!cr.Success)
                return cr;
            string runName = path.Substring(0, path.LastIndexOf(".java"));
            return CmResult.BuildSuccess(runName);
        }

    }
}
