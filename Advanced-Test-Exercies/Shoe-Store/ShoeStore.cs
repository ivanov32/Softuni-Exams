using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoeStore
{
    public class ShoeStore
    {
        private List<Shoe> Shoes;
        public ShoeStore(string n, int sc)
        {
            name = n;
            storageCapacity = sc;
            this.Shoes = new List<Shoe>();  
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int storageCapacity;

        public int StorageCapacity
        {
            get { return storageCapacity; }
            set { storageCapacity = value; }
        }
        public int Count => Shoes.Count;
        public string AddShoe(Shoe shoe)
        {
            if (Shoes.Count == StorageCapacity)
            {
                return "No more space in the storage room.";
            }
            Shoes.Add(shoe);
            return $"Successfully added {shoe.Type} {shoe.Material} pair of shoes to the store.";
        }
        public int RemoveShoes(string material)
        {
            var shoesToRemove = Shoes.Where(m => m.Material == material);
            int c = shoesToRemove.Count();
            Shoes.RemoveAll(m => m.Material == material);
            return c;
        }
        public List<Shoe> GetShoesByType(string type)
        {
            List<Shoe> shoeList = Shoes
                .Where(t => t.Type.ToLower() == type.ToLower())
                .ToList();
            return shoeList;
        }
        public Shoe GetShoeBySize(double size)
        {
            Shoe shoe = Shoes.FirstOrDefault(s => s.Size == size);
            return shoe;
        }
        public string StockList(double size, string type)
        {
            var sb = new StringBuilder();
            var soretedShoes = Shoes.Where(s=>s.Size == size && s.Type.ToLower() == type.ToLower()).ToList();
            if (soretedShoes.Count == 0)
            {
                return "No matches found!";
            }
            else
            {
                sb.AppendLine($"Stock list for size {size} - {type} shoes:");
                foreach (var s in soretedShoes)
                {
                    sb.AppendLine($"Size {s.Size}, {s.Material} {s.Brand} {s.Type} shoe.");
                }
            }
            return sb.ToString().Trim();
        }
    }
}
