using AppCore.Run;
using AppCore.Utils;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AppService.Impl
{
    public class CoreService : ICoreService
    {
        private readonly IRunCommand run;

        public CoreService(IRunCommand run)
        {
            this.run = run;
        }

        public CmResult CSharp(string code)
        {
            return run.CSharp(code);
        }

        public CmResult Java(string code)
        {
            Match match = Regex.Match(code, @"public class\s+(?<classname>\w+)\s+");
            if (!match.Success)
                return CmResult.BuildFail("", "找不到public class类");
            string className = match.Groups["classname"].Value;
            string path = Path.Combine(StaticVariable.NowPath, $"{className}.java");
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(code);
                sw.Flush();
            }
            return run.Java(path);
        }

        public CmResult Nodejs(string code)
        {
            string path = Path.Combine(StaticVariable.NowPath, $"{Guid.NewGuid().ToString("N")}.js");
            File.WriteAllText(path, code, Encoding.UTF8);
            return run.Nodejs(path);
        }

        public CmResult Python(string code)
        {
            string path = Path.Combine(StaticVariable.NowPath, $"{Guid.NewGuid().ToString("N")}.py");
            File.WriteAllText(path, code, Encoding.UTF8);
            return run.Python(path);
        }

        public CmResult C(string code)
        {
            string path = Path.Combine(StaticVariable.NowPath, $"{Guid.NewGuid().ToString("N")}.c");
            File.WriteAllText(path, code, Encoding.UTF8);
            return run.C(path);
        }

        public CmResult CPlusPlus(string code)
        {
            string path = Path.Combine(StaticVariable.NowPath, $"{Guid.NewGuid().ToString("N")}.cpp");
            File.WriteAllText(path, code, Encoding.UTF8);
            return run.CPlusPlus(path);
        }
    }
}
