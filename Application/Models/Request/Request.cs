﻿using Application.Models.ItemModel;

namespace Application.Models.Request;


public class Request
{
    public int Id { get; set; }

    public string EmployeeName { get; set; }

    public Item Item { get; set; }

    public int ItemId { get; set; }

    public ItemMeasurement Measurement { get; set; }

    public int MeasurementId { get; set; }

    public int Quantity { get; set; }

    //public int Price { get; set; } 

    public string? Comment { get; set; }

    public string? Status { get; set; }

}
