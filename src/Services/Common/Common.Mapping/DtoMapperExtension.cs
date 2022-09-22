using System.Text.Json;


namespace Common.Mapping
{
    #region Mapea cualquier clase que tenga las mismas propiedades
    public static class DtoMapperExtension
    {
        public static T MapTo<T>(this object value) 
        { 
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value));
        }
            
    }
    #endregion
}
