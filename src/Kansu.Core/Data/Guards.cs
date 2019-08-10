using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kansu.Data {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    public delegate bool Guard<in T>(T value);

    /// <summary>
    /// 
    /// </summary>
    [DebuggerStepThrough]
    public static class Guards {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrDefault<T>(T value) =>
            EqualityComparer<T>.Default.Equals(value, default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="guard"></param>
        /// <param name="paramName"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowWhen<T>(T value, Guard<T> guard, string paramName) {
            if (guard(value)) {
                throw new ArgumentNullException(paramName, "Argument cannot be null or default.");
            }
        }

    }

}