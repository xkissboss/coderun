using AppCore.Build;
using AppCore.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Run.Impl
{
    public class RunCommand : IRunCommand
    {

        private readonly IBuildCommand build;


        public RunCommand(IBuildCommand build)
        {
            this.build = build;
        }

        public CmResult C(string path)
        {
            CmResult cr = build.C(path);
            if (!cr.Success)
                return cr;
            if (!FileHelper.IsExists(cr.Message))
                return CmResult.BuildFail("", "编译文件不存在");
            return BuildHelper.BuildCmm($"{cr.Message}");
        }

        public CmResult CPlusPlus(string path)
        {
            CmResult cr = build.CPlusPlus(path);
            if (!cr.Success)
                return cr;
            if (!FileHelper.IsExists(cr.Message))
                return CmResult.BuildFail("", "编译文件不存在");
            return BuildHelper.BuildCmm($"{cr.Message}");
        }

        public CmResult CSharp(string path)
        {
            try
            {
                object result = BuildHelper.Execute(path + "return Test.Main();");
                if (result == null)
                    return CmResult.BuildSuccess("执行成功：没有返回值");
                return CmResult.BuildSuccess(string.IsNullOrEmpty(result.ToString()) ? "执行成功" : result.ToString());
            }
            catch (Exception ex)
            {
                return CmResult.BuildFail("执行失败", ex.StackTrace);
            }
        }

        public CmResult Java(string path)
        {
            CmResult cr = build.Java(path);
            if (!cr.Success)
                return cr;
            if (!FileHelper.IsExists($"{cr.Message}.class"))
                return CmResult.BuildFail("", "执行文件不存在");
            string classPath = FileHelper.GetDir(path);

            string className = FileHelper.GetName(path).Replace(".java", "");
            return BuildHelper.BuildCmm($"java -classpath {classPath} -Dfile.encoding=utf8 {className}");
        }

        // 无需编译
        public CmResult Nodejs(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            if (!path.EndsWith(".js"))
                return CmResult.BuildFail("", "文件类型不正确");
            return BuildHelper.BuildCmm($"node {path}");
        }

        // 无需编译
        public CmResult Python(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            if (!path.EndsWith(".py"))
                return CmResult.BuildFail("", "文件类型不正确");
            return BuildHelper.BuildCmm($"python {path}");
        }
    }
}
