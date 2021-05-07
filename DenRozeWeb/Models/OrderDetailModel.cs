using System;
using System.Collections.ObjectModel;

namespace DenRozeWeb.Models
{   
    public class OrderDetailModel
    {
        public OrderModel Order { get; set; }
        public ObservableCollection<BillItemModel> BillItems { get; set; }
        public ObservableCollection<ItemModel> Items { get; set; }
    }
}
