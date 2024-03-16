using Application.Models.ItemModel;
using System.Diagnostics.Metrics;

namespace Application.Models.Request

{
    public class Request
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public int ItemId { get; set; }

        public ItemMeasurement Measurement { get; set; }

        public int MeasurementId { get; set; }

        public int Quantity { get; set; }

        //public int Price { get; set; } пускай общий считается прайс

        public string? Comment { get; set; }

        public string? Status { get; set; }

    }
}
