using System.Configuration;
using System.Linq;
using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel;

namespace ForumWeb.Common
{
    public class StudioForumHelper
    {
        private static string _forumlist;
        private static string[] _queryStringlist;

        public static bool IsModeratorAndStudioForum(Post post)
        {
            return IsModeratorAndStudioForum(post.User, post.Topic.Category);
        }

        public static bool IsModeratorAndStudioForum(Topic topic)
        {
            return IsModeratorAndStudioForum(topic.User, topic.Category);
        }

        private static bool IsModeratorAndStudioForum(MembershipUser user, Category category)
        {
            return IsStudioForum(category.Name) && user.Roles.Any(x => x.RoleName.Equals(AppConstants.ModeratorRoleName));
        }

        public static bool IsStudioForum(string categoryName)
        {
            _forumlist = ConfigurationManager.AppSettings["ForumList"];
            return _forumlist.Split(',').Contains(categoryName);
        }

        public static bool IsStudioForumInQueryString(string queryString)
        {
            _queryStringlist = ConfigurationManager.AppSettings["QueryStrings"].Split(',');
            foreach (var item in _queryStringlist)
            {
                if (queryString.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }


    }
}
