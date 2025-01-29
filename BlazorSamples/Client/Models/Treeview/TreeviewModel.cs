namespace BlazorTemplate.Client.Models.Treeview
{
    public class TreeviewModel
    {   
        public int NodeId { get; set; }
        
        public string DisplayText { get; set; } = string.Empty;

        public List<TreeviewModel> Nodes { get; set; } = new();
    }
}
