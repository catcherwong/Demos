using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorizedServer
{
    [Table("rtoken")]
    public class RToken
    {
        [Column("id")]
        [Key]
        public string Id { get; set; }

        [Column("client_id")]
        public string ClientId { get; set; }

        [Column("refresh_token")]
        public string RefreshToken { get; set; }
        
        [Column("isstop")]
        public int IsStop { get; set; }
    }    
}
