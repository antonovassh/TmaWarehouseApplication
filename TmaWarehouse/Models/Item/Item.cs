using Application.Models.ItemModel;

namespace Application.Models

{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ItemGroup Group { get; set; }

        public int GroupId { get; set; }

        public ItemMeasurement Measurement { get; set; }

        public int MeasurementId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public string Status { get; set; }

        public string? StorageLocation { get; set; }

        public string? ContactPerson { get; set; }

        public string? Photo { get; set; }
    }
}
