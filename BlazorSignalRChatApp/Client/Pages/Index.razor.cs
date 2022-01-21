namespace BlazorSignalRChatApp.Client.Pages
{
    public partial class Index
    {
        private string? username;

        //Send username to chat page
        private void OnClick()
        {
            navManager.NavigateTo($"chat/{username}");
        }
    }
}
