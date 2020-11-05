using DaySlayer.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DaySlayer.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        int CountUserType(int userTypeId);
        List<UserProfile> GetAll();
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetUserById(int id);
        void isActive(UserProfile user);
        void isAdmin(UserProfile user);
        UserProfile UserProfileBuilder(SqlDataReader reader);
    }
}