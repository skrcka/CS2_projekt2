using System;
using System.Collections.ObjectModel;

namespace DenRozeWeb.Models
{   
    public class BasketModel
    {
        public OrderModel Order { get; set; }
        public ObservableCollection<BillItemModel> BillItems { get; set; }
        public UserModel User { get; set; }
    }
}
