using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorSignalRChatApp.Client.Pages
{
    public partial class Chat
    {
        [Parameter]
        public string? username { get; set; }

        private HubConnection? hubConnection;
        private List<string> messages = new List<string>();
        private string? _message;
        private string? ReceiveUser;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(navManager.ToAbsoluteUri("/chathub"))
                .Build();

            hubConnection.On("ReceiveMessage", (Action<string, string>)((user, message) =>
            {
                _message = "";
                ReceiveUser = user;
                var encodedMsg = $"{user} : {message}";
                messages.Add(encodedMsg);
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

        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                _ = SendMessage();
                _message = "";
            }
        }
    }
}
