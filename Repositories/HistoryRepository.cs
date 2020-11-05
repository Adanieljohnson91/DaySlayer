using DaySlayer.Models;
using DaySlayer.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySlayer.Repositories
{
    public class HistoryRepository : BaseRepository, IHistoryRepository
    {
        //MAKE PRIVATE
        public string historyid = "historyId";
        public string historydate = "historyDate";
        public string hrsofsleep = "hrsOfSleep";
        public string physical = "physical";
        public string mental = "mental";
        public string spiritual = "spiritual";
        public string ritualId = "ritualId";
        public string ritualName = "ritualName";
        public string ritualWeight = "ritualWeight";
        public string ritualIsActive = "ritualIsActive";
        public string historyNote = "historyNote";
        public string historyTimeToBed = "historyTimeToBed";
        public string historyUserId = "historyUserId";
        public string historyResult = "histroyResult";

        public HistoryRepository(IConfiguration configuration) : base(configuration) { }

        public  List<History> GetHistory(int id)
        {

            //GET HISTORY BY USER ID
            List<History> history = new List<History>();
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @$"SELECT 
                                        h.[date] as {historydate},
                                        h.id as {historyid},
                                        h.hrs_of_sleep as {hrsofsleep},
                                        h.physical as {physical},
                                        h.mental as {mental},
                                        h.time_to_bed as {historyTimeToBed},
                                        h.spiritual as {spiritual},
                                        h.ritualId as {ritualId},
                                        h.note as {historyNote},
                                        h.userId as {historyUserId},
                                        r.name as {ritualName},
										r.isActive as {ritualIsActive},
                                        r.weight as {ritualWeight},
                                        h.result as {historyResult}
                                        FROM History h
                                        JOIN Ritual r ON h.ritualId = r.id 
                                        Where h.userId = @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                history.Add(UserHistoryBuilder(reader));
            }
            history[0].DayStreak = GetDayStreak(history[0].UserId);
            return history;
        }

        //GET SINGLE HISTORY BY ID

        public History GetHistoryById(int id)
        {
            History history = null;
            
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @$"SELECT 
                                        h.[date] as historyDate,
                                        h.id as {historyid},
                                        h.hrs_of_sleep as {hrsofsleep},
                                        h.physical as {physical},
                                        h.mental as {mental},
                                        h.spiritual as {spiritual},
                                        h.time_to_bed as {historyTimeToBed},
                                        h.ritualId as {ritualId},
                                        h.userId as {historyUserId},
                                        h.note as {historyNote},
                                        r.name as {ritualName},
										r.isActive as {ritualIsActive},
                                        r.weight as {ritualWeight},
                                        h.result as {historyResult}
                                        FROM History h
                                        JOIN Ritual r ON h.ritualId = r.id 
                                        Where h.Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                history = UserHistoryBuilder(reader);
                history.DayStreak =  GetDayStreak(history.UserId);
                conn.Close();
                return history;
            }
            
            conn.Close();
            return history;
        }
        //INPUT HISTORY
        public int AddHistory(History history)
        {
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $@"INSERT INTO History 
                                        (date,
                                        hrs_of_sleep,
                                        physical,
                                        mental,
                                        spiritual,
                                        ritualId,
                                        userId,
                                        time_to_bed,
                                        note,
                                        result)
                                        OUTPUT INSERTED.ID
                                        VALUES 
                                        (
                                        @histroyDate,
                                        @hrsofsleep,
                                        @physical,
                                        @mental,
                                        @spiritual,
                                        @ritualId,
                                        @userId,
                                        @time_to_bed,
                                        @note,
                                        @result)";
            DbUtils.AddParameter(cmd, "@histroyDate", history.Date);
            DbUtils.AddParameter(cmd, "@hrsofsleep", history.Hrs_Of_Sleep);
            DbUtils.AddParameter(cmd, "@physical", history.Physical);
            DbUtils.AddParameter(cmd, "@mental", history.Mental);
            DbUtils.AddParameter(cmd, "@spiritual", history.Spiritual);
            DbUtils.AddParameter(cmd, "@ritualId", history.RitualId);
            DbUtils.AddParameter(cmd, "@userId", history.UserId);
            DbUtils.AddParameter(cmd, "@time_to_bed", history.Time_To_Bed);
            DbUtils.AddParameter(cmd, "@result", history.Result);
            DbUtils.AddParameter(cmd, "@note", history.Note);
            history.Id = (int)cmd.ExecuteScalar();
            conn.Close();
            return history.Id;
        }
        //UPDATE HISTORY
        public void UpdateHistory(int id, History history )
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $@"UPDATE History SET  
                                        date = @histroyDate,
                                        hrs_of_sleep = @hrsofsleep,
                                        physical = @physical,
                                        mental = @mental,
                                        spiritual=@spiritual,
                                        ritualId = @ritualId,
                                        userId = @userId,
                                        time_to_bed = @time_to_bed,
                                        note = @note,
                                        result= @result
                                        WHERE Id = @id";
            DbUtils.AddParameter(cmd, "@histroyDate", history.Date);
            DbUtils.AddParameter(cmd, "@hrsofsleep", history.Hrs_Of_Sleep);
            DbUtils.AddParameter(cmd, "@physical", history.Physical);
            DbUtils.AddParameter(cmd, "@mental", history.Mental);
            DbUtils.AddParameter(cmd, "@spiritual", history.Spiritual);
            DbUtils.AddParameter(cmd, "@ritualId", history.RitualId);
            DbUtils.AddParameter(cmd, "@userId", history.UserId);
            DbUtils.AddParameter(cmd, "@time_to_bed", history.Time_To_Bed);
            DbUtils.AddParameter(cmd, "@result", history.Result);
            DbUtils.AddParameter(cmd, "@note", history.Note);
            DbUtils.AddParameter(cmd, "@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //GET HISTROY PAGINATION
        public HistoryTotalCount GetHistoryPagination(Pagination pagination, int id)
        {
            var history = new HistoryTotalCount();
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $@"SELECT 
                                h.[date] as {historydate},
                                h.id as {historyid},
                                h.hrs_of_sleep as {hrsofsleep},
                                h.physical as {physical},
                                h.mental as {mental},
                                h.time_to_bed as {historyTimeToBed},
                                h.spiritual as {spiritual},
                                h.ritualId as {ritualId},
                                h.userId as {historyUserId},
                                h.note as {historyNote},
                                r.name as {ritualName},
								r.isActive as {ritualIsActive},
                                r.weight as {ritualWeight},
                                h.result as {historyResult},
                                TotalRows = COUNT(*) OVER()
                                FROM History h
                                JOIN Ritual r ON h.ritualId = r.id
                                WHERE h.userId = @userId 
                                ORDER BY date DESC
                                OFFSET   @pagenum *  10 ROWS
                                FETCH NEXT @pagesize ROWS ONLY;";
            DbUtils.AddParameter(cmd, "@pagenum", pagination.PageNumber);
            DbUtils.AddParameter(cmd, "@pagesize", pagination.PageSize);
            DbUtils.AddParameter(cmd, "@userId", id);
            var reader = cmd.ExecuteReader();
            history.Histories = new List<History>();
            while (reader.Read())
            {
                history.Histories.Add(UserHistoryBuilder(reader)); 
                history.TotalCount = DbUtils.GetInt(reader, "TotalRows");
            }
            history.DayStreak = GetDayStreak(history.Histories[0].UserId);
            conn.Close();
            return history;

        }
        //GET DAY STREAK... Experiement
        public   DayStreak GetDayStreak(int id)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $@"SELECT 
                                DayStreak=ROW_NUMBER() OVER( PARTITION BY GRP  ORDER BY date DESC) ,date
                                FROM
                                (
                                SELECT 
                                    date, DATEDIFF(Day, '1900-01-01' , date)- ROW_NUMBER() OVER( ORDER BY date ASC) AS GRP
                                FROM History 
                                WHERE userId = @id
                                ) A";
            DbUtils.AddParameter(cmd, "@id", id);
            SqlDataReader reader = null;
            try
            {
 reader = cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Using LINQ .Last()
            List<int> dayarray = new List<int>();
            while(reader.Read())
                {
                int num = (int)DbUtils.GetBigInt(reader, "DayStreak");
                dayarray.Add(num);
                }
            DayStreak dayStreak = new DayStreak();
           
            dayStreak.Streak = dayarray.Last();
            return dayStreak;
            
        }




        //DELETE HISTORY
        public void DeleteHistory(int id)
        {
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $@"DELETE History
                                         Where id = @id";
            DbUtils.AddParameter(cmd, "@Id", id);
            cmd.ExecuteNonQuery();
        }







        public History UserHistoryBuilder(SqlDataReader reader)
        {
            return new History()
            {
                Id = DbUtils.GetInt(reader, historyid),
                Date = DbUtils.GetDateTime(reader, historydate),
                Hrs_Of_Sleep = DbUtils.GetInt(reader, hrsofsleep),
                Physical = DbUtils.GetBool(reader, physical),
                Mental = DbUtils.GetBool(reader, mental),
                Spiritual = DbUtils.GetBool(reader, spiritual),
                RitualId = DbUtils.GetInt(reader, ritualId),
                Time_To_Bed = DbUtils.GetInt(reader, historyTimeToBed),
                Note = DbUtils.GetString(reader, historyNote),
                UserId = DbUtils.GetInt(reader, historyUserId),
                Result = DbUtils.GetInt(reader, historyResult),
                Ritual = new Ritual()
                {
                    Id = DbUtils.GetInt(reader, ritualId),
                    Name = DbUtils.GetString(reader, ritualName),
                    Weight = DbUtils.GetInt(reader, ritualWeight),
                    IsActive = DbUtils.GetBool(reader, ritualIsActive)
                }
            };
        }
    }
}
