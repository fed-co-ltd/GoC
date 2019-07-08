

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

    public struct Limit{

        public float x,y;

        public Limit(float _xy)
        {
            this.x = _xy;
            this.y = _xy;
        }
        public Limit(float _x, float _y)
        {
            this.x = _x;
            this.y = _y;
        }

        public bool CompareTo(UnityEngine.Vector3 other){
            var isX = (other.x > this.x) || (other.x < -this.x);
            var isY = (other.y > this.y) || (other.y < -this.y);
            if (isX || isY)
            {
                return false;
            }
            return true;
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