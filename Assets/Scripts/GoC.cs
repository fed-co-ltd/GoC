

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

    public struct Coord{

        public float x,y;

        public Coord(float _xy)
        {
            this.x = _xy;
            this.y = _xy;
        }
        public Coord(float _x, float _y)
        {
            this.x = _x;
            this.y = _y;
        }
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