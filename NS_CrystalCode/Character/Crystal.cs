using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using NS_Crystal.NS_CrystalCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using NS_Crystal.NS_CrystalCode.Cards;
using NS_Crystal.NS_CrystalCode.Relics;

namespace NS_Crystal.NS_CrystalCode.Character;

public class Crystal : PlaceholderCharacterModel
{
    public const string CharacterId = "Crystal";

    public static readonly Color Color = new("ffffff");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck => [
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<CrystallineShatter>(),
        ModelDb.Card<Crystalize>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<CrystalCore>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<CrystalCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<CrystalRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<CrystalPotionPool>();

    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }
    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}