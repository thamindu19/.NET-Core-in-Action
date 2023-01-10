namespace WidgetScmDataAccess {
    public class PartCommand {
        public int Id {get; set;}
        public int PartTypeId {get; set;}
        public PartType Part {get; set;}
        public int PartCount {get; set;}
        public PartCountOperation Command {get; set;}
    }

    // Enumerations are used for discrete sets of values.
    public enum PartCountOperation {
        Add,
        Remove
    }
}