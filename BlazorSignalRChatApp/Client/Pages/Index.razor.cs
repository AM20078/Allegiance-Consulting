namespace BlazorSignalRChatApp.Client.Pages
{
    public partial class Index
    {
        private string? username;
        private void OnClick()
        {
            navManager.NavigateTo($"chat/{username}");
        }
    }
}
