using System;
using System.Collections.Generic;

namespace BO;

public partial class SilverJewelry
{
    public int SilverJewelryId { get; set; }

    public string SilverJewelryName { get; set; } = null!;

    public string? SilverJewelryDescription { get; set; }

    public decimal? MetalWeight { get; set; }

    public decimal? Price { get; set; }

    public int? ProductionYear { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
