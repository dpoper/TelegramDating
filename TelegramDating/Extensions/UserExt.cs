using TelegramDating.Model;

namespace TelegramDating.Extensions
{
    public static class UserExt
    {
        public static bool IsCreatingProfile(this User currentUser)
        {
            return currentUser.ProfileCreatingState.HasValue
                   || (!currentUser.ProfileCreatingState.HasValue &&
                       (  currentUser.Name == null
                       || currentUser.City == null
                       || currentUser.Country == null));
        }
    }
}
