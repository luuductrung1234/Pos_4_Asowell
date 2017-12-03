using System;
using PdfRpt.Core.Contracts;
using POS.Repository.DAL;
using PdfRpt.FluentInterface;
using POS.Helper.PrintHelper.Model;
using System.Linq;
using System.Collections.Generic;
using POS.Entities;
using POS.Entities.CustomEntities;

namespace POS.Helper.PrintHelper.Report
{
    /// <summary>
    /// Create Report about SalaryNote(outcome) in specific time
    /// </summary>
    public class SalaryNoteReport : IListPdfReport
    {
        public IPdfReportData CreatePdfReport(AdminwsOfCloudPOS unitofwork, DateTime startTime, DateTime endTime, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Salary Report" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
                doc.PrintingPreferences(new PrintingPreferences
                {
                    ShowPrintDialogAutomatically = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\arial.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("MM/dd/yyyy"));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    defaultHeader.ImagePath(System.IO.Path.Combine(AppPath.ApplicationPath, "Images\\logo.png"));
                    defaultHeader.Message("SALARY REPORT (" + startTime.ToShortDateString() + " - " + endTime.ToShortDateString() +")");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue1Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(5);
            })
            .MainTableDataSource(dataSource =>
                {
                    var queryWithYear =
                        unitofwork.SalaryNoteRepository.Get(x =>
                            x.ForYear >= startTime.Year && x.ForYear <= endTime.Year).ToList();

                    var queryWithStartTimeMonth = 
                        queryWithYear.Where(x => (x.ForMonth >= startTime.Month && x.ForYear == startTime.Year)
                                                                                || x.ForYear > startTime.Year).ToList();

                    var queryWithEndTimeMonth =
                        queryWithStartTimeMonth.Where(x => (x.ForMonth <= endTime.Month && x.ForYear == endTime.Year)
                                                                                || x.ForYear < endTime.Year).ToList();

                    List<SalaryNoteForReport> salaryReportList = new List<SalaryNoteForReport>();
                    foreach (var salary in queryWithEndTimeMonth)
                    {
                        var salaryRpt = new SalaryNoteForReport()
                        {
                            SnId = salary.SnId,
                            EmpId = salary.EmpId,
                            EmpName = salary.Employee.Name,
                            DatePay = (salary.DatePay == null) ? "" : salary.DatePay.ToString(),
                            SalaryValue = salary.SalaryValue,
                            WorkHour = salary.WorkHour,
                            ForMonth = salary.ForMonth,
                            ForYear = salary.ForYear,
                            IsPaid = (salary.IsPaid == 1) ? "Yes" : "No"
                        };
                        salaryReportList.Add(salaryRpt);
                    }

                    dataSource.StronglyTypedList(salaryReportList);
                })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.OverallSummarySettings("Summary");
                summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.SnId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Blue);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.EmpName);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(2);
                    column.HeaderCell("Emp Name");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.DatePay);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Date Pay");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.IsPaid);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Is Paid");
                });


                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.ForMonth);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(6);
                    column.Width(2);
                    column.HeaderCell("For Month");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.ForYear);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("For Year");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.WorkHour);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Hour (h)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.SalaryValue);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Salary Value (kVND)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display.");
            })
            .Export(export =>
            {
                export.ToExcel();
                export.ToCsv();
                export.ToXml();
            })
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Salary-Report-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateDetailsPdfReport(AdminwsOfCloudPOS unitofwork, DateTime startTime, DateTime endTime, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Salary Detail Report" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
                doc.PrintingPreferences(new PrintingPreferences
                {
                    ShowPrintDialogAutomatically = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\arial.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("MM/dd/yyyy"));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    defaultHeader.ImagePath(System.IO.Path.Combine(AppPath.ApplicationPath, "Images\\logo.png"));
                    defaultHeader.Message("Salary DETAILS REPORT (" + startTime.ToShortDateString() + " - " + endTime.ToShortDateString() + ")");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue2Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(20);
            })
            .MainTableDataSource(dataSource =>
            {

                var queryWithYear =
                    unitofwork.WorkingHistoryRepository.Get(x =>
                        x.SalaryNote.ForYear >= startTime.Year && x.SalaryNote.ForYear <= endTime.Year).ToList();

                var queryWithStartTimeMonth =
                    queryWithYear.Where(x => (x.SalaryNote.ForMonth >= startTime.Month && x.SalaryNote.ForYear == startTime.Year)
                                             || x.SalaryNote.ForYear > startTime.Year).ToList();

                var queryWithEndTimeMonth =
                    queryWithStartTimeMonth.Where(x => (x.SalaryNote.ForMonth <= endTime.Month && x.SalaryNote.ForYear == endTime.Year)
                                                       || x.SalaryNote.ForYear < endTime.Year).ToList();

                dataSource.StronglyTypedList(queryWithEndTimeMonth);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.OverallSummarySettings("Summary");
                summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<WorkingHistory>(x => x.WhId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("WorkHistory ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Blue);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<WorkingHistory>(x => x.ResultSalary);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Salary ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<WorkingHistory>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Emp ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<WorkingHistory>(x => x.Employee.Name);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(3);
                    column.HeaderCell("Emp Name");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<WorkingHistory>(x => x.StartTime);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Start Time");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<WorkingHistory>(x => x.EndTime);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(6);
                    column.Width(2);
                    column.HeaderCell("End Time");
                });

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display.");
            })
            .Export(export =>
            {
                export.ToExcel();
                export.ToCsv();
                export.ToXml();
            })
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Salary-DetailsReport-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateEntityPdfReport(AdminwsOfCloudPOS unitofwork, DateTime startTime, DateTime endTime, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Salary Entities Report" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
                doc.PrintingPreferences(new PrintingPreferences
                {
                    ShowPrintDialogAutomatically = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\arial.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("MM/dd/yyyy"));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    defaultHeader.ImagePath(System.IO.Path.Combine(AppPath.ApplicationPath, "Images\\logo.png"));
                    defaultHeader.Message("SALARY ENTITIES REPORT (" + startTime.ToShortDateString() + " - " + endTime.ToShortDateString() + ")");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue2Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(5);
            })
            .MainTableDataSource(dataSource =>
            {
                var queryWithYear =
                    unitofwork.SalaryNoteRepository.Get(x =>
                        x.ForYear >= startTime.Year && x.ForYear <= endTime.Year).ToList();

                var queryWithStartTimeMonth =
                    queryWithYear.Where(x => (x.ForMonth >= startTime.Month && x.ForYear == startTime.Year)
                                             || x.ForYear > startTime.Year).ToList();

                var queryWithEndTimeMonth =
                    queryWithStartTimeMonth.Where(x => (x.ForMonth <= endTime.Month && x.ForYear == endTime.Year)
                                                       || x.ForYear < endTime.Year).ToList();

                List<SalaryEntityForReport> salaryEntityReportList = new List<SalaryEntityForReport>();
                foreach (var emp in unitofwork.EmployeeRepository.Get().ToList())
                {
                    var queryWithEmployee = queryWithEndTimeMonth.Where(x => x.EmpId.Equals(emp.EmpId));

                    double totalWorkHour = 0;
                    decimal totalSalary = 0;
                    foreach (var salNote in queryWithEmployee)
                    {
                        totalWorkHour += salNote.WorkHour;
                        totalSalary += salNote.SalaryValue;
                    }

                    var salaryEntity = new SalaryEntityForReport()
                    {
                        Id = emp.EmpId,
                        Name = emp.Name,
                        WorkHour = totalWorkHour,
                        Salary =  totalSalary
                    };


                    salaryEntityReportList.Add(salaryEntity);
                }

                dataSource.StronglyTypedList(salaryEntityReportList);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.OverallSummarySettings("Summary");
                summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryEntityForReport>(x => x.Id);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Blue);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryEntityForReport>(x => x.Name);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Name", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryEntityForReport>(x => x.WorkHour);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Work Hour(h)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    //column.AggregateFunction(aggregateFunction =>
                    //{
                    //    aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                    //    aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                    //        ? string.Empty : string.Format("{0:n0}", obj));
                    //});
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryEntityForReport>(x => x.Salary);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Salary (kVND)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display.");
            })
            .Export(export =>
            {
                export.ToExcel();
                export.ToCsv();
                export.ToXml();
            })
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Salary-EntityReport-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateMonthPdfReport(AdminwsOfCloudPOS unitofwork, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Salary Report" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
                doc.PrintingPreferences(new PrintingPreferences
                {
                    ShowPrintDialogAutomatically = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\arial.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("MM/dd/yyyy"));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    defaultHeader.ImagePath(System.IO.Path.Combine(AppPath.ApplicationPath, "Images\\logo.png"));
                    defaultHeader.Message("SALARY REPORT (" + DateTime.Now.Month + "/" + DateTime.Now.Year + ")");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue1Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(5);
            })
            .MainTableDataSource(dataSource =>
            {
                var monthQuery =
                    unitofwork.SalaryNoteRepository.Get(x =>
                        x.ForMonth == DateTime.Now.Month && x.ForYear == DateTime.Now.Year).ToList();

                List<SalaryNoteForReport> salaryReportList = new List<SalaryNoteForReport>();
                foreach (var salary in monthQuery)
                {
                    var salaryRpt = new SalaryNoteForReport()
                    {
                        SnId = salary.SnId,
                        EmpId = salary.EmpId,
                        EmpName = salary.Employee.Name,
                        DatePay = (salary.DatePay == null) ? "" : salary.DatePay.ToString(),
                        SalaryValue = salary.SalaryValue,
                        WorkHour = salary.WorkHour,
                        ForMonth = salary.ForMonth,
                        ForYear = salary.ForYear,
                        IsPaid = (salary.IsPaid == 1) ? "Yes" : "No"
                    };
                    salaryReportList.Add(salaryRpt);
                }

                dataSource.StronglyTypedList(salaryReportList);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.OverallSummarySettings("Summary");
                summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.SnId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Blue);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.EmpName);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(2);
                    column.HeaderCell("Emp Name");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.DatePay);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Date Pay");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.IsPaid);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Is Paid");
                });


                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.ForMonth);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(6);
                    column.Width(2);
                    column.HeaderCell("For Month");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.ForYear);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("For Year");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.WorkHour);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Hour (h)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.SalaryValue);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Salary Value (kVND)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display.");
            })
            .Export(export =>
            {
                export.ToExcel();
                export.ToCsv();
                export.ToXml();
            })
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Salary-Report-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateDayPdfReport(AdminwsOfCloudPOS unitofwork, string folderName)
        {
            throw new NotImplementedException();
        }


        public IPdfReportData CreateYearPdfReport(AdminwsOfCloudPOS unitofwork, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Salary Report" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
                doc.PrintingPreferences(new PrintingPreferences
                {
                    ShowPrintDialogAutomatically = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\arial.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("MM/dd/yyyy"));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    defaultHeader.ImagePath(System.IO.Path.Combine(AppPath.ApplicationPath, "Images\\logo.png"));
                    defaultHeader.Message("SALARY REPORT (" + DateTime.Now.Year + ")");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue1Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(5);
            })
            .MainTableDataSource(dataSource =>
            {
                var monthQuery =
                    unitofwork.SalaryNoteRepository.Get(x => x.ForYear == DateTime.Now.Year).ToList();

                List<SalaryNoteForReport> salaryReportList = new List<SalaryNoteForReport>();
                foreach (var salary in monthQuery)
                {
                    var salaryRpt = new SalaryNoteForReport()
                    {
                        SnId = salary.SnId,
                        EmpId = salary.EmpId,
                        EmpName = salary.Employee.Name,
                        DatePay = (salary.DatePay == null) ? "" : salary.DatePay.ToString(),
                        SalaryValue = salary.SalaryValue,
                        WorkHour = salary.WorkHour,
                        ForMonth = salary.ForMonth,
                        ForYear = salary.ForYear,
                        IsPaid = (salary.IsPaid == 1) ? "Yes" : "No"
                    };
                    salaryReportList.Add(salaryRpt);
                }

                dataSource.StronglyTypedList(salaryReportList);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.OverallSummarySettings("Summary");
                summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.SnId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Blue);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.EmpName);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(2);
                    column.HeaderCell("Emp Name");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.DatePay);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Date Pay");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.IsPaid);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Is Paid");
                });


                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.ForMonth);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(6);
                    column.Width(2);
                    column.HeaderCell("For Month");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.ForYear);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("For Year");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.WorkHour);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Hour (h)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<SalaryNoteForReport>(x => x.SalaryValue);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Salary Value (kVND)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display.");
            })
            .Export(export =>
            {
                export.ToExcel();
                export.ToCsv();
                export.ToXml();
            })
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Salary-Report-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }
    }
}
