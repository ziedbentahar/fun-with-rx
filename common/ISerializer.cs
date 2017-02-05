namespace Common
{
    public interface ISerializer<TSerializationFormat>
    {
        TSerializationFormat Serialize<TValue>(TValue value);
        TValue Deserialize<TValue>(TSerializationFormat wire);
    }

}
