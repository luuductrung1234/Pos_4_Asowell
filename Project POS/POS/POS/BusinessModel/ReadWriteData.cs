using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using POS.Entities;
using POS.Repository.DAL;
using System.Linq;

namespace POS.BusinessModel
{
    public class ReadWriteData
    {
        //string a = System.IO.Directory.GetCurrentDirectory();
        static string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        private static List<Chair> chairTemp = new List<Chair>();

        //check file tableSize
        public static bool checkTableSizeFileExist()
        {
            return File.Exists(startupProjectPath + "\\SerializedData\\tableSize.txt");
        }

        //read file tableSize
        public static string[] readTableSize()
        {
            using (FileStream fs = new FileStream(startupProjectPath + "\\SerializedData\\tableSize.txt", FileMode.Open))
            {
                using (StreamReader rd = new StreamReader(fs, Encoding.UTF8))
                {
                    string tableSize = rd.ReadLine();
                    return tableSize.Split('-');
                }
            }
        }

        //write file tableSize
        public static void writeTableSize(string size)
        {
            using (FileStream fs = new FileStream(startupProjectPath + "\\SerializedData\\tableSize.txt", FileMode.Create))
            {
                using (StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8))
                {
                    sWriter.WriteLine(size);
                }
            }
        }


        public static string[] ReadPrinterSetting()
        {
            using (FileStream fs = new FileStream(startupProjectPath + "\\SerializedData\\printerSetting.txt", FileMode.Open))
            {
                using (StreamReader rd = new StreamReader(fs, Encoding.UTF8))
                {
                    string printer = rd.ReadLine();
                    string[] result = printer?.Split(',');

                    if (result?.Length >= 4)
                    {
                        return result;
                    }
                }

                MessageBox.Show("There has no previous setting, so the configuration will set to default!");
                return null;
            }
        }

        public static void WritePrinterSetting(string printers)
        {
            using (FileStream fs = new FileStream(startupProjectPath + "\\SerializedData\\printerSetting.txt", FileMode.Create))
            {
                using (StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8))
                {
                    sWriter.WriteLine(printers);
                }
            }
        }

        public static string[] ReadDBConfig()
        {
            using (FileStream fs = new FileStream(startupProjectPath + "\\SerializedData\\dbconfig.txt", FileMode.Open))
            {
                using (StreamReader rd = new StreamReader(fs, Encoding.UTF8))
                {
                    string dbConfig = rd.ReadLine();
                    string[] result = dbConfig?.Split(',');

                    if (result?.Length >= 5)
                    {
                        return result;
                    }
                }

                
                return null;
            }
        }

        //ToDo: Need to encrypt config before save to file
        public static void WriteDBConfig(string dbconfig)
        {
            using (FileStream fs = new FileStream(startupProjectPath + "\\SerializedData\\dbconfig.txt", FileMode.Create))
            {
                using (StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8))
                {
                    sWriter.WriteLine(dbconfig);
                }
            }
        }

        //check file tableImagePath isExist
        public static bool checkTableImagePathFileExist()
        {
            return File.Exists(startupProjectPath + "\\SerializedData\\tableImagePath.txt");
        }

        //read file tableImagePath
        public static string readTableImagePath()
        {
            using (FileStream fs = new FileStream(startupProjectPath + "\\SerializedData\\tableImagePath.txt", FileMode.Open))
            {
                using (StreamReader rd = new StreamReader(fs, Encoding.UTF8))
                {
                    string tableImagePath = rd.ReadLine();
                    return tableImagePath;
                }
            }
        }

        //write file tableImagePath
        public static void writeTableImagePath(string browseFilePath)
        {
            using (FileStream fs = new FileStream(startupProjectPath + "\\SerializedData\\tableImagePath.txt", FileMode.Create))
            {
                using (StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8))
                {
                    sWriter.WriteLine(browseFilePath);
                }
            }
        }

        ////check file tableRuntimeHistory isExist
        //public static bool checkTableRuntimeHistoryFileExist()
        //{
        //    return File.Exists(startupProjectPath + "\\SerializedData\\tableRuntimeHistory.bin");
        //}

        //write khi add new
        public static Entities.Table writeOnAddNew(EmployeewsOfLocalPOS _unitofwork, Rectangle rec, Entities.Employee emp)
        {
            Entities.Table newTable = new Entities.Table()
            {
                TableId = 0,
                TableNumber = int.Parse(rec.Name.Substring(5)),
                ChairAmount = 0,
                PosX = Convert.ToInt32(rec.Margin.Left),
                PosY = Convert.ToInt32(rec.Margin.Top),
                IsPinned = 0,
                IsOrdered = 0,
                IsLocked = 0,
                TableRec = rec
            };

            _unitofwork.TableRepository.Insert(newTable);
            _unitofwork.Save();

            Entities.OrderTemp newOrderTemp = new Entities.OrderTemp()
            {
                CusId = "CUS0000001",
                EmpId = emp.EmpId,
                TableOwned = _unitofwork.TableRepository.Get(x => x.TableNumber.Equals(newTable.TableNumber)).First().TableId,
                Ordertime = DateTime.Now,
                TotalPrice = 0,
                CustomerPay = 0,
                PayBack = 0
            };

            _unitofwork.OrderTempRepository.Insert(newOrderTemp);
            _unitofwork.Save();

            return _unitofwork.TableRepository.Get(x => x.TableNumber.Equals(newTable.TableNumber)).First();
        }

        ////write khi update
        //public static void writeOnUpdateChair(Table table, List<Chair> chList, int chairAmount)
        //{
        //    foreach (Table curTable in TableTempData.TbList)
        //    {
        //        if (curTable.TableNumber == table.TableNumber)
        //        {
        //            curTable.TableNumber = table.TableNumber;
        //            curTable.Position = table.Position;
        //            curTable.ChairAmount = chairAmount;

        //            curTable.ChairData = chList;

        //            for (int i = chList.Count; i < chairAmount; i++)
        //            {
        //                Chair newChair = new Chair();
        //                newChair.ChairNumber = i + 1;
        //                newChair.TableOfChair = curTable.TableNumber;
        //                newChair.ChairOrderDetails = new List<OrderNoteDetail>();
        //                curTable.ChairData.Add(newChair);
        //            }
        //        }
        //    }

        //    writeToBinFile();
        //}

        ////write khi co order
        //public static void writeOnOrder(Rectangle rec)
        //{

        //}

        ////write khi drag
        //public static void writeOnDrag(Rectangle rec)
        //{
        //    foreach (Table curTable in TableTempData.TbList)
        //    {
        //        if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
        //        {
        //            curTable.TableNumber = int.Parse(rec.Name.Substring(5));
        //            curTable.Position = new Point(rec.Margin.Left, rec.Margin.Top);
        //        }
        //    }

        //    writeToBinFile();
        //}

        ////write khi pin
        //public static void writeOnPin(Rectangle rec)
        //{
        //    foreach (Table curTable in TableTempData.TbList)
        //    {
        //        if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
        //        {
        //            curTable.TableNumber = int.Parse(rec.Name.Substring(5));
        //            curTable.Position = new Point(rec.Margin.Left, rec.Margin.Top);
        //            curTable.IsPinned = true;
        //        }
        //    }

        //    writeToBinFile();
        //}

        ////write khi move(unpin)
        //public static void writeOnMove(Rectangle rec)
        //{
        //    foreach (Table curTable in TableTempData.TbList)
        //    {
        //        if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
        //        {
        //            curTable.TableNumber = int.Parse(rec.Name.Substring(5));
        //            curTable.Position = new Point(rec.Margin.Left, rec.Margin.Top);
        //            curTable.IsPinned = false;
        //        }
        //    }

        //    writeToBinFile();
        //}

        ////write khi remove
        //public static void writeOnRemove(Rectangle rec)
        //{
        //    Table delTable = new Table();

        //    foreach (Table curTable in TableTempData.TbList)
        //    {
        //        if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
        //        {
        //            delTable = curTable;
        //            break;
        //        }
        //    }

        //    TableTempData.TbList.Remove(delTable);

        //    writeToBinFile();
        //}

        ////write khi pay
        //public static void writeOnPay(Rectangle rec)
        //{
        //    foreach (Table curTable in TableTempData.TbList)
        //    {
        //        if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
        //        {
        //            curTable.TableNumber = int.Parse(rec.Name.Substring(5));
        //            curTable.Position = new Point(rec.Margin.Left, rec.Margin.Top);
        //            curTable.TableOrder = new OrderNote() { EmpId = (App.Current.Properties["EmpLogin"] as Employee).EmpId, Ordertable = int.Parse(rec.Name.Substring(5)), Ordertime = DateTime.Now };
        //            curTable.TableOrderDetails = new List<OrderNoteDetail>();
        //            curTable.IsOrdered = false;

        //            curTable.ChairData = new List<Chair>();

        //            for (int i = 0; i < curTable.ChairAmount; i++)
        //            {
        //                Chair newChair = new Chair();
        //                newChair.ChairNumber = i + 1;
        //                newChair.TableOfChair = curTable.TableNumber;
        //                newChair.ChairOrderDetails = new List<OrderNoteDetail>();
        //                curTable.ChairData.Add(newChair);
        //            }
        //            break;
        //        }
        //    }

        //    writeToBinFile();
        //}

        ////write to file tableRuntimeHistory.bin;
        //public static void writeToBinFile()
        //{
        //    string dir = startupProjectPath;
        //    string serializationFile = System.IO.Path.Combine(dir, "SerializedData\\tableRuntimeHistory.bin");

        //    using (Stream stream = File.Open(serializationFile, FileMode.Create))
        //    {
        //        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //        bformatter.Serialize(stream, TableTempData.TbList);
        //    }
        //}

    }
}
