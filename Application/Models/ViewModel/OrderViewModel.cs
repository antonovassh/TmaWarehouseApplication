using Application.Models.ItemModel;

namespace TmaWarehouse.Models.ViewModel
{
    public class OrderViewModel
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public ItemMeasurement Measurement { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public string Comment { get; internal set; }
    }
}
