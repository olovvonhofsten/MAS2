
namespace MirrorAlignmentSystem
{
    /// <summary>
    ///  This class handles the current user that is logged in and makes the application able to get user information fast without 
    ///  asking for information in the database
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>
        /// The current user 
        /// </summary>
        static string currentUser;

        /// <summary>
        /// The type of the current user
        /// </summary>
        static string currentUserType;

        /// <summary>
        /// Sets the username of the current logged in user.
        /// </summary>
        /// <param name="username">The username of the user</param>
        public static void SetCurrentUser(string username) 
        {
            currentUser = username;
        }

        /// <summary>
        /// Gets the current logged in user
        /// </summary>   
        /// <returns>
        /// Returns a string with the username of the currently logged in user
        /// </returns>
        public static string GetCurrentUser()
        {
            return currentUser;
        }

        /// <summary>
        /// Sets the user type of the current logged in user
        /// </summary>
        /// <param name="userType">The user type to set</param>   
        public static void SetCurrentUserType(string userType)
        {
            currentUserType = userType;
        }

        /// <summary>
        /// Gets the user type of the currently logged in user
        /// </summary>   
        /// <returns>
        /// Returns a string with the user type of the currently logged in user
        /// </returns>
        public static string GetCurrentUserType()
        {
            return currentUserType;
        }

        /// <summary>
        /// If the user logs out the current user and current user type are reseted
        /// </summary>   
        public static void Logout() 
        {
            currentUser = "";
            currentUserType = "";
        }
    }
}
