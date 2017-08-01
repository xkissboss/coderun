using AppCore.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppService
{
    public interface ICoreService
    {
        /// <summary>
        /// 执行c语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CmResult> C(string code);
        /// <summary>
        /// 执行c++语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CmResult> CPlusPlus(string code);
        /// <summary>
        /// 执行java语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CmResult> Java(string code);
        /// <summary>
        /// 执行python语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CmResult> Python(string code);
        /// <summary>
        /// 执行c#语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CmResult> CSharp(string code);
        /// <summary>
        /// 执行node脚本
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CmResult> Nodejs(string code);
    }
}
