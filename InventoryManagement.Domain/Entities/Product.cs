namespace InventoryManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public void IncreaseStock(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount must be positive.");

            Quantity += amount;
        }

        public void DecreaseStock(int amount)
        {
            if (amount < 0 || amount > Quantity)
                throw new ArgumentException("Invalid amount.");

            Quantity -= amount;
        }
    }
}
