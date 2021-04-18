using HiddenLove.DataAccess.Entities;

namespace HiddenLove.DataAccess.Entities
{
    public class FakeSpam : IEntity<int>
    {
        public int Id { get; set; }
		public string Name { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
    }
}