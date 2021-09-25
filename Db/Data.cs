using System;

namespace SetResponseHeaders.Db
{
    public class Data
    {
        public Guid DataId { get; set; }
        public string Name { get; set; } = null!;
        public int Number { get; set; }
        public DateTime StartDate { get; set; }
    }
}