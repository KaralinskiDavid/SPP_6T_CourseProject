using System;

namespace Dao.Impl.DaoModels
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string RefreshTokenValue { get; set; }
        public string Key { get; set; }
        public DateTime RecordCreated { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string IpAddress { get; set; }
    }
}
