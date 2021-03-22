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
                new Filter<C_LevelLoad>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {
            Entity e = selection.First();
            C_LevelLoad c = e.GetComp<C_LevelLoad>();

            if(c.isDone)
            {
                e.RemoveComp(c);
                return;
            }

            c.loadingPercent++;

            C_LevelData data = null;

            switch (c.loadingPercent)
            {
                case 10:
                    c.level = CfgProxy.Instance.Get<LevelEntry>(1);
                    c.mapData = c.level.mapData;
                    break;
                case 12:
                    data = e.GetComp<C_LevelData>();
                    if(data == null)
                    {
                        data = e.AddComp<C_LevelData>();
                    }
                    data.paths.Clear();
                    c.mapData.pathTiles.ForEach(o => data.paths.AddLast(o));
                    data.monsterSequences.Clear();
                    data.monsterSequences.AddRange(c.level.waveInfo.waves);
                    break;
                case 20:
                    e.AddComp<C_PlayerProperties>();
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
                    break;
                case 49:
                    data = e.GetComp<C_LevelData>();
                    if (data == null)
                    {
                        data = e.AddComp<C_LevelData>();
                    }
                    var mg = e.AddComp<C_MonsterGenerator>();
                    mg.InitMonsters(data.monsterSequences);
                    break;
                case 50:
                    c.isDone = true;
                    break;
                default:
                    break;
            }
        }
    }
}
