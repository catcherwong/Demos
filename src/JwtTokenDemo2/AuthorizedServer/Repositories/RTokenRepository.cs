using System.Linq;

namespace AuthorizedServer.Repositories
{
    public class RTokenRepository : IRTokenRepository
    {        
        public bool AddToken(RToken token)
        {
            using (DemoDbContext db =new DemoDbContext())
            {
                db.RTokens.Add(token);

                return db.SaveChanges() > 0;
            }                   
        }

        public bool ExpireToken(RToken token)
        {
            using (DemoDbContext db = new DemoDbContext())
            {
                db.RTokens.Update(token);

                return db.SaveChanges() > 0;
            }
        }

        public RToken GetToken(string refresh_token, string client_id)
        {
            using (DemoDbContext db = new DemoDbContext())
            {
                return db.RTokens.FirstOrDefault(x => x.ClientId == client_id && x.RefreshToken == refresh_token);
            }
        }
    }
}
