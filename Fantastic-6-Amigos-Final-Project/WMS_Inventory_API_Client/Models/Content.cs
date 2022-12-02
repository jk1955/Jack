namespace WMS_Inventory_API_Client.Models
{
    public class Content
    {
        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
        public int? ContainerId { get; set; }
        public virtual Container? Container { get; set; }

        public Content(int? id, int? quantity, string? description, int? containerId, Container? container)
        {
            Id = id;
            Quantity = quantity;
            Description = description;
            ContainerId = containerId;
            Container = container;
        }

        public Content()
        {
            return;
        }
    }
}
