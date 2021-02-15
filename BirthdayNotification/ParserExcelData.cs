using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace BirthdayNotification
{
    public static class ParserExcelData
    {
        /// <summary>
        /// Парсинг Excel файла "List_Birthday_employ.xlsx" 
        /// </summary>
        /// <param name="list">Номер листа (0-11)</param>
        /// <returns></returns>
        public static List<(string name, string day)> ParseExcel(int list)
        {
            var result = new List<(string, string)>();
            var fileName = "List_Birthday_employ.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (File.Exists(fileName))
            {
                using (var excel = new ExcelPackage(new FileInfo(fileName)))
                {
                    var sheet = excel.Workbook.Worksheets[list];
                    if (sheet == null) throw new Exception("Не существующий лист");
                    var startCount = 0;
                    var endCount = 0;
                    (startCount, endCount) = StartEndCount(sheet);
                    if (startCount == 0 || endCount == 0) throw new Exception("Нет данных");

                    for (int i = startCount; i < endCount; i++)
                    {
                        var name = "";
                        var day = new object();
                        try
                        {
                            name = (sheet.Cells[i, 2].Value as string);
                            day = sheet.Cells[i, 3].Value;
                            
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        result.Add((name, day.ToString()));
                    }
                }
            }
            return result;
        }

        private static (int startCount, int endCount) StartEndCount(ExcelWorksheet sheet)
        {
            int startCount = 0;
            int endCount = 0;

            for (int i = 2; i < sheet.Cells.Count(); i++)
            {
                var value = sheet.Cells[i, 2].Value as string;
                if (value == null) continue;

                if (value.Contains("Фамилия"))
                    startCount = i + 1;

                if (value.Contains("{End}"))
                {
                    endCount = i;
                    break;
                }
            }

            return (startCount, endCount);
        }
    }
}
