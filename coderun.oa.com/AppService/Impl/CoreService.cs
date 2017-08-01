using AppCore.Run;
using AppCore.Utils;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppService.Impl
{
    public class CoreService : ICoreService
    {
        private readonly IRunCommand run;

        public CoreService(IRunCommand run)
        {
            this.run = run;
        }

        public Task<CmResult> CSharp(string code)
        {
            return Task.Run(() => { return run.CSharp(code); });
        }

        public Task<CmResult> Java(string code)
        {
            Match match = Regex.Match(code, @"public class\s+(?<classname>\w+)\s+");
            if (!match.Success)
                return Task.Run(() => { return CmResult.BuildFail("", "找不到public class类"); });
            string className = match.Groups["classname"].Value;
            string path = Path.Combine(StaticVariable.NowPath, $"{className}.java");
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(code);
                sw.Flush();
            }
            return Task.Run(() => { return run.Java(path); });
        }

        public Task<CmResult> Nodejs(string code)
        {
            string path = Path.Combine(StaticVariable.NowPath, $"{Guid.NewGuid().ToString("N")}.js");
            File.WriteAllText(path, code, Encoding.UTF8);
            return Task.Run(() => { return run.Nodejs(path); });
        }

        public Task<CmResult> Python(string code)
        {
            string path = Path.Combine(StaticVariable.NowPath, $"{Guid.NewGuid().ToString("N")}.py");
            File.WriteAllText(path, code, Encoding.UTF8);
            return Task.Run(() => { return run.Python(path); });
        }

        public Task<CmResult> C(string code)
        {
            string path = Path.Combine(StaticVariable.NowPath, $"{Guid.NewGuid().ToString("N")}.c");
            File.WriteAllText(path, code, Encoding.UTF8);

            return Task.Run(() => { return run.C(path); });
        }

        public Task<CmResult> CPlusPlus(string code)
        {
            string path = Path.Combine(StaticVariable.NowPath, $"{Guid.NewGuid().ToString("N")}.cpp");
            File.WriteAllText(path, code, Encoding.UTF8);
            return Task.Run(() => { return run.CPlusPlus(path); });
        }
    }
}
