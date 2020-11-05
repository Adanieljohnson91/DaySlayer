using DaySlayer.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DaySlayer.Repositories
{
    public interface IRitualRepository
    {
        void AddRitual(Ritual ritual);
        void DeactivateRitual(int id);
        void ReactivateRitual(int id);
        Ritual GetRitualById(int id);
        List<Ritual> GetRituals(int id);
        Ritual RitualBuilder(SqlDataReader reader);
        void UpdateRitual(Ritual ritual);
    }
}