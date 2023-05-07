using Postgrest.Attributes;
using Postgrest.Models;

namespace API.Models
{
    [Table("Clients")]
    public class MSupabase : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("CI")]
        public string CI { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Saldo_Inicial")]
        public float Saldo_Inicial { get; set; }

        [Column("Fecha")]
        public string Fecha { get; set; }

        [Column("Status")]
        public bool Status { get; set; }
    }
}