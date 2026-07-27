using BaseLib.Abstracts;
using NS_Crystal.NS_CrystalCode.Extensions;
using Godot;

namespace NS_Crystal.NS_CrystalCode.Character;

public class CrystalRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Crystal.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}