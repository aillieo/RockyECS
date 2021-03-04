namespace Sample
{
    public static class GameDefine
    {
        // 运行时 地块枚举
        public enum TileType
        {
            Empty               = 0,           

            // 行走相关
            Path                = 1,            // 0000 0000 0000 0001
            PathStart           = 1 << 1,       // 0000 0000 0000 0010
            PathEnd             = 1 << 2,       // 0000 0000 0000 0100
            WalkableMask        = 0x000f,       // 0000 0000 0000 1111
            
            // 建造相关
            Slot                = 1 << 4,       // 0000 0000 0001 0000
            ClickableMask       = 0x00f0,       // 0000 0000 1111 0000
        }
    }
}
