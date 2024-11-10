using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BO;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryDescription { get; set; } = null!;

    public string? FromCountry { get; set; }

    [JsonIgnore]
    public virtual ICollection<SilverJewelry> SilverJewelries { get; set; } = new List<SilverJewelry>();
}
