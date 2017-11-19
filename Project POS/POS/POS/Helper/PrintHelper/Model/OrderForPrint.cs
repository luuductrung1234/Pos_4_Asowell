using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;
using POS.Entities.CustomEntities;
using POS.Repository.DAL;

namespace POS.Helper.PrintHelper.Model
{
    public class OrderForPrint
    {
        public string No { get; set; }
        public string Casher { get; set; }
        public string Customer { get; set; }
        public int Table { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal CustomerPay { get; set; }
        public decimal PayBack { get; set; }

        public List<OrderDetailsForPrint> OrderDetails { get; set; }

        public Entities.Table TableOwned { get; set; }

        public OrderForPrint()
        {
            OrderDetails = new List<OrderDetailsForPrint>();
        }

        /// <summary>
        /// Convert data of OrderTemp object to OrderForPrint
        /// </summary>
        /// <param name="table">target table that contain order</param>
        public OrderForPrint GetAndConvertOrder(Entities.Table targetTable, EmployeewsOfAsowell unitofwork)
        {
            TableOwned = targetTable;

            var targetOrder = unitofwork.OrderTempRepository.Get(x => x.TableOwned == targetTable.TableId).First();
            No = targetOrder.OrdertempId.ToString();
            Casher = targetOrder.EmpId;
            Customer = targetOrder.EmpId;
            Table = targetTable.TableNumber;
            Date = targetOrder.Ordertime;
            TotalPrice = targetOrder.TotalPrice;
            CustomerPay = targetOrder.CustomerPay;
            PayBack = targetOrder.PayBack;

            return this;
        }

        /// <summary>
        /// Convert the list of OrderDetailsTemp's data to OrderDetailForPrint
        /// </summary>
        /// <param name="targetTable"></param>
        /// <param name="unitofwork"></param>
        /// <returns></returns>
        public OrderForPrint GetAndConverOrderDetails(Entities.Table targetTable, EmployeewsOfAsowell unitofwork)
        {
            // get Chairs data from target Table
            var targetChairs = unitofwork.ChairRepository.Get(x => x.TableOwned == targetTable.TableId);

            // get OrderDetailsTemp data from target Table
            List<OrderDetailsTemp> targetOrderDetails = new List<OrderDetailsTemp>();
            foreach (var chair in targetChairs)
            {
                targetOrderDetails.AddRange(unitofwork.OrderDetailsTempRepository.Get(x => x.ChairId == chair.ChairId));
            }
           
            // convert
            foreach (var orderDetailsTemp in targetOrderDetails)
            {
                OrderDetails.Add(new OrderDetailsForPrint()
                {
                    Quan = orderDetailsTemp.Quan,
                    ProductName = unitofwork.ProductRepository.Get(p => p.ProductId == orderDetailsTemp.ProductId).First().Name,
                    ProductPrice = unitofwork.ProductRepository.Get(p => p.ProductId == orderDetailsTemp.ProductId).First().Price,

                    ProductId = orderDetailsTemp.ProductId,
                    ProductType = unitofwork.ProductRepository.Get(p => p.ProductId == orderDetailsTemp.ProductId).First().Type,
                    ChairNumber = unitofwork.ChairRepository.Get(c => c.ChairId == orderDetailsTemp.ChairId).First().ChairNumber,
                    Note = orderDetailsTemp.Note,
                    SelectedStats = orderDetailsTemp.SelectedStats,
                });
                
            }


            return this;
        }




        /// <summary>
        /// Apply the Receipt Printing to OrderDetails
        /// </summary>
        /// <returns></returns>
        public List<OrderDetailsForPrint> GetOrderDetailsForReceipt()
        {
            var resultList = new List<OrderDetailsForPrint>();

            if (TableOwned != null)
            {
                foreach (var resultItem in OrderDetails)
                {
                    bool ishad = false;
                    foreach (var periodItem in resultList)
                    {
                        if (periodItem.ProductId == resultItem.ProductId)
                        {
                            periodItem.Quan += resultItem.Quan;

                            ishad = true;
                            break;
                        }
                    }

                    if (!ishad)
                    {
                        resultList.Add(resultItem);
                    }
                }
            }
                

            return resultList;
        }

        /// <summary>
        /// Apply the Kitchen Printing to OrderDetails
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<int, List<OrderDetailsForPrint>>> GetOrderDetailsForKitchen()
        {
            var resultList = new Dictionary<string, Dictionary<int, List<OrderDetailsForPrint>>>();

            if (TableOwned != null)
            {
                //Starter
                var starterQuery = OrderDetails.Where(od => od.SelectedStats == "Starter").ToList();
                var starterList = new Dictionary<int, List<OrderDetailsForPrint>>();
                for (int i = 1; i <= TableOwned.ChairAmount; i++)
                {
                    var chairStartQuery = starterQuery.Where(od => od.ChairNumber == i).ToList();
                    starterList.Add(i, chairStartQuery);
                }
                resultList.Add("Starter", starterList);

                //Main
                var mainQuery = OrderDetails.Where(od => od.SelectedStats == "Main").ToList();
                var mainList = new Dictionary<int, List<OrderDetailsForPrint>>();
                for (int i = 1; i <= TableOwned.ChairAmount; i++)
                {
                    var chairMainCostQuery = mainQuery.Where(od => od.ChairNumber == i).ToList();
                    mainList.Add(i, chairMainCostQuery);
                }
                resultList.Add("Main", mainList);


                //Dessert
                var dessertQuery = OrderDetails.Where(od => od.SelectedStats == "Dessert").ToList();
                var dessertList = new Dictionary<int, List<OrderDetailsForPrint>>();
                for (int i = 1; i <= TableOwned.ChairAmount; i++)
                {
                    var chairDessertQuery = dessertQuery.Where(od => od.ChairNumber == i).ToList();
                    dessertList.Add(i, chairDessertQuery);
                }
                resultList.Add("Dessert", dessertList);
            }
            


            return resultList;
        }

        /// <summary>
        /// Apply the Bar Printing to OrderDetails
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<int, List<OrderDetailsForPrint>>> GetOrderDetailsForBar()
        {
            var resultList = new Dictionary<string, Dictionary<int, List<OrderDetailsForPrint>>>();


            if (TableOwned != null)
            {
                //Beverage
                var drinkQuery = OrderDetails.Where(od => od.SelectedStats == "Drink").ToList();


                //Coffee
                var coffeeQuery = drinkQuery.Where(od => od.ProductType == (int) ProductType.Coffee).ToList();
                var coffeeList = new Dictionary<int, List<OrderDetailsForPrint>>();
                for (int i = 1; i <= TableOwned.ChairAmount; i++)
                {
                    var chairCoffeeQuery = coffeeQuery.Where(od => od.ChairNumber == i).ToList();
                    coffeeList.Add(i, chairCoffeeQuery);
                }
                resultList.Add(ProductType.Coffee.ToString(), coffeeList);

                //Beverage
                var beverageQuery = drinkQuery.Where(od => od.ProductType == (int)ProductType.Beverage).ToList();
                var beverageList = new Dictionary<int, List<OrderDetailsForPrint>>();
                for (int i = 1; i <= TableOwned.ChairAmount; i++)
                {
                    var chairBeverageQuery = beverageQuery.Where(od => od.ChairNumber == i).ToList();
                    beverageList.Add(i, chairBeverageQuery);
                }
                resultList.Add(ProductType.Beverage.ToString(), beverageList);


                //Cocktall
                var cocktailQuery = drinkQuery.Where(od => od.ProductType == (int)ProductType.Cocktail).ToList();
                var cocktailList = new Dictionary<int, List<OrderDetailsForPrint>>();
                for (int i = 1; i <= TableOwned.ChairAmount; i++)
                {
                    var chairCocktailQuery = cocktailQuery.Where(od => od.ChairNumber == i).ToList();
                    cocktailList.Add(i, chairCocktailQuery);
                }
                resultList.Add(ProductType.Cocktail.ToString(), cocktailList);


                //Wine
                var wineQuery = drinkQuery.Where(od => od.ProductType == (int)ProductType.Wine).ToList();
                var windList = new Dictionary<int, List<OrderDetailsForPrint>>();
                for (int i = 1; i <= TableOwned.ChairAmount; i++)
                {
                    var chairWineQuery = wineQuery.Where(od => od.ChairNumber == i).ToList();
                    windList.Add(i, chairWineQuery);
                }
                resultList.Add(ProductType.Wine.ToString(), windList);


                //Beer
                var beerQuery = drinkQuery.Where(od => od.ProductType == (int)ProductType.Beer).ToList();
                var beerList = new Dictionary<int, List<OrderDetailsForPrint>>();
                for (int i = 1; i <= TableOwned.ChairAmount; i++)
                {
                    var chairBeerQuery = beerQuery.Where(od => od.ChairNumber == i).ToList();
                    beerList.Add(i, chairBeerQuery);
                }
                resultList.Add(ProductType.Beer.ToString(), beerList);
            }

            return resultList;
        }





        // Receipt Meta
        public Dictionary<string, string> getMetaReceiptInfo()
        {
            return new Dictionary<string, string>()
            {
                { "No", No},
                { "Table", Table.ToString()},
                { "Date", Date.ToString() },
                { "Casher", Casher},
                { "Customer", Customer}
            };
        }
        public string[] getMetaReceiptTable()
        {
            return new string[]
            {
                "Product Price",
                "Quan",
                "Price",
                "Amt"
            };
        }

        // Kitchen Meta
        public string[] getMetaKitchenTable()
        {
            return new string[]
            {
                "Position",
                "Qty",
                "Product",
            };
        }

        // Bar Meta
        public string[] getMetaBarTable()
        {
            return new string[]
            {
                "Position",
                "Qty",
                "Product",
            };
        }
    }
}
