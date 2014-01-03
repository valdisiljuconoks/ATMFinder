using System.Collections.ObjectModel;

namespace Viiar.AtmFinder.W8.DataSources
{
    public sealed class SampleDataSource
    {
        private readonly ObservableCollection<EntityDataGroup> allGroups = new ObservableCollection<EntityDataGroup>();

        public SampleDataSource()
        {
            string ITEM_CONTENT = string.Format(
                                                "Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}", 
                                                "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var group1 = new EntityDataGroup(
                    "Group-1", 
                    "Group Title: 1", 
                    "Group Subtitle: 1", 
                    "Assets/DarkGray.png", 
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group1.Items.Add(
                             new EntityDataItem(
                                     "Group-1-Item-1", 
                                     "Item Title: 1", 
                                     "Item Subtitle: 1", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT, 
                                     "Chain name",
                                     0,
                                     0,
                                     group1));
            group1.Items.Add(
                             new EntityDataItem(
                                     "Group-1-Item-2", 
                                     "Item Title: 2", 
                                     "Item Subtitle: 2", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group1));
            group1.Items.Add(
                             new EntityDataItem(
                                     "Group-1-Item-3", 
                                     "Item Title: 3", 
                                     "Item Subtitle: 3", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group1));
            group1.Items.Add(
                             new EntityDataItem(
                                     "Group-1-Item-4", 
                                     "Item Title: 4", 
                                     "Item Subtitle: 4", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group1));
            group1.Items.Add(
                             new EntityDataItem(
                                     "Group-1-Item-5", 
                                     "Item Title: 5", 
                                     "Item Subtitle: 5", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group1));
            AllGroups.Add(group1);

            var group2 = new EntityDataGroup(
                    "Group-2", 
                    "Group Title: 2", 
                    "Group Subtitle: 2", 
                    "Assets/LightGray.png", 
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group2.Items.Add(
                             new EntityDataItem(
                                     "Group-2-Item-1", 
                                     "Item Title: 1", 
                                     "Item Subtitle: 1", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group2));
            group2.Items.Add(
                             new EntityDataItem(
                                     "Group-2-Item-2", 
                                     "Item Title: 2", 
                                     "Item Subtitle: 2", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group2));
            group2.Items.Add(
                             new EntityDataItem(
                                     "Group-2-Item-3", 
                                     "Item Title: 3", 
                                     "Item Subtitle: 3", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group2));
            AllGroups.Add(group2);

            var group3 = new EntityDataGroup(
                    "Group-3", 
                    "Group Title: 3", 
                    "Group Subtitle: 3", 
                    "Assets/MediumGray.png", 
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group3.Items.Add(
                             new EntityDataItem(
                                     "Group-3-Item-1", 
                                     "Item Title: 1", 
                                     "Item Subtitle: 1", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group3));
            group3.Items.Add(
                             new EntityDataItem(
                                     "Group-3-Item-2", 
                                     "Item Title: 2", 
                                     "Item Subtitle: 2", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group3));
            group3.Items.Add(
                             new EntityDataItem(
                                     "Group-3-Item-3", 
                                     "Item Title: 3", 
                                     "Item Subtitle: 3", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group3));
            group3.Items.Add(
                             new EntityDataItem(
                                     "Group-3-Item-4", 
                                     "Item Title: 4", 
                                     "Item Subtitle: 4", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group3));
            group3.Items.Add(
                             new EntityDataItem(
                                     "Group-3-Item-5", 
                                     "Item Title: 5", 
                                     "Item Subtitle: 5", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group3));
            group3.Items.Add(
                             new EntityDataItem(
                                     "Group-3-Item-6", 
                                     "Item Title: 6", 
                                     "Item Subtitle: 6", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group3));
            group3.Items.Add(
                             new EntityDataItem(
                                     "Group-3-Item-7", 
                                     "Item Title: 7", 
                                     "Item Subtitle: 7", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group3));
            AllGroups.Add(group3);

            var group4 = new EntityDataGroup(
                    "Group-4", 
                    "Group Title: 4", 
                    "Group Subtitle: 4", 
                    "Assets/LightGray.png", 
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group4.Items.Add(
                             new EntityDataItem(
                                     "Group-4-Item-1", 
                                     "Item Title: 1", 
                                     "Item Subtitle: 1", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group4));
            group4.Items.Add(
                             new EntityDataItem(
                                     "Group-4-Item-2", 
                                     "Item Title: 2", 
                                     "Item Subtitle: 2", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group4));
            group4.Items.Add(
                             new EntityDataItem(
                                     "Group-4-Item-3", 
                                     "Item Title: 3", 
                                     "Item Subtitle: 3", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group4));
            group4.Items.Add(
                             new EntityDataItem(
                                     "Group-4-Item-4", 
                                     "Item Title: 4", 
                                     "Item Subtitle: 4", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group4));
            group4.Items.Add(
                             new EntityDataItem(
                                     "Group-4-Item-5", 
                                     "Item Title: 5", 
                                     "Item Subtitle: 5", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group4));
            group4.Items.Add(
                             new EntityDataItem(
                                     "Group-4-Item-6", 
                                     "Item Title: 6", 
                                     "Item Subtitle: 6", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group4));
            AllGroups.Add(group4);

            var group5 = new EntityDataGroup(
                    "Group-5", 
                    "Group Title: 5", 
                    "Group Subtitle: 5", 
                    "Assets/MediumGray.png", 
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group5.Items.Add(
                             new EntityDataItem(
                                     "Group-5-Item-1", 
                                     "Item Title: 1", 
                                     "Item Subtitle: 1", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group5));
            group5.Items.Add(
                             new EntityDataItem(
                                     "Group-5-Item-2", 
                                     "Item Title: 2", 
                                     "Item Subtitle: 2", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group5));
            group5.Items.Add(
                             new EntityDataItem(
                                     "Group-5-Item-3", 
                                     "Item Title: 3", 
                                     "Item Subtitle: 3", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group5));
            group5.Items.Add(
                             new EntityDataItem(
                                     "Group-5-Item-4", 
                                     "Item Title: 4", 
                                     "Item Subtitle: 4", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group5));
            AllGroups.Add(group5);

            var group6 = new EntityDataGroup(
                    "Group-6", 
                    "Group Title: 6", 
                    "Group Subtitle: 6", 
                    "Assets/DarkGray.png", 
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group6.Items.Add(
                             new EntityDataItem(
                                     "Group-6-Item-1", 
                                     "Item Title: 1", 
                                     "Item Subtitle: 1", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group6));
            group6.Items.Add(
                             new EntityDataItem(
                                     "Group-6-Item-2", 
                                     "Item Title: 2", 
                                     "Item Subtitle: 2", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group6));
            group6.Items.Add(
                             new EntityDataItem(
                                     "Group-6-Item-3", 
                                     "Item Title: 3", 
                                     "Item Subtitle: 3", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group6));
            group6.Items.Add(
                             new EntityDataItem(
                                     "Group-6-Item-4", 
                                     "Item Title: 4", 
                                     "Item Subtitle: 4", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group6));
            group6.Items.Add(
                             new EntityDataItem(
                                     "Group-6-Item-5", 
                                     "Item Title: 5", 
                                     "Item Subtitle: 5", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group6));
            group6.Items.Add(
                             new EntityDataItem(
                                     "Group-6-Item-6", 
                                     "Item Title: 6", 
                                     "Item Subtitle: 6", 
                                     "Assets/MediumGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group6));
            group6.Items.Add(
                             new EntityDataItem(
                                     "Group-6-Item-7", 
                                     "Item Title: 7", 
                                     "Item Subtitle: 7", 
                                     "Assets/DarkGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group6));
            group6.Items.Add(
                             new EntityDataItem(
                                     "Group-6-Item-8", 
                                     "Item Title: 8", 
                                     "Item Subtitle: 8", 
                                     "Assets/LightGray.png", 
                                     "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.", 
                                     ITEM_CONTENT,
                                     "Chain name",
                                     0,
                                     0,
                                     group6));
            AllGroups.Add(group6);
        }

        public ObservableCollection<EntityDataGroup> AllGroups { get { return this.allGroups; } }
    }
}
