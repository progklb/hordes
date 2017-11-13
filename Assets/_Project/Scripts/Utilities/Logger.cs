using System;

using UnityEngine;

namespace Hordes
{
    public static class Logger
    {
        public static void LogFormat(object context, string message)
        {
            Debug.LogFormat("[{0}] {1}", context.GetType().Name, message);
        }

        public static void LogErrorFormat(object context, string message)
        {
            Debug.LogErrorFormat("[{0}] {1}", context.GetType().Name, message);
        }
    }
}
