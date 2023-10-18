using System;
using MudBlazor;

namespace EndavaTechCourse.BankApp.Client.Pages
{
	public partial class Tabs
	{
        public Position Position { get; set; } = Position.Left;

        private void OnSelectedValue(Position value)
        {
            switch (value)
            {
                case Position.Top:
                    Position = Position.Top;
                    break;
                case Position.Start:
                    Position = Position.Start;
                    break;
                case Position.Left:
                    Position = Position.Left;
                    break;
                case Position.Right:
                    Position = Position.Right;
                    break;
                case Position.End:
                    Position = Position.End;
                    break;
                case Position.Bottom:
                    Position = Position.Bottom;
                    break;
            }
        }
    }
}

