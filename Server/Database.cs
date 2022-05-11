using Server.Models;

namespace Server
{
    public class Database
    {
        List<MockProduct> mockProduct = new List<MockProduct>();

        public void Add(MockProduct mp)
        {
            mockProduct.Add(mp);
        }       

        public List<MockProduct> Print()
        {
            return mockProduct;
        }

    }
}
