// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2019 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    
// 
// Notes:
//

using DaggerfallConnect.FallExe;
using DaggerfallWorkshop.Game.Entity;

namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    /// <summary>
    /// Adds -5 to armour class in every armor slot. Does not stack with other items of this effect.
    /// </summary>
    public class WeakensArmor : BaseEntityEffect
    {
        public static readonly string EffectKey = EnchantmentTypes.WeakensArmor.ToString();

        const int enchantCost = -700;
        const int decreaseArmorValue = -5;

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.GroupName = TextManager.Instance.GetText(textDatabase, EffectKey);
            properties.ShowSpellIcon = false;
            properties.AllowedCraftingStations = MagicCraftingStations.ItemMaker;
            properties.ItemMakerFlags = ItemMakerFlags.None;
            properties.EnchantmentPayloadFlags = EnchantmentPayloadFlags.Held;
        }

        public override EnchantmentSettings[] GetEnchantmentSettings()
        {
            EnchantmentSettings[] enchantments = new EnchantmentSettings[1];
            enchantments[0] = new EnchantmentSettings()
            {
                Version = 1,
                EffectKey = EffectKey,
                ClassicType = EnchantmentTypes.WeakensArmor,
                ClassicParam = -1,
                PrimaryDisplayName = properties.GroupName,
                EnchantCost = enchantCost,
            };

            return enchantments;
        }

        public override void ConstantEffect()
        {
            base.ConstantEffect();

            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            entityBehaviour.Entity.SetDecreasedArmorValueModifier(decreaseArmorValue);
        }

        public override bool IsEnchantmentExclusiveTo(EnchantmentSettings[] settingsToTest, EnchantmentParam? comparerParam = null)
        {
            // Exclusive to StrengthensArmor - no param test required
            string strengthensArmorKey = EnchantmentTypes.StrengthensArmor.ToString();
            foreach (EnchantmentSettings settings in settingsToTest)
            {
                if (settings.EffectKey == strengthensArmorKey)
                    return true;
            }

            return false;
        }
    }
}