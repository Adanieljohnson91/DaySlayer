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
    public class RitualRepository : BaseRepository, IRitualRepository
    {
        public RitualRepository(IConfiguration configuration) : base(configuration) { }
        private string baseRitualSql = @"select r.id as ritualId,r.userId as ritualUserId,r.name as ritualName,r.weight as ritualWeight, r.isActive as ritualIsActive from Ritual r ";
        // INSERT Ritual(isActive, name, userId, weight) VALUES(0, 'Deactivated Ritual Number 2', 1, 50)
        private string insertRitual = @"INSERT Ritual (isActive, name, userId, weight) OUTPUT INSERTED.ID  VALUES ";
        //DELETE RITUAL WHERE ID = @id
        private string deleteRitual = @"Delete Ritual";
        //UPDATE Ritual SET name = 'Stillness', isActive = 1 WHERE isActive = 0 AND id = 5;
        private string updateRitual = @"Update Ritual Set ";
        public void AddRitual(Ritual ritual)
        {
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = insertRitual + @"  (@isActive, @name, @userId, @weight)";
            DbUtils.AddParameter(cmd, "isActive", ritual.IsActive);
            DbUtils.AddParameter(cmd, "name", ritual.Name);
            DbUtils.AddParameter(cmd, "userId", ritual.UserId);
            DbUtils.AddParameter(cmd, "weight", ritual.Weight);
            ritual.Id = (int)cmd.ExecuteScalar();
        }
        public void DeactivateRitual(int id)
        {
            
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = updateRitual + @"isActive = 0 WHERE id = @id";
            DbUtils.AddParameter(cmd, "@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void ReactivateRitual(int id)
        {
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = updateRitual += @"isActive = 1 WHERE id = @id";
            DbUtils.AddParameter(cmd, "@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public List<Ritual> GetRituals(int id)
        {
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = baseRitualSql += $@"WHERE r.userId = @id ORDER BY r.name";
            DbUtils.AddParameter(cmd, "@id", id);
            var reader = cmd.ExecuteReader();
            List<Ritual> ritual = new List<Ritual>();
            while (reader.Read())
            {
                ritual.Add(RitualBuilder(reader));
            }
            conn.Close();
            return ritual;
        }
        public Ritual GetRitualById(int id)
        {
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = baseRitualSql += @"WHERE id = @id";
            DbUtils.AddParameter(cmd, "@id", id);
            cmd.ExecuteReader();
            Ritual ritual = new Ritual();
            conn.Close();
            return ritual;
        }
        public void UpdateRitual(Ritual ritual)
        {
            using SqlConnection conn = Connection;
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = updateRitual += @"isActive = @isActive,
                                                name = @name, 
                                                weight = @weight, 
                                                userId = @userId 
                                                WHERE id = @id";
            DbUtils.AddParameter(cmd, "@isActive", ritual.IsActive);
            DbUtils.AddParameter(cmd, "@name", ritual.Name);
            DbUtils.AddParameter(cmd, "@weight", ritual.Weight);
            DbUtils.AddParameter(cmd, "@userId", ritual.UserId);
            DbUtils.AddParameter(cmd, "@id", ritual.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public Ritual RitualBuilder(SqlDataReader reader)
        {
            return new Ritual()
            {
                Id = DbUtils.GetInt(reader, "ritualId"),
                Name = DbUtils.GetString(reader, "ritualName"),
                UserId = DbUtils.GetInt(reader, "ritualUserId"),
                Weight = DbUtils.GetInt(reader, "ritualWeight"),
                IsActive = DbUtils.GetBool(reader, "ritualIsActive")
            };
        }

    }
}
