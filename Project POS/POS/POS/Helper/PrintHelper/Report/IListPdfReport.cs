using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;
using POS.Entities;
using POS.Entities.CustomEntities;
using POS.Repository.DAL;
using AggregateFunction = PdfRpt.Core.Contracts.AggregateFunction;


namespace POS.Helper.PrintHelper.Report
{
    

    public interface IListPdfReport
    {
        IPdfReportData CreatePdfReport(AdminwsOfCloud unitofwork, DateTime startime, DateTime endTime, string folderName);

        IPdfReportData CreateDetailsPdfReport(AdminwsOfCloud unitofwork, DateTime startime, DateTime endTime, string folderName);

        IPdfReportData CreateEntityPdfReport(AdminwsOfCloud unitofwork, DateTime startime, DateTime endTime, string folderName);

        IPdfReportData CreateMonthPdfReport(AdminwsOfCloud unitofwork, string folderName);

        IPdfReportData CreateDayPdfReport(AdminwsOfCloud unitofwork, string folderName);

        IPdfReportData CreateYearPdfReport(AdminwsOfCloud unitofwork, string folderName);
    }
}
