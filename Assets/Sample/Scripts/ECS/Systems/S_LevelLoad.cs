using System.Linq;
using AillieoUtils.TypeExt;
using RockyECS;

namespace Sample
{
    public class S_LevelLoad :
        BaseSystem, IFilteredUpdatingSystem
    {
        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_LevelData>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {
            C_LevelData c = selection.First().GetComp<C_LevelData>();

            switch (c.loadingPercent)
            {
                case 0:
                    c.loadingPercent++;
                    break;
                case 10:
                    c.loadingPercent++;
                    break;
                case 11:
                    c.level = CfgProxy.Instance.Get<LevelEntry>(1);
                    c.mapData = c.level.mapData;
                    c.loadingPercent++;
                    break;
                case 12:
                    c.paths.Clear();
                    c.mapData.pathTiles.ForEach(o => c.paths.AddLast(o));
                    c.monsterSequences.Clear();
                    c.monsterSequences.AddRange(c.level.waveInfo.waves);
                    c.loadingPercent++;
                    break;
                case 20:
                    var player = selection.context.Add();
                    player.AddComp<C_PlayerProperties>();
                    player.AddComp<C_IdentifyPlayer>();
                    c.loadingPercent++;
                    break;
                case 28:
                    foreach (var tileData in c.mapData.functionTiles)
                    {
                        switch (tileData.type)
                        {
                            case GameDefine.TileType.Slot:
                                Entity slot = selection.context.Create(Recipes.Get<SlotRecipe>());
                                slot.SetPosition(tileData.position);
                                break;
                            default:
                                break;
                        }
                    }
                    c.loadingPercent++;
                    break;
                case 35:
                    c.mapData.pathTiles.ForEach(o =>
                    {
                        var t = context.Add();
                        t.AddComp<C_Rotation>();
                        t.AddComp<C_Position>().position = o.position;
                        t.AddComp<C_Renderer>();
                        var asset = t.AddComp<C_Asset>();
                        asset.mesh = "Mesh/Tile";
                        asset.material = "Materials/Default";
                    });
                    c.loadingPercent++;
                    break;
                case 49:
                    Entity e = context.Add();
                    var mg = e.AddComp<C_MonsterGenerator>();
                    mg.InitMonsters(c.monsterSequences);
                    break;
                case 50:
                    break;
                default:
                    c.loadingPercent++;
                    break;
            }

            //Debug.LogError($"load {c.loadingFlag}/50");
        }
    }
}
