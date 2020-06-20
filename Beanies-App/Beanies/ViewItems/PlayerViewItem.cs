using System;
using Beanies.Models;
using Beanies.Styles;
using Xamarin.Forms;

namespace Beanies.ViewItems
{
    public class PlayerViewItem
    {
        public PlayerViewItem()
        {
        }

        public PlayerViewItem(User user)
        {
            Name = user.Name;
            Id = user.Id;
        }

        public Command<string> SelectedCommand { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public bool Selected { get; set; }

        public string Icon => GetSelectedIcon();
        private string GetSelectedIcon()
        {
            return Selected ? IconFont.CheckBold : "";
        }

        public void ToggleSelected()
        {
            Selected = !Selected;
        }
    }
}
