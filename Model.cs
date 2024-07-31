using Syncfusion.Windows.Shared;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticSearch
{
    public class ProductInfo : NotificationObject
    {
        private int _ProductID;        

        private string _ProductName;

        private bool _Availability;

        private double _Price;

        private int _shippingDays;


        [Display(Name = "Product ID")]
        public int ProductID
        {
            get
            {
                return this._ProductID;
            }
            set
            {
                this._ProductID = value;
                this.RaisePropertyChanged("ProductID");
            }
        }

        [Display(Name ="Product Name")]
        public string ProductName
        {
            get
            {
                return this._ProductName;
            }
            set
            {
                this._ProductName = value;
                this.RaisePropertyChanged("ProductName");
            }
        }

        [Display(Name = "Availability")]
        public bool Availability
        {
            get
            {
                return this._Availability;
            }
            set
            {
                this._Availability = value;
                this.RaisePropertyChanged("Availability");
            }
        }

        [Display( Name = "Price")]
        public double Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
                this.RaisePropertyChanged("Price");
            }
        }

        [Display(Name = "Shipping Days")]
        public int ShippingDays
        {
            get
            {
                return this._shippingDays;
            }
            set
            {
                this._shippingDays = value;
                this.RaisePropertyChanged("ShippingDays");
            }
        }
      
    }
}
