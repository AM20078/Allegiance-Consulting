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
            //Setup server connection
            hubConnection = new HubConnectionBuilder()
                .WithUrl(navManager.ToAbsoluteUri("/chathub"))
                .Build();

            //Retrieve message from server
            hubConnection.On("ReceiveMessage", (Action<string, string>)((user, message) =>
            {
                _message = "";
                bool isSender = user.Equals(username, StringComparison.OrdinalIgnoreCase);
                messages.Add(new Message(user, message, isSender));
                StateHasChanged();
            }));

            await hubConnection.StartAsync();
        }

        //Send message to server
        private async Task SendMessage()
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", username, _message);
            }
        }

        //Check if user is connected to server
        public bool Connected =>
            hubConnection?.State == HubConnectionState.Connected;
    }
}
