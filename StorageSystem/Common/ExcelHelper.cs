using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using StorageSystem.Shared.Dtos;
using System.IO;

namespace StorageSystem.Common
{
    public static class ExcelHelper
    {
        public static List<StorageDetailDto> GetListFromFile(string filePath)
        {
            List<StorageDetailDto> list = new List<StorageDetailDto>();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("未找到文件！");
            }

            IWorkbook workbook = null; //创建一个新的Excel文件
            ICell cell = null;
            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //把xls文件读入workbook变量里，之后就可以关闭了
                workbook = new XSSFWorkbook(fs);
            }

            ISheet sheet = workbook.GetSheetAt(0); //按名称获取工作表

            if (sheet == null)
            {
                throw new FileNotFoundException("文件不正确！");
            }

            int row = 1;
            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    cell = sheet.GetRow(row).GetCell(i);
                    if (cell == null)
                        break;
                }

                if (cell == null)
                    break;

                StorageDetailDto storage = new StorageDetailDto();
                storage.InstorageDate = DateTime.Now;
                storage.Infinish = false;
                cell = sheet.GetRow(row).GetCell(0);
                storage.WbsNo = GetValue(cell);

                cell = sheet.GetRow(row).GetCell(1);
                storage.MaterielSN = GetValue(cell);

                cell = sheet.GetRow(row).GetCell(2);
                storage.MaterielDetail = GetValue(cell);

                cell = sheet.GetRow(row++).GetCell(3);
                storage.StorageName = GetValue(cell);

                list.Add(storage);
            }
            return list;
        }

        private static string GetValue(ICell cell)
        {
            if (cell.CellType == CellType.String)
            {
                return cell.StringCellValue;
            }
            else if (cell.CellType == CellType.Numeric)
            {
                return cell.NumericCellValue.ToString();
            }
            return "";
        }
    }
}