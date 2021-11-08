using Global;

public class Item
{
    private int cost;
    
    public UpgradeName Name;

    public Item(int cost, UpgradeName name = UpgradeName.Heal1)
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
