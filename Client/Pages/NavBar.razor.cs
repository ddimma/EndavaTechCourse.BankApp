using System;
namespace EndavaTechCourse.BankApp.Client.Pages
{
	public partial class NavBar
	{
        bool open = false;

        void ToggleDrawer()
        {
            open = !open;
        }
    }
}

