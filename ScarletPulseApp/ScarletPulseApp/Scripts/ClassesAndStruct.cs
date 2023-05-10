using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletPulseApp.ClassesAndStruct {
    public struct MenuItems {
        public string IconPath;
        public string Title;
        public string Link;
        public MenuItems(string IconPath, string Title, string Link) {
            this.IconPath = IconPath;
            this.Title = Title;
            this.Link = Link;
        }
    }

    public static class AppState {
        public static string Name = "placeholder";
        public static string Icon = "\ue900";
    }
}
