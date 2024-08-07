using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticSearch
{
    public class ProductInfoViewModel : NotificationObject
    {

        Random r = new Random();     

        public ProductInfoViewModel()
        {
            _ProductDetails = GetProductDetails();
        }


        private List<ProductInfo> _ProductDetails;

        /// <summary>
        /// Gets or sets the orders details.
        /// </summary>
        /// <value>The orders details.</value>
        public List<ProductInfo> ProductDetails
        {
            get { return _ProductDetails; }
            set
            {
                _ProductDetails = value;
                RaisePropertyChanged("ProductDetails");
            }
        }

        /// <summary>
        /// Gets the orders details.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public  List<ProductInfo> GetProductDetails()
        {
            List<ProductInfo> productDetails = new List<ProductInfo>();
            productDetails.Add(new ProductInfo() { ProductID = 1001, ProductName = "KnifeSet", Availability = true, Price = r.Next(400, 600), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 1002, ProductName = "Measuring Cups", Availability = true, Price = r.Next(400, 600), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 1003, ProductName = "Toaster", Availability = false, Price = r.Next(4000, 6000), ShippingDays = 3 });
            productDetails.Add(new ProductInfo() { ProductID = 1004, ProductName = "Blender", Availability = true, Price = r.Next(4000, 6000), ShippingDays = 4 });

            productDetails.Add(new ProductInfo() { ProductID = 1005, ProductName = "CoffeeMaker", Availability = true, Price = r.Next(40000, 60000), ShippingDays = 5 });
            productDetails.Add(new ProductInfo() { ProductID = 1006, ProductName = "CuttingBoard", Availability = true, Price = r.Next(200, 500), ShippingDays = 2 });
          

            productDetails.Add(new ProductInfo() { ProductID = 2001, ProductName = "HardDisk", Availability = true, Price = r.Next(2000, 6000), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 2002, ProductName = "Laptop", Availability = true, Price = r.Next(40000, 60000), ShippingDays = 3 });
            productDetails.Add(new ProductInfo() { ProductID = 2003, ProductName = "Printers", Availability = true, Price = r.Next(40000, 60000), ShippingDays = 5 });
            productDetails.Add(new ProductInfo() { ProductID = 2004, ProductName = "Monitors", Availability = true, Price = r.Next(4000, 6000), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 2005, ProductName = "Wireless Mouse", Availability = true, Price = r.Next(400, 1000), ShippingDays = 1 });

            productDetails.Add(new ProductInfo() { ProductID = 3001, ProductName = "WashingMachine", Availability = true, Price = r.Next(20000, 60000), ShippingDays = 4 });
            productDetails.Add(new ProductInfo() { ProductID = 3002, ProductName = "Refrigerator", Availability = true, Price = r.Next(25000, 50000), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 3003, ProductName = "Wardrobe", Availability = true, Price = r.Next(10000, 50000), ShippingDays = 3 });
            productDetails.Add(new ProductInfo() { ProductID = 3004, ProductName = "DishWasher", Availability = true, Price = r.Next(40000, 60000), ShippingDays = 4});
            productDetails.Add(new ProductInfo() { ProductID = 3005, ProductName = "MicroWave", Availability = true, Price = r.Next(4000, 15000), ShippingDays = 2 });

            productDetails.Add(new ProductInfo() { ProductID = 4001, ProductName = "Table", Availability = true, Price = r.Next(4000, 6000), ShippingDays = 2 });
           

            productDetails.Add(new ProductInfo() { ProductID = 5001, ProductName = "Orange Juice", Availability = true, Price = r.Next(150, 200), ShippingDays = 1 });
            productDetails.Add(new ProductInfo() { ProductID = 5002, ProductName = "Almond Milk", Availability = true, Price = r.Next(150, 200), ShippingDays = 1 });
            productDetails.Add(new ProductInfo() { ProductID = 5003, ProductName = "Brown Rice", Availability = true, Price = r.Next(400, 600), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 5004, ProductName = "Chicken Breast", Availability = true, Price = r.Next(400, 600), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 5005, ProductName = "Organic Apples", Availability = true, Price = r.Next(300, 400), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 5006, ProductName = "Olive Oil", Availability = true, Price = r.Next(300, 600), ShippingDays = 2 });
            productDetails.Add(new ProductInfo() { ProductID = 5007, ProductName = "Whole Wheat Bread", Availability = true, Price = r.Next(200, 250), ShippingDays = 1 });
            productDetails.Add(new ProductInfo() { ProductID = 5008, ProductName = "Frozen Vegetables", Availability = true, Price = r.Next(200, 600), ShippingDays = 1 });                  
                      
            return productDetails;
        }

      

       
       

     

       

        
    }
}
