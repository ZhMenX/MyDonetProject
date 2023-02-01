using Microsoft.AspNetCore.Authorization;

namespace DonetWeb2.Authorization
{
    public class MusicRequirement : IAuthorizationRequirement
    {
        public MusicRequirement(List<string> Permissions )
        {
            this.Permissions = Permissions;
        }

        public List<string> Permissions { get; set; }


    }
}
