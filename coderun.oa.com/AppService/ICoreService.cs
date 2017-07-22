using AppCore.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService
{
    public interface ICoreService
    {
        /// <summary>
        /// 执行c语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        CmResult C(string code);
        /// <summary>
        /// 执行c++语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        CmResult CPlusPlus(string code);
        /// <summary>
        /// 执行java语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        CmResult Java(string code);
        /// <summary>
        /// 执行python语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        CmResult Python(string code);
        /// <summary>
        /// 执行c#语言
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        CmResult CSharp(string code);
        /// <summary>
        /// 执行node脚本
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        CmResult Nodejs(string code);
    }
}
