
namespace MicroblogServer.Api.Models
{
    public class FollowModel
    {
        public string UserId { get; set; }
        public string UserToFollowId { get; set; }
    }
}