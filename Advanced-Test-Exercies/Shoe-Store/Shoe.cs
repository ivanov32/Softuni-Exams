namespace ShoeStore
{
    public class Shoe
    {
        public Shoe(string b, string t, double s, string m)
        {
            brand = b;
            type = t;
            size = s;
            material = m;
        }
        private string brand;

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private double size;

        public double Size
        {
            get { return size; }
            set { size = value; }
        }
        private string material;

        public string Material
        {
            get { return material; }
            set { material = value; }
        }
        public override string ToString()
        {
            return $"Size {Size}, {Material} {Brand} {Type} shoe.";
        }
    }
}
