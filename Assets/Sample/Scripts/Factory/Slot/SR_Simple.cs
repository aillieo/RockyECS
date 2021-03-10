using RockyECS;
using UnityEngine;

namespace Sample
{
    public class SR_Simple : SlotRecipe
    {
        public override void SetupSlot(Entity slot)
        {
            C_Collider collider = slot.AddComp<C_Collider>();
            collider.onClick = () => SimpleOnClick(slot.id);
            collider.threshold = 0.15f;

            slot.AddComp<C_Renderer>();

            C_Asset asset = slot.AddComp<C_Asset>();
            asset.mesh = "Mesh/TowerSlot";
            asset.material = "Materials/Default";
        }

        private void SimpleOnClick(int e)
        {
            var container = Container.Instance;
            Entity unitObj = container.Get(e);
            if (unitObj == null)
                return;

            Vector2 position = unitObj.GetPosition();

            Entity tower = Factory.CreateTower(CfgProxy.Instance.Get<TowerEntry>(1000));

            container.Remove(e);
            tower.SetPosition(position);
        }
    }
}

