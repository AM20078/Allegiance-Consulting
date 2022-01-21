using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using BlazorSignalRChatApp.Shared;

namespace BlazorSignalRChatApp.Client.Pages
{
    public partial class Chat
    {
        [Parameter]
        public string? username { get; set; }

        private HubConnection? hubConnection;
        private List<Message> messages = new List<Message>();
        private string? _message;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(navManager.ToAbsoluteUri("/chathub"))
                .Build();

            hubConnection.On("ReceiveMessage", (Action<string, string>)((user, message) =>
            {
                _message = "";
                bool isSender = user.Equals(username, StringComparison.OrdinalIgnoreCase);
                messages.Add(new Message(user, message, isSender));
                StateHasChanged();
            }));

            await hubConnection.StartAsync();
        }

        private async Task SendMessage()
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", username, _message);
            }
        }
        public bool Connected =>
            hubConnection?.State == HubConnectionState.Connected;
    }
}
