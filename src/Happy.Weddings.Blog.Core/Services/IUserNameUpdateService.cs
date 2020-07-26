using Happy.Weddings.Blog.Core.DTO;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Core.Services
{
    /// <summary>
    /// Interface to update the user details in all the stories
    /// </summary>
    public interface IUserNameUpdateService
    {
        /// <summary>
        /// Updates the user name in stories.
        /// </summary>
        /// <param name="updateUserFullNameModel">The update user full name model.</param>
        Task UpdateUserNameInStories(UpdateUserFullNameModel updateUserFullNameModel);
    }
}
