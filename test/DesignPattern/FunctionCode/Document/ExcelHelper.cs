using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.FunctionCode.Document
{
    public class ExcelReportHelper
    {
        //private readonly ILogger logger = LogManager.GetLogger("ExcelReportHelper");

        /// <summary>
        /// 将列表导出到Excel中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listData"></param>
        /// <param name="sheetName"></param>
        /// <param name="hssfWorkbook"></param>
        /// <returns></returns>
        public XSSFWorkbook ExportToExcelSheet<T>(List<T> listData, string sheetName, XSSFWorkbook hssfWorkbook)
        {
            try
            {
                int propertyCount = getPropertyCount(typeof(T));
                var sheet1 = hssfWorkbook.CreateSheet(sheetName);
                var row1 = (XSSFRow)sheet1.CreateRow(0);
                WriteHeader(typeof(T), row1);

                int i = 0;
                foreach (var item in listData)
                {
                    int rowIndex = i;
                    var rowData = (XSSFRow)sheet1.CreateRow(rowIndex + 1);
                    WriteData(item, typeof(T), rowData);
                    i++;
                }
                //setAutoColumn(sheet1, i);
                return hssfWorkbook;
            }
            catch (Exception e)
            {
                //logger.Error(e, "ExportToExcelSheet failed,{0}", e.Message);
                return null;
            }
        }

        private void setAutoColumn(ISheet sheet, int maxColumn)
        {
            //列宽自适应，只对英文和数字有效  
            for (int i = 0; i <= maxColumn; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            //获取当前列的宽度，然后对比本列的长度，取最大值  
            for (int columnNum = 0; columnNum <= maxColumn; columnNum++)
            {
                int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                for (int rowNum = 3; rowNum <= sheet.LastRowNum; rowNum++)
                {
                    IRow currentRow;
                    //当前行未被使用过  
                    if (sheet.GetRow(rowNum) == null)
                    {
                        currentRow = sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        currentRow = sheet.GetRow(rowNum);
                    }

                    if (currentRow.GetCell(columnNum) != null)
                    {
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                        if (columnWidth < length)
                        {
                            columnWidth = length;
                        }
                    }
                }
                sheet.SetColumnWidth(columnNum, columnWidth * 256);
            }
        }
        private int getPropertyCount(Type type)
        {
            if (type != null)
            {
                Type t = type;
                PropertyInfo[] propertyInfo = t.GetProperties();

                int i = 0;
                foreach (PropertyInfo propInfo in propertyInfo)
                {
                    object[] objAttrs = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true);

                    if (objAttrs.Length > 0)
                    {
                        i++;
                    }

                }
                return i;
            }
            return 0;
        }

        /// <summary>
        /// 写表头
        /// </summary>
        /// <param name="type"></param>
        /// <param name="row"> </param>
        public void WriteHeader(Type type, XSSFRow row)
        {
            if (type != null)
            {
                Type t = type;
                PropertyInfo[] propertyInfo = t.GetProperties();
                int i = 0;
                foreach (PropertyInfo propInfo in propertyInfo)
                {
                    var cell = row.CreateCell(i);
                    object[] objAttrs = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true);

                    if (objAttrs.Length > 0)
                    {
                        var attr = objAttrs[0] as DisplayNameAttribute;
                        cell.SetCellValue(attr != null ? attr.DisplayName : "");
                        i++;
                    }
                    //cell.CellStyle = style;

                }

            }
        }

        public void WriteData<T>(T obj, Type type, XSSFRow row)
        {
            if (obj != null)
            {
                Type t = type;
                PropertyInfo[] propertyInfo = t.GetProperties();
                int i = 0;
                foreach (PropertyInfo propInfo in propertyInfo)
                {
                    object[] objAttrs = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true);

                    if (objAttrs.Length > 0)
                    {
                        var cell = row.CreateCell(i);
                        object value = propInfo.GetValue(obj, null);
                        if (value != null)
                        {
                            if (propInfo.PropertyType == typeof(int))
                            {

                                cell.SetCellValue((int)value);
                            }
                            else
                            {
                                cell.SetCellValue(value.ToString());
                            }
                        }

                        i++;
                    }
                }

            }
        }


        // <summary>
        /// 将Excel的列索引转换为列名，列索引从0开始，列名从A开始。如第0列为A，第1列为B...
        /// </summary>
        /// <param name="index">列索引</param>
        /// <returns>列名，如第0列为A，第1列为B...</returns>
        public static string ConvertColumnIndexToColumnName(int index)
        {
            index = index + 1;
            int system = 26;
            char[] digArray = new char[100];
            int i = 0;
            while (index > 0)
            {
                int mod = index % system;
                if (mod == 0) mod = system;
                digArray[i++] = (char)(mod - 1 + 'A');
                index = (index - 1) / 26;
            }
            StringBuilder sb = new StringBuilder(i);
            for (int j = i - 1; j >= 0; j--)
            {
                sb.Append(digArray[j]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将列表导出到CSV中
        /// </summary>
        public MemoryStream ExportToCsv<T>(List<T> listData)
        {
            try
            {
                var exportContentStringBuilder = this.DataConversion(listData);
                string content = exportContentStringBuilder.ToString();

                MemoryStream stream = new MemoryStream();
                byte[] bytes = Encoding.UTF8.GetBytes(content);

                byte[] bom = new[] { (byte)0xEF, (byte)0xBB, (byte)0xBF };
                stream.Write(bom, 0, bom.Length);
                stream.Write(bytes, 0, bytes.Length);
                return stream;
            }
            catch (Exception e)
            {
                //logger.Error(e, "ExportToCsv failed{0}", e.Message);
                return null;
            }
        }

        public StringBuilder DataConversion<T>(List<T> listData)
        {
            StringBuilder exportContentStringBuilder = new StringBuilder();

            if (typeof(T) != null)
            {
                Type t = typeof(T);
                PropertyInfo[] propertyInfo = t.GetProperties();
                int n = 0;
                foreach (PropertyInfo propInfo in propertyInfo)
                {
                    object[] objAttrs = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true);

                    if (objAttrs.Length > 0)
                    {
                        var attr = objAttrs[0] as DisplayNameAttribute;
                        if (n == 0)
                        {
                            exportContentStringBuilder.Append(attr.DisplayName);
                        }
                        else
                        {
                            exportContentStringBuilder.Append("," + attr.DisplayName);
                        }
                    }
                    n++;
                }
            }

            exportContentStringBuilder.AppendLine();

            foreach (var item in listData)
            {
                if (listData != null)
                {
                    int p = 0;
                    Type t = typeof(T);
                    PropertyInfo[] propertyInfo = t.GetProperties();
                    foreach (PropertyInfo propInfo in propertyInfo)
                    {
                        object[] objAttrs = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                        List<string> row = new List<string>();
                        if (objAttrs.Length > 0)
                        {
                            object value = propInfo.GetValue(item, null);
                            if (value!=null)
                            { 
                             if (p == 0)
                                {
                                    exportContentStringBuilder.Append(value);
                                }
                                else
                                {
                                    exportContentStringBuilder.Append("," + value);
                                }
                                p++;
                            }
                        }

                    }
                    exportContentStringBuilder.AppendLine();
                }
            }
            return exportContentStringBuilder;
        }
    }

}
