using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Context.Logging
{
    interface ILogger
    {
        void Infomation(string message);
        void Information(string fmt, params object[] vars);
        void Information(Exception exception, string fmt, params object[] vars);

        void Warning(string message);
        void Warning(string fmt, params object[] vars);
        void Warning(Exception exception, string fmt, params object[] vars);

        void Error(string message);
        void Error(string fmt, params object[] vars);
        void Error(Exception exception, string fmt, params object[] vars);

        void TraceApi(string componentName, string method, TimeSpan timeSpan);
        void TraceApi(string componentName, string method, TimeSpan timeSpan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timeSpan, string fmt, params object[] vars);
    }
}
