using System;
using MudBlazor;

namespace EndavaTechCourse.BankApp.Client.Pages
{
	public partial class DropZone
	{
        private void ItemUpdated(MudItemDropInfo<DropItem> dropItem)
        {
            dropItem.Item.Identifier = dropItem.DropzoneIdentifier;
        }

        private List<DropItem> _items = new()
    {
        new DropItem(){ Name = "Drag me!", Identifier = "Drop Zone 1" },
        new DropItem(){ Name = "Or me!", Identifier = "Drop Zone 2" },
        new DropItem(){ Name = "Just Mud", Identifier = "Drop Zone 1" },
    };

        public class DropItem
        {
            public string Name { get; init; }
            public string Identifier { get; set; }
        }
    }
}

