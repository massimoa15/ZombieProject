using Global;

public class Item
{
    private int cost;
    
    public ItemName Name;

    public Item(int cost, ItemName name = ItemName.Heal1)
    {
        this.cost = cost;
        this.Name = name;
    }

    public int GetCost()
    {
        return cost;
    }

    public override string ToString()
    {
        return "$" + cost + ".";
    }
}
