namespace WidgetScmDataAccess {
    public class InventoryItem {
        public int PartTypeId {get; set; }
        // Direct access to part type
        public PartType Part {get; set; }
        public int Count {get; set; }
        public int OrderThreshold {get; set; }
    }
}