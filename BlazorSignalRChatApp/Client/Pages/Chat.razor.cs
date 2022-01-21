using Microsoft.AspNetCore.Components;

namespace BlazorSignalRChatApp.Client.Pages
{
    public partial class Chat
    {
        [Parameter]
        public string? username { get; set; }
    }
}
