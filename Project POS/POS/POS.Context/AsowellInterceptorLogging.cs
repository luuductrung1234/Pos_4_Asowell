using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Context.Logging;

namespace POS.Context
{
    public class AsowellInterceptorLogging : DbCommandInterceptor
    {
        private ILogger _logger = new Logger();
        private readonly  Stopwatch _stopwatch = new Stopwatch();

        public override void ScalarExecuting(DbCommand command,
            DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Start();
        }

        public override void ScalarExecuted(DbCommand command,
            DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "AsowellInterceptor.ScalarExecuted", _stopwatch.Elapsed, "Command: {0}", command.CommandText);    
            }
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command,
            DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void NonQueryExecuted(DbCommand command,
            DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                _logger.Error("SQL Database", "Error executing command: {0}", command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "AsowellIntereptor.NonQueryExecuted", _stopwatch.Elapsed, "Command: {0}", command.CommandText);
            }
            base.NonQueryExecuting(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                _logger.Error("SQL Database", "Error executing command: {0}", command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "AsowellIntereptor.NonQueryExecuted", _stopwatch.Elapsed, "Command: {0}", command.CommandText);
            }
            base.ReaderExecuted(command, interceptionContext);
        }
    }
}
