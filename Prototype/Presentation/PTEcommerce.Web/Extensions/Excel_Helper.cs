using Framework.Configuration;
using Framework.Helper.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace PTEcommerce.Web.Extensions
{
    public class Excel_Helper
    {
        public List<PropertyInfo> GetProperties(Type type, IEnumerable<string> propertyNames = null)
        {
            var allProperties = type.GetProperties();
            var properties = new List<PropertyInfo>();

            foreach (var property in allProperties)
            {
                var name = property.Name;
                if (propertyNames != null)
                {
                    var field = propertyNames.FirstOrDefault(o => o.Contains(name));
                    if (!string.IsNullOrEmpty(field)) continue;
                }
                properties.Add(property);
            }
            return properties;
        }

        public List<object> ReadData<T>(Stream stream, ConfigExcel configExcel, ref List<ErrorRow> errors, ref List<dynamic> listDataTable)
        {
            var list = new List<T>();
            IWorkbook workbook;
            if (configExcel.FileImport.EndsWith("xls"))
                workbook = new HSSFWorkbook(stream);
            else if (configExcel.FileImport.EndsWith("xlsx"))
                workbook = new XSSFWorkbook(stream);
            else
                throw new Exception("Tệp tin sai định dạng!");

            var sheet = workbook.GetSheetAt(0);

            if (sheet.PhysicalNumberOfRows <= 1)
            {
                return list.Cast<object>().ToList();
            }

            //Get colums
            var clsType = typeof(T);
            var columns = clsType.GetProperties().Where(prop =>
            {
                if (configExcel.IgnoreColumns != null)
                {
                    var field = configExcel.IgnoreColumns.FirstOrDefault(o => o.Contains(prop.Name));
                    if (!string.IsNullOrEmpty(field)) return false;
                }
                return Attribute.IsDefined(prop, typeof(ExcelReaderAttribute));
            });

            var startRow = 0;
            if (!configExcel.RenderHeader && configExcel.StartRow > 0)
            {
                startRow = configExcel.StartRow - 1;
            }

            //var cr = new CellReference("D5");
            //var row1 = sheet.GetRow(cr.Row);
            //var cell1 = row1.GetCell(cr.Col);
            var rowDataTest = false;
            var evaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();
            var indexRow = 1;
            for (var i = startRow; i < sheet.PhysicalNumberOfRows; i++)
            {
                var item = Activator.CreateInstance<T>();
                dynamic itemTable = new System.Dynamic.ExpandoObject();
                var row = sheet.GetRow(i);
                var lineNumber = i + 1;
                var errorLine = errors.FirstOrDefault(x => x.Line == lineNumber);
                var j = CellReference.ConvertColStringToIndex(configExcel.StartColumnLetter);

                foreach (var column in columns)
                {
                    try
                    {
                        rowDataTest = false;
                        var attrs = (ExcelReaderAttribute)Attribute.GetCustomAttribute(column, typeof(ExcelReaderAttribute));

                        var columnLetter = CellReference.ConvertNumToColString(j);

                        if (j == row.Cells.Count)
                            break;

                        var t = Nullable.GetUnderlyingType(column.PropertyType) != null ? Nullable.GetUnderlyingType(column.PropertyType) : column.PropertyType;

                        var cell = row.GetCell(j);
                        j++;
                        if (cell == null) continue;
                        object val = null;
                        var cellValue = evaluator.Evaluate(cell);
                        if (cellValue == null) val = "";
                        else
                        {
                            switch (cellValue.CellType)
                            {
                                case CellType.Numeric:
                                    val = cellValue.NumberValue;
                                    break;
                                case CellType.Blank:
                                case CellType.String:
                                    val = cellValue.StringValue;
                                    break;
                                case CellType.Boolean:
                                    val = cellValue.BooleanValue;
                                    break;
                                case CellType.Error:
                                    val = cellValue.ErrorValue;
                                    break;
                                case CellType.Unknown:
                                    break;
                                case CellType.Formula:
                                    break;
                            }
                        }
                        if (cell.CellType == CellType.String || cell.CellType == CellType.Blank)
                        {
                            //Require
                            if (string.IsNullOrWhiteSpace((string)val) && attrs != null && !attrs.Required)
                            {
                                var msg = string.Format(Config.GetConfigByKey("MessageErrorImportRequired"), columnLetter);
                                if (errorLine == null)
                                {
                                    errorLine = new ErrorRow()
                                    {
                                        Line = lineNumber,
                                        ErrorColumn = new List<string>() { msg }
                                    };
                                    errors.Add(errorLine);
                                }
                                else errorLine.ErrorColumn.Add(msg);
                                continue;
                            }

                            var sArrayDataTest = Config.GetConfigByKey("ArrayDataTest") ?? string.Empty;
                            if (sArrayDataTest.Length > 0)
                            {
                                var arrayDataTest = sArrayDataTest.Split(',');
                                if (arrayDataTest.Any(x => x == (string)val))
                                {
                                    rowDataTest = true;
                                    break; // get out of the loop
                                }
                            }
                        }

                        //evaluator.EvaluateAll();
                        if ((val.IsNumber() && NumericTypeExtension.IsNumeric(t)) || val.Equals(t))
                        {
                            column.SetValue(item, Convert.ChangeType(val, t), null);
                            ((IDictionary<string, object>)itemTable)["column" + j] = Convert.ChangeType(val, t);
                        }

                        else
                        {
                            var msg = string.Format(Config.GetConfigByKey("MessageErrorImportInCorrect"), columnLetter);
                            if (errorLine == null)
                            {
                                errorLine = new ErrorRow()
                                {
                                    Line = lineNumber,
                                    ErrorColumn = new List<string>() { msg }
                                };
                                errors.Add(errorLine);
                            }
                            else
                                errorLine.ErrorColumn.Add(msg);
                        }
                        //column.SetValue(item, Convert.ChangeType(val, t), null);
                    }
                    catch (Exception ex)
                    {
                        errorLine = new ErrorRow()
                        {
                            Line = lineNumber,
                            ErrorColumn = new List<string>() { "Lỗi: " + ex.Message }
                        };
                        if (errorLine == null)
                            errors.Add(errorLine);
                        else
                            errorLine.ErrorColumn.Add(ex.Message);
                    }
                }
                if (!rowDataTest && errorLine == null)
                {
                    var error = ValidateItem(item, lineNumber);
                    if (error != null) errors.Add(error);
                    else
                    {
                        list.Add(item);
                        ((IDictionary<string, object>)itemTable)["column1"] = indexRow;
                        listDataTable.Add(itemTable);
                        indexRow++;
                    }
                }
            }


            workbook.Close();
            stream.Close();

            return list.Cast<object>().ToList();
        }

        public List<object> ReadDataExtension<T>(Stream stream, ConfigExcel configExcel, ref List<ErrorRow> errors, ref List<dynamic> listDataTable)
        {
            var list = new List<T>();
            IWorkbook workbook;
            if (configExcel.FileImport.EndsWith("xls"))
                workbook = new HSSFWorkbook(stream);
            else if (configExcel.FileImport.EndsWith("xlsx"))
                workbook = new XSSFWorkbook(stream);
            else
                throw new Exception("Tệp tin sai định dạng!");

            var sheet = workbook.GetSheetAt(0);

            if (sheet.PhysicalNumberOfRows <= 1)
            {
                return list.Cast<object>().ToList();
            }

            //Get colums
            var clsType = typeof(T);
            var columns = clsType.GetProperties().Where(prop =>
            {
                if (configExcel.IgnoreColumns != null)
                {
                    var field = configExcel.IgnoreColumns.FirstOrDefault(o => o.Contains(prop.Name));
                    if (!string.IsNullOrEmpty(field)) return false;
                }
                return Attribute.IsDefined(prop, typeof(ExcelReaderAttribute));
            }).ToList();

            var startRow = 0;
            if (!configExcel.RenderHeader && configExcel.StartRow > 0)
            {
                startRow = configExcel.StartRow - 1;
            }

            //var cr = new CellReference("D5");
            //var row1 = sheet.GetRow(cr.Row);
            //var cell1 = row1.GetCell(cr.Col);
            var rowDataTest = false;
            var evaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();
            var indexRow = 1;
            for (var i = startRow; i < sheet.PhysicalNumberOfRows; i++)
            {
                var item = Activator.CreateInstance<T>();
                dynamic itemTable = new System.Dynamic.ExpandoObject();
                var row = sheet.GetRow(i);
                var lineNumber = i + 1;
                var errorLine = errors.FirstOrDefault(x => x.Line == lineNumber);
                var j = CellReference.ConvertColStringToIndex(configExcel.StartColumnLetter);

                foreach (var column in columns)
                {
                    try
                    {
                        rowDataTest = false;
                        var attrs = (ExcelReaderAttribute)Attribute.GetCustomAttribute(column, typeof(ExcelReaderAttribute));

                        var columnLetter = CellReference.ConvertNumToColString(j);

                        if (j == row.Cells.Count)
                            break;

                        var t = Nullable.GetUnderlyingType(column.PropertyType) != null ? Nullable.GetUnderlyingType(column.PropertyType) : column.PropertyType;

                        var cell = row.GetCell(j);
                        j++;
                        if (cell == null) continue;
                        object val = null;
                        var cellValue = evaluator.Evaluate(cell);
                        if (cellValue == null) val = "";
                        else
                        {
                            switch (cellValue.CellType)
                            {
                                case CellType.Numeric:
                                    val = cellValue.NumberValue;
                                    break;
                                case CellType.Blank:
                                case CellType.String:
                                    val = cellValue.StringValue;
                                    break;
                                case CellType.Boolean:
                                    val = cellValue.BooleanValue;
                                    break;
                                case CellType.Error:
                                    val = cellValue.ErrorValue;
                                    break;
                                case CellType.Unknown:
                                    break;
                                case CellType.Formula:
                                    break;
                            }
                        }

                        #region comment
                        //switch (cell.CellType)
                        //{
                        //    case CellType.Numeric:
                        //        val = cell.NumericCellValue;
                        //        break;
                        //    case CellType.Blank:
                        //    case CellType.String:
                        //        val = cell.StringCellValue;
                        //        break;
                        //    case CellType.Boolean:
                        //        val = cell.BooleanCellValue;
                        //        break;
                        //    case CellType.Error:
                        //        val = cell.ErrorCellValue;
                        //        break;
                        //    case CellType.Unknown:
                        //        break;
                        //    case CellType.Formula:
                        //        val = cell.CellFormula;
                        //        break;
                        //}
                        #endregion

                        if (cell.CellType == CellType.String || cell.CellType == CellType.Blank)
                        {
                            //Require
                            if (string.IsNullOrWhiteSpace((string)val) && attrs != null && !attrs.Required)
                            {
                                var msg = string.Format(Config.GetConfigByKey("MessageErrorImportRequired"), columnLetter);
                                if (errorLine == null)
                                {
                                    errorLine = new ErrorRow()
                                    {
                                        Line = lineNumber,
                                        ErrorColumn = new List<string>() { msg }
                                    };
                                    errors.Add(errorLine);
                                }
                                else errorLine.ErrorColumn.Add(msg);
                                continue;
                            }

                            var sArrayDataTest = Config.GetConfigByKey("ArrayDataTest") ?? string.Empty;
                            if (sArrayDataTest.Length > 0)
                            {
                                var arrayDataTest = sArrayDataTest.Split(',');
                                if (arrayDataTest.Any(x => x == (string)val))
                                {
                                    rowDataTest = true;
                                    break; // get out of the loop
                                }
                            }
                        }

                        //evaluator.EvaluateAll();
                        if ((val.IsNumber() && NumericTypeExtension.IsNumeric(t)) || val.GetType().Equals(t))
                        {
                            column.SetValue(item, Convert.ChangeType(val, t), null);
                            ((IDictionary<string, object>)itemTable)["column" + j] = Convert.ChangeType(val, t);
                        }

                        else
                        {
                            var msg = string.Format(Config.GetConfigByKey("MessageErrorImportInCorrect"), columnLetter);
                            if (errorLine == null)
                            {
                                errorLine = new ErrorRow()
                                {
                                    Line = lineNumber,
                                    ErrorColumn = new List<string>() { msg }
                                };
                                errors.Add(errorLine);
                            }
                            else
                                errorLine.ErrorColumn.Add(msg);
                        }
                        //column.SetValue(item, Convert.ChangeType(val, t), null);
                    }
                    catch (Exception ex)
                    {
                        errorLine = new ErrorRow()
                        {
                            Line = lineNumber,
                            ErrorColumn = new List<string>() { "Lỗi: " + ex.Message }
                        };
                        if (errorLine == null)
                            errors.Add(errorLine);
                        else
                            errorLine.ErrorColumn.Add(ex.Message);
                    }
                }
                if (!rowDataTest && errorLine == null)
                {
                    var error = ValidateItem(item, lineNumber);
                    if (error != null) errors.Add(error);
                    //else
                    //{
                    //    list.Add(item);
                    //    ((IDictionary<string, object>)itemTable)["column1"] = indexRow;
                    //    listDataTable.Add(itemTable);
                    //    indexRow++;
                    //}

                }
                list.Add(item);
                ((IDictionary<string, object>)itemTable)["column1"] = indexRow;
                listDataTable.Add(itemTable);
                indexRow++;
            }

            workbook.Close();
            stream.Close();

            return list.Cast<object>().ToList();
        }

        private ErrorRow ValidateItem<T>(T item, int lineNumber)
        {
            ErrorRow errorRow = null;
            var validationContext = new ValidationContext(item, null, null);
            var results = new List<ValidationResult>();


            if (!Validator.TryValidateObject(item, validationContext, results, true))
            {
                if (results != null && results.Count > 0)
                {
                    errorRow = new ErrorRow
                    {
                        Line = lineNumber,
                        ErrorColumn = new List<string>()
                    };

                    errorRow.ErrorColumn.AddRange(results.Select(e => e.ErrorMessage));
                }
            }

            return errorRow;
        }

        public virtual void CreateExportExcel<T>(List<T> items, ConfigExcel configExcel, ref HSSFWorkbook workbook)
        {
            var templateFile = (configExcel != null && !string.IsNullOrEmpty(configExcel.TemplateFile))
                ? configExcel.TemplateFile
                : "";

            //http://www.c-sharpcorner.com/blogs/export-to-excel-using-npoi-dll-library
            // dll refered NPOI.dll and NPOI.OOXML
            IWorkbook workbookTemplate;

            using (var file = new FileStream(templateFile, FileMode.Open, FileAccess.Read))
            {
                workbookTemplate = new HSSFWorkbook(file);
                file.Close();
            }

            ////Style cell
            //ICellStyle styleCell = workbook.CreateCellStyle();

            ////Setting the line of the top border
            //styleCell.BorderTop = BorderStyle.Thick;
            //styleCell.TopBorderColor = 256;

            //styleCell.BorderLeft = BorderStyle.Thick;
            //styleCell.LeftBorderColor = 256;

            //styleCell.BorderRight = BorderStyle.Thick;
            //styleCell.RightBorderColor = 256;

            //styleCell.BorderBottom = BorderStyle.Thick;
            //styleCell.BottomBorderColor = 256;

            //Copy sheet from template
            ISheet sheet;
            //https://stackoverflow.com/questions/44377465/copying-excel-worksheets-with-npoi-copysheet-copy-is-always-blank
            var safeName = WorkbookUtil.CreateSafeSheetName(configExcel.SheetName);
            ((HSSFSheet)workbookTemplate.GetSheetAt(0)).CopyTo(workbook, safeName, true, true);
            sheet = workbook.GetSheet(configExcel.SheetName);
            var properties = GetProperties(typeof(T), configExcel != null ? configExcel.IgnoreColumns : null);

            var i = 0;

            // Header row
            if (configExcel != null && configExcel.RenderHeader)
            {
                var j = 0;
                var row = sheet.CreateRow(i);
                foreach (var property in properties)
                {
                    row.CreateCell(j).SetCellValue(property.Name);
                    j++;
                }
                i++;
            }

            //Replace header if have mark value
            if (configExcel != null && configExcel.MarkHeader != null)
            {
                var rowMaxHeader = configExcel.StartRow - 1;
                for (var iHeader = 0; iHeader < rowMaxHeader; iHeader++)
                {
                    var j = 0;
                    var row = sheet.GetRow(iHeader);
                    foreach (var property in properties)
                    {
                        if (j == row.Cells.Count)
                            continue;
                        var val = row.GetCell(j).StringCellValue;

                        if (string.IsNullOrWhiteSpace(val))
                        {
                            j++;
                            continue;
                        }
                        val = val.ReplaceWithStringBuilder(configExcel.MarkHeader);
                        row.Cells[j].SetCellValue(val);
                        j++;
                    }
                    i++;
                }

            }

            if (configExcel != null && !configExcel.RenderHeader && configExcel.StartRow > 0)
            {
                i = configExcel.StartRow - 1;
            }

            //Fill data
            foreach (var item in items)
            {
                var j = configExcel.StartColumn;
                //var row = sheet.CreateRow(i);
                var row = sheet.GetRow(i);
                if (row == null) row = sheet.CreateRow(i);
                //http://npoi.codeplex.com/SourceControl/latest#NPOI.Examples/NumberFormatInXls/Program.cs
                //http://npoi.codeplex.com/SourceControl/latest#NPOI.Examples/MergeCellsInXls/Program.cs
                foreach (var property in properties)
                {
                    var value = property.GetValue(item);
                    if (value != null)
                    {
                        var cell = row.GetCell(j);
                        if (cell == null) cell = row.CreateCell(j);
                        cell.SetCellValue(value.ToString());
                        //cell.CellStyle =  styleCell;
                        //row.CreateCell(j).SetCellValue(value.ToString());
                    }
                    j++;
                }
                i++;
            }

            if (configExcel.AutoSizeColumn)
            {
                for (int index = 0; index < properties.Count; index++)
                {
                    sheet.AutoSizeColumn(index);
                }
            }

            if (configExcel.MergeCells != null && configExcel.MergeCells.Any())
            {
                configExcel.MergeCells.ForEach(o =>
                {
                    sheet.AddMergedRegion(new CellRangeAddress(o.FirstRow, o.LastRow, o.FirstCol, o.LastCol));
                });
            }

            //add picture data to this workbook.
            if (!string.IsNullOrEmpty(configExcel.ImageData))
            {
                var bytes = Convert.FromBase64String(configExcel.ImageData);
                int pictureIdx = workbook.AddPicture(bytes, PictureType.PNG);

                ICreationHelper helper = workbook.GetCreationHelper();

                // Create the drawing patriarch.  This is the top level container for all shapes.
                IDrawing drawing = sheet.CreateDrawingPatriarch();

                // add a picture shape
                IClientAnchor anchor = helper.CreateClientAnchor();

                //set top-left corner of the picture,
                //subsequent call of Picture#resize() will operate relative to it
                anchor.Col1 = 0;
                anchor.Row1 = i + 3;
                IPicture pict = drawing.CreatePicture(anchor, pictureIdx);
                //auto-size picture relative to its top-left corner
                pict.Resize();
            }

            //enables gridline
            sheet.DisplayGridlines = true;

            //Force excel to recalculate all the formula while open
            sheet.ForceFormulaRecalculation = true;

            //var memoryStream = new MemoryStream();
            //workbook.Write(memoryStream);
            //return new FileContentResult(memoryStream.ToArray(), contentType)
            //{
            //    FileDownloadName = fileExport
            //};
        }

        public FileContentResult ExportExcel<T>(List<T> items, ConfigExcel configExcel, ref string pathFileSave, bool save = true)
        {
            try
            {
                var fileExport = DateTime.Now.ToString("dd-mm-yyyy");
                if (configExcel != null && !string.IsNullOrEmpty(configExcel.FileExport))
                    fileExport = configExcel.FileExport;

                var extension = Path.GetExtension(fileExport);
                var templateFile = (configExcel != null && !string.IsNullOrEmpty(configExcel.TemplateFile))
                    ? configExcel.TemplateFile
                    : "";

                //http://www.c-sharpcorner.com/blogs/export-to-excel-using-npoi-dll-library
                // dll refered NPOI.dll and NPOI.OOXML
                IWorkbook workbook;
                var contentType = "";
                switch (extension)
                {
                    case ".xlsx":
                        using (var file = new FileStream(templateFile, FileMode.Open, FileAccess.Read))
                        {
                            workbook = new XSSFWorkbook(file);
                            file.Close();
                        }
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        break;
                    case ".xls":
                        using (var file = new FileStream(templateFile, FileMode.Open, FileAccess.Read))
                        {
                            workbook = new HSSFWorkbook(file);
                            file.Close();
                        }
                        //workbook = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF((XSSFWorkbook)workbook);
                        //workbook = new HSSFWorkbook();
                        contentType = "application/vnd.ms-excel";
                        break;
                    default:
                        throw new Exception("This format is not supported");
                }

                ISheet sheet;
                if (configExcel != null && !string.IsNullOrEmpty(configExcel.TemplateFile))
                {
                    sheet = workbook.GetSheetAt(0); //get first Excel sheet from workbook  
                }
                else sheet = workbook.CreateSheet();

                //ISheet sheet1 = workbook.CreateSheet("Sheet 1");

                var properties = GetProperties(typeof(T), configExcel != null ? configExcel.IgnoreColumns : null);

                var i = 0;

                // Header row
                if (configExcel != null && configExcel.RenderHeader)
                {
                    var j = 0;
                    var row = sheet.CreateRow(i);
                    foreach (var property in properties)
                    {
                        row.CreateCell(j).SetCellValue(property.Name);
                        j++;
                    }
                    i++;
                }

                //Replace header if have mark value
                if (configExcel != null && configExcel.MarkHeader != null)
                {
                    var rowMaxHeader = configExcel.StartRow - 1;
                    for (var iHeader = 0; iHeader < rowMaxHeader; iHeader++)
                    {
                        var j = 0;
                        var row = sheet.GetRow(iHeader);
                        foreach (var property in properties)
                        {
                            if (j == row.Cells.Count)
                                continue;
                            var val = row.GetCell(j).StringCellValue;

                            if (string.IsNullOrWhiteSpace(val))
                            {
                                j++;
                                continue;
                            }
                            val = val.ReplaceWithStringBuilder(configExcel.MarkHeader);
                            row.Cells[j].SetCellValue(val);
                            j++;
                        }
                        i++;
                    }

                }

                if (configExcel != null && !configExcel.RenderHeader && configExcel.StartRow > 0)
                {
                    i = configExcel.StartRow - 1;
                }

                //Fill data
                foreach (var item in items)
                {
                    var j = configExcel.StartColumn;
                    //var row = sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    if (row == null) row = sheet.CreateRow(i);
                    //http://npoi.codeplex.com/SourceControl/latest#NPOI.Examples/NumberFormatInXls/Program.cs
                    //http://npoi.codeplex.com/SourceControl/latest#NPOI.Examples/MergeCellsInXls/Program.cs
                    foreach (var property in properties)
                    {
                        var value = property.GetValue(item);
                        if (value != null)
                        {
                            var cell = row.GetCell(j);
                            if (cell == null) cell = row.CreateCell(j);
                            cell.SetCellValue(value.ToString());
                            //cell.CellStyle =  styleCell;
                            //row.CreateCell(j).SetCellValue(value.ToString());
                        }
                        j++;
                    }
                    i++;
                }

                if (configExcel.AutoSizeColumn)
                {
                    for (int index = 0; index < properties.Count; index++)
                    {
                        sheet.AutoSizeColumn(index);
                    }
                }

                if (configExcel.MergeCells != null && configExcel.MergeCells.Any())
                {
                    configExcel.MergeCells.ForEach(o =>
                    {
                        sheet.AddMergedRegion(new CellRangeAddress(o.FirstRow, o.LastRow, o.FirstCol, o.LastCol));
                    });
                }

                //add picture data to this workbook.
                if (configExcel.ListChart != null && configExcel.ListChart.Any())
                {
                    foreach (var chart in configExcel.ListChart)
                    {
                        byte[] bytes = File.ReadAllBytes(chart);
                        int pictureIdx = workbook.AddPicture(bytes, PictureType.PNG);

                        ICreationHelper helper = workbook.GetCreationHelper();

                        // Create the drawing patriarch.  This is the top level container for all shapes.
                        IDrawing drawing = sheet.CreateDrawingPatriarch();

                        // add a picture shape
                        IClientAnchor anchor = helper.CreateClientAnchor();

                        //set top-left corner of the picture,
                        //subsequent call of Picture#resize() will operate relative to it
                        anchor.Col1 = 0;
                        anchor.Row1 = i + 3;
                        IPicture pict = drawing.CreatePicture(anchor, pictureIdx);
                        //auto-size picture relative to its top-left corner
                        pict.Resize();
                    }
                }

                //enables gridline
                sheet.DisplayGridlines = true;

                //Force excel to recalculate all the formula while open
                sheet.ForceFormulaRecalculation = true;

                var memoryStream = new MemoryStream();
                workbook.Write(memoryStream);
                if (save)
                {
                    System.IO.File.WriteAllBytes(pathFileSave, memoryStream.ToArray());
                }

                //return memoryStream;
                return new FileContentResult(memoryStream.ToArray(), contentType)
                {
                    FileDownloadName = fileExport
                };
            }
            catch (Exception ex)
            {
                Logging.PutError("Export_Order", ex);
                return null;
            }
        }
    }
    public class ConfigExcel
    {
        public string FileExport { get; set; }
        public string FileImport { get; set; }
        public string TemplateFile { get; set; }
        public bool RenderHeader { get; set; }
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public string StartColumnLetter { get; set; }
        public string SheetName { get; set; }
        public string ImageData { get; set; }
        public bool AutoSizeColumn { get; set; }
        public Dictionary<string, string> MarkHeader { get; set; }
        public IEnumerable<string> IgnoreColumns { get; set; }
        public IEnumerable<MergeCell> MergeCells { get; set; }
        public IEnumerable<string> ListChart { get; set; }
    }
    public class MergeCell
    {
        public int FirstRow { get; set; }
        public int LastRow { get; set; }
        public int FirstCol { get; set; }
        public int LastCol { get; set; }
        public object Reference { get; internal set; }
    }
    public static class NumericTypeExtension
    {
        public static bool IsNumeric(Type dataType)
        {
            return (dataType == typeof(int)
                    || dataType == typeof(double)
                    || dataType == typeof(long)
                    || dataType == typeof(short)
                    || dataType == typeof(float)
                    || dataType == typeof(Int16)
                    || dataType == typeof(Int32)
                    || dataType == typeof(Int64)
                    || dataType == typeof(uint)
                    || dataType == typeof(UInt16)
                    || dataType == typeof(UInt32)
                    || dataType == typeof(UInt64)
                    || dataType == typeof(sbyte)
                    || dataType == typeof(Single)
                    || dataType == typeof(Decimal)
                   );
        }
    }
    public static class StringExtension
    {
        /// <summary>
        /// Use the current thread's culture info for conversion
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Overload which uses the culture info with the specified name
        /// </summary>
        public static string ToTitleCase(this string str, string cultureInfoName)
        {
            var cultureInfo = new CultureInfo(cultureInfoName);
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Overload which uses the specified culture info
        /// </summary>
        public static string ToTitleCase(this string str, CultureInfo cultureInfo)
        {
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        //https://chodounsky.net/2013/11/27/replace-multiple-strings-effectively/
        public static string ReplaceWithStringBuilder(this string value, Dictionary<string, string> toReplace)
        {
            var result = new StringBuilder(value);
            foreach (var item in toReplace)
            {
                result.Replace(item.Key, item.Value);
            }
            return result.ToString();
        }
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }
    public class ErrorRow
    {
        public ErrorRow()
        {
            ErrorColumn = new List<string>();
        }

        public int Line { get; set; }
        //public int ErrorText { get; set; }
        public List<string> ErrorColumn { get; set; }
    }
    public class ExcelReaderAttribute : Attribute
    {
        //private string name;
        public string Column { get; set; }
        public string AliAsColumn { get; set; }
        public int Row { get; set; }
        public bool Required { get; set; }

        public ExcelReaderAttribute(string column = "")
        {
            Column = column;
            Required = false;
        }
    }
}