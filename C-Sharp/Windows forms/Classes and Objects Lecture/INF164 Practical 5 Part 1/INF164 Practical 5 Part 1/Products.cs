using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF164_Practical_5_Part_1
{
    // STEP 2: Make class public
    public class Products
    {
        //Data attributes called data members
        private string mProductCode;
        private string mProductName;
        private double mUnitPrice;
        private int mUnitsinStock;

        
        //Optionally properties
        public string ProductCode { get => mProductCode; set => mProductCode = value; }
        public string ProductName { get => mProductName; set => mProductName = value; }
        public double UnitPrice { get => mUnitPrice; set => mUnitPrice = value; }
        public int UnitsinStock { get => mUnitsinStock; set => mUnitsinStock = value; }


        //Optionally methods(that uses or processes the data attributes)
        public Products()
        {
            //mProductCode = "";
            //mProductName = "";
            //mUnitPrice = 0.00;
            //mUnitsinStock = 0;
        }

        public Products(string ProductCode, string ProductName, double UnitPrice, int UnitInStock)
        {
            mProductCode = ProductCode;
            mProductName = ProductName;
            mUnitPrice = UnitPrice;
            mUnitsinStock = UnitInStock;
        }

        
        //Optionally events(mostly with interface classes, seldom with entity classes)
        public bool CheckStock()
        {
            if (mUnitsinStock <= 20)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


    }


}
