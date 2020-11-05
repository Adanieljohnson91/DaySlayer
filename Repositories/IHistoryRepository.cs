using DaySlayer.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaySlayer.Repositories
{
    public interface IHistoryRepository
    {
        HistoryTotalCount GetHistoryPagination(Pagination pagination, int id);
        int AddHistory(History history);
        void DeleteHistory(int id);
        List<History> GetHistory(int id);
        History GetHistoryById(int id);
        History UserHistoryBuilder(SqlDataReader reader);
        void UpdateHistory(int id, History history);
        DayStreak GetDayStreak(int id);
    }
}