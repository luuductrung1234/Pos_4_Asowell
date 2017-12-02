using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.Test.RepositoryTest
{
    [TestClass]
    public class GenericRepositoryTest
    {
        private EmployeewsOfCloudPOS _unitofwork = new EmployeewsOfCloudPOS();

        [TestMethod]
        public void AutoGeneteId_DBAsowell_Test()
        {
            Product entity = new Product();
            string sign = "";
            if (entity is Employee)
            {
                sign = "EMP";
            }
            else if (entity is AdminRe)
            {
                sign = "AD";
            }
            else if (entity is Customer)
            {
                sign = "CUS";
            }
            else if (entity is WareHouse)
            {
                sign = "WAH";
            }
            else if (entity is Ingredient)
            {
                sign = "IGD";
            }
            else if (entity is Product)
            {
                sign = "P";
            }
            else if (entity is ProductDetail)
            {
                sign = "PD";
            }
            else if (entity is OrderNote)
            {
                sign = "ORD";
            }
            else if (entity is ReceiptNote)
            {
                sign = "RN";
            }
            else if (entity is SalaryNote)
            {
                sign = "SAN";
            }
            else if (entity is WorkingHistory)
            {
                sign = "WOH";
            }

            // lấy số thứ tự mới nhất
            string numberWantToset = (_unitofwork.ProductRepository.Get().Count() + 1).ToString();

            int blank = 10 - (sign.Length + numberWantToset.Length);
            string result = sign;
            for (int i = 0; i < blank; i++)
            {
                result += "0";
            }
            result += numberWantToset;

            Assert.AreEqual("P000000058", result);
        }
    }
}
