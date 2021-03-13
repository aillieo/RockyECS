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

            switch (c.loadingFlag)
            {
                case 0:
                    c.loadingFlag++;
                    break;
                case 10:
                    c.loadingFlag++;
                    break;
                case 11:
                    c.level = CfgProxy.Instance.Get<LevelEntry>(1);
                    c.mapData = c.level.mapData;
                    c.loadingFlag++;
                    break;
                case 12:
                    c.paths.Clear();
                    c.mapData.pathTiles.ForEach(o => c.paths.AddLast(o));
                    c.monsterSequences.Clear();
                    c.monsterSequences.AddRange(c.level.waveInfo.waves);
                    c.loadingFlag++;
                    break;
                case 20:
                    var player = selection.context.Add();
                    player.AddComp<C_PlayerProperties>();
                    player.AddComp<C_IdentifyPlayer>();
                    c.loadingFlag++;
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
                    c.loadingFlag++;
                    break;
                case 50:
                    break;
                default:
                    c.loadingFlag++;
                    break;
            }

            //Debug.LogError($"load {c.loadingFlag}/50");
        }
    }
}
