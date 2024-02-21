
namespace Godly.UnitySavingSystem.Serializables.Example
{
    [Serializable]
    public class CollectibleSerializable : SavingSerializable<int>
    {
        public CollectibleType Type
        {
            get;

        }

        public CollectibleSerializable(CollectibleType type, int value) : base("collectible_" + type.ToString())
        {
            this.Type = type;
            this.value = value;
        }

        
    }
}