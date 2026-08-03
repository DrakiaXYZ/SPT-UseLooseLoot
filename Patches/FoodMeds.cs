using EFT;
using EFT.InventoryLogic;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPT.Reflection.Utils;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Gaylatea
{
    namespace UseLooseLoot
    {
        /// <summary>
        /// Changes loose food and med items so that they can be used without
        /// needing to take them into your inventory first.
        /// </summary>
        class MakeFoodMedsUsablePatch : ModulePatch
        {
            protected override MethodBase GetTargetMethod()
            {
                return AccessTools.Method(typeof(InteractionContextHelper), nameof(InteractionContextHelper.GetAvailableInteractionState));
            }

            [PatchPostfix]
            public static void PatchPostfix(ref AvailableInteractionState __result, Item rootItem, GamePlayerOwner owner)
            {
                if(!(rootItem is Meds) && !(rootItem is FoodDrink)) {
                    return;
                }

                var actionHandler = new FoodMedUser
                {
                    owner = owner,
                    item = rootItem
                };

                var useMenuItem = new InteractionAction
                {
                    Name = "Use".Localized(),
                    Action = new Action(actionHandler.UseAll)
                };

                __result.Actions.Add(useMenuItem);
            }
        }

        class FoodMedUser {
            public GamePlayerOwner owner;
            public Item item;

            public void UseAll() {
                this.owner.Player.HealthController.ApplyItem(this.item, EBodyPart.Common);
            }
        }
    }
}