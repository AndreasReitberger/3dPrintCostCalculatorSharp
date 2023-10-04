﻿namespace AndreasReitberger.Print3d.Enums
{
    public enum ProcedureAttribute
    {
        // Filament
        NozzleTemperature,
        NozzleWear,
        PrintSheetWear,
        HeatedBedTemperature,
        MultiMaterial,

        // Resin
        GlovesCosts,
        WashingCosts,
        CuringCosts,
        FilterCosts,
        ResinTankWear,

        // Powder
        MaterialRefreshingRatio,
        Granulation,
        TensileStrength,
        ElongationAtBreak,
        ImpactResistance,
        ShoreHardness,

        // Misc
        MeltingPoint,
    }
}
