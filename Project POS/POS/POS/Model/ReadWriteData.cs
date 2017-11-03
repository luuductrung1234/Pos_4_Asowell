using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace POS.Model
{
    public class ReadWriteData
    {
        //string a = System.IO.Directory.GetCurrentDirectory();
        static string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        private static List<Chair> chairTemp = new List<Chair>();

        //check file bin isExist
        public static bool checkBinFileExist()
        {
            return File.Exists(startupProjectPath + "\\SerializedData\\tableRuntimeHistory.bin");
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

        //write khi add new
        public static void writeOnAddNew(Rectangle rec)
        {
            Table newTable = new Table()
            {
                TableNumber = int.Parse(rec.Name.Substring(5)),
                ChairData = new List<Chair>(),
                Position = new Point(rec.Margin.Left, rec.Margin.Top),
                IsPinned = false,
                TableOrder = new OrderNote() { emp_id = (App.Current.Properties["EmpLogin"] as Employee).Emp_id, ordertable = int.Parse(rec.Name.Substring(5)), ordertime = DateTime.Now },
                TableOrderDetails = new List<OrderNoteDetails>()
            };

            for (int i = 0; i < newTable.ChairAmount; i++)
            {
                Chair newChair = new Chair();
                newChair.ChairNumber = i + 1;
                newChair.TableOfChair = newTable.TableNumber;
                newChair.ChairOrderDetails = new List<OrderNoteDetails>();
                newTable.ChairData.Add(newChair);
            }

            TableTempData.TbList.Add(newTable);

            writeToBinFile();
        }

        //write khi update
        public static void writeOnUpdateChair(Table table, List<Chair> chList, int chairAmount)
        {
            foreach (Table curTable in TableTempData.TbList)
            {
                if (curTable.TableNumber == table.TableNumber)
                {
                    curTable.TableNumber = table.TableNumber;
                    curTable.Position = table.Position;
                    curTable.ChairAmount = chairAmount;

                    curTable.ChairData = chList;

                    for (int i = chList.Count; i < chairAmount; i++)
                    {
                        Chair newChair = new Chair();
                        newChair.ChairNumber = i + 1;
                        newChair.TableOfChair = curTable.TableNumber;
                        newChair.ChairOrderDetails = new List<OrderNoteDetails>();
                        curTable.ChairData.Add(newChair);
                    }
                }
            }

            writeToBinFile();
        }

        //write khi co order
        public static void writeOnOrder(Rectangle rec)
        {

        }

        //write khi drag
        public static void writeOnDrag(Rectangle rec)
        {
            foreach (Table curTable in TableTempData.TbList)
            {
                if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
                {
                    curTable.TableNumber = int.Parse(rec.Name.Substring(5));
                    curTable.Position = new Point(rec.Margin.Left, rec.Margin.Top);
                }
            }

            writeToBinFile();
        }

        //write khi pin
        public static void writeOnPin(Rectangle rec)
        {
            foreach (Table curTable in TableTempData.TbList)
            {
                if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
                {
                    curTable.TableNumber = int.Parse(rec.Name.Substring(5));
                    curTable.Position = new Point(rec.Margin.Left, rec.Margin.Top);
                    curTable.IsPinned = true;
                }
            }

            writeToBinFile();
        }

        //write khi move(unpin)
        public static void writeOnMove(Rectangle rec)
        {
            foreach (Table curTable in TableTempData.TbList)
            {
                if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
                {
                    curTable.TableNumber = int.Parse(rec.Name.Substring(5));
                    curTable.Position = new Point(rec.Margin.Left, rec.Margin.Top);
                    curTable.IsPinned = false;
                }
            }

            writeToBinFile();
        }

        //write khi remove
        public static void writeOnRemove(Rectangle rec)
        {
            Table delTable = new Table();

            foreach (Table curTable in TableTempData.TbList)
            {
                if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
                {
                    delTable = curTable;
                    break;
                }
            }

            TableTempData.TbList.Remove(delTable);

            writeToBinFile();
        }

        //write khi pay
        public static void writeOnPay(Rectangle rec)
        {
            foreach (Table curTable in TableTempData.TbList)
            {
                if (curTable.TableNumber == int.Parse(rec.Name.Substring(5)))
                {
                    curTable.TableNumber = int.Parse(rec.Name.Substring(5));
                    curTable.Position = new Point(rec.Margin.Left, rec.Margin.Top);
                    curTable.TableOrder = new OrderNote() { emp_id = (App.Current.Properties["EmpLogin"] as Employee).Emp_id, ordertable = int.Parse(rec.Name.Substring(5)), ordertime = DateTime.Now };
                    curTable.TableOrderDetails = new List<OrderNoteDetails>();

                    for (int i = 0; i < curTable.ChairAmount; i++)
                    {
                        Chair newChair = new Chair();
                        newChair.ChairNumber = i + 1;
                        newChair.TableOfChair = curTable.TableNumber;
                        newChair.ChairOrderDetails = new List<OrderNoteDetails>();
                        curTable.ChairData.Add(newChair);
                    }
                    break;
                }
            }

            writeToBinFile();
        }

        //write to file tableRuntimeHistory.bin;
        public static void writeToBinFile()
        {
            string dir = startupProjectPath;
            string serializationFile = System.IO.Path.Combine(dir, "SerializedData\\tableRuntimeHistory.bin");

            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, TableTempData.TbList);
            }
        }

    }
}
