using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace AppCore.Utils
{
    public class BuildHelper
    {
        public static CmResult BuildCmm(string cmd)
        {
            string strOutput = null;
            bool success = false;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("sh");
                psi.UseShellExecute = false;
                psi.RedirectStandardError = true;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = true;
                psi.CreateNoWindow = true;
                using (Process proCompiler = new Process())
                {
                    proCompiler.StartInfo = psi;
                    proCompiler.Start();
                    proCompiler.StandardInput.WriteLine(cmd);

                    proCompiler.StandardInput.Flush();
                    proCompiler.StandardInput.Dispose();

                    //  是否有错误信息
                    bool flag = proCompiler.WaitForExit(StaticVariable.RUN_MILL);
                    Console.WriteLine("-----------flag：" + flag.ToString() + " pid:" + proCompiler.Id);
                    if (!flag)
                    {
                        proCompiler.Dispose();
                        success = false;
                        strOutput = $"执行时间超过{StaticVariable.RUN_MILL}ms，代码中可能包含死循环";
                    }
                    else
                    {
                        strOutput = proCompiler.StandardError.ReadToEnd();

                        if (string.IsNullOrEmpty(strOutput))
                        {
                            strOutput = proCompiler.StandardOutput.ReadToEnd();
                            success = true;
                        }
                    }
                }
                return success ? CmResult.BuildSuccess(string.IsNullOrEmpty(strOutput) ? "执行成功" : strOutput) : CmResult.BuildFail("执行失败", strOutput);
            }
            catch (Exception ex)
            {
                return CmResult.BuildFail("执行失败", ex.Message);
            }
        }

        public static object Execute(string code)
        {
            ScriptOptions scriptOptions = ScriptOptions.Default;
            var mscorlib = typeof(System.Object).GetTypeInfo().Assembly;
            var systemCore = typeof(System.Linq.Enumerable).GetTypeInfo().Assembly;
            scriptOptions = scriptOptions.AddReferences(mscorlib, systemCore);
            scriptOptions = scriptOptions.AddImports("System");
            scriptOptions = scriptOptions.AddImports("System.Linq");
            scriptOptions = scriptOptions.AddImports("System.Collections.Generic");

            Script script = CSharpScript.Create(code, scriptOptions);
            var endState = script.RunAsync().Result;

            //Task<ScriptState<object>> scrstate = CSharpScript.RunAsync(yourscript);
            return endState.ReturnValue;
        }
    }
}
