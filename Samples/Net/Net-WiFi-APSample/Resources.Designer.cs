//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Net_WiFi_APSample
{
    
    internal partial class Resources
    {
        private static System.Resources.ResourceManager manager;
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if ((Resources.manager == null))
                {
                    Resources.manager = new System.Resources.ResourceManager("Net_WiFi_APSample.Resources", typeof(Resources).Assembly);
                }
                return Resources.manager;
            }
        }
        internal static string GetString(Resources.StringResources id)
        {
            return ((string)(nanoFramework.Runtime.Native.ResourceUtility.GetObject(ResourceManager, id)));
        }
        internal static byte[] GetBytes(Resources.BinaryResources id)
        {
            return ((byte[])(nanoFramework.Runtime.Native.ResourceUtility.GetObject(ResourceManager, id)));
        }
        [System.SerializableAttribute()]
        internal enum BinaryResources : short
        {
            favicon = -22914,
        }
        [System.SerializableAttribute()]
        internal enum StringResources : short
        {
            main = 4691,
        }
    }
}