using BlazorTemplate.Client.Models.Treeview;

namespace BlazorTemplate.Client.Helpers.Treeview;

public static class CreateTreviewData
{

    public static TreeviewModel RootNode()
    {
        return new TreeviewModel
        {
            NodeId = 1,
            DisplayText = "<ROOT>",
            Nodes =
            {
                new TreeviewModel
                {
                    NodeId = 2,
                    DisplayText = "HEADING 1",
                    Nodes =
                    {
                        new TreeviewModel
                        { 
                            NodeId = 3,
                            DisplayText = "Sub Heading 1.1"
                        }
                    }
                },
                new TreeviewModel
                {
                    NodeId = 4,
                    DisplayText = "HEADING 2",
                    Nodes =
                    {
                        new TreeviewModel
                        {
                            NodeId = 5,
                            DisplayText = "Sub Heading 2.1",
                            Nodes = new List<TreeviewModel>
                            {
                                new TreeviewModel
                                {
                                    NodeId = 6,
                                    DisplayText = "Article 2.1.1"
                                },
                                new TreeviewModel
                                {
                                    NodeId = 7,
                                    DisplayText = "Article 2.1.2"
                                },
                            }
                        },
                        new TreeviewModel
                        {
                            NodeId = 8,
                            DisplayText = "Sub Heading 2.2",
                            Nodes = new List<TreeviewModel>
                            {
                                new TreeviewModel
                                {
                                    NodeId = 9,
                                    DisplayText = "Article 2.2.1"
                                },
                                new TreeviewModel
                                {
                                    NodeId = 10,
                                    DisplayText = "Article 2.2.2"
                                }
                            }
                        }
                    }
                },
            }
        };
    }
}
