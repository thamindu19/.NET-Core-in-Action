﻿namespace EfScmDataAccess
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public PartType Part { get; set; }
        public int Count { get; set; }
        public int OrderThreshold { get; set; }
    }
}
