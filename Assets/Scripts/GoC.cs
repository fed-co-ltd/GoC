namespace GoC
{
    enum GoCTypes
    {
        Hall, EnvironmentFeature, TroopUnit, Resource
    }
    public struct TileLocation
    {
        int x,y;
    }
    public class GObject
    {
        protected TileLocation location;
        protected int Type;
    }

    public class LObject : GObject
    {
        protected int LifePoints;
        protected int Attack;
    }
    public interface ITroopunit 
    {
        int Attack();
        bool Die();
        int GatherRes();
    }

}