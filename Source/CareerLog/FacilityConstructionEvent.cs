﻿using System;

namespace RP0
{
    public class FacilityConstructionEvent : CareerEvent
    {
        [Persistent]
        public FacilityType Facility;

        [Persistent]
        public ConstructionState State;

        [Persistent]
        public Guid FacilityID;

        public FacilityConstructionEvent(double UT) : base(UT)
        {
        }

        public FacilityConstructionEvent(ConfigNode n) : base(n)
        {
        }

        public static FacilityType ParseFacilityType(SpaceCenterFacility scf)
        {
            return (FacilityType)Enum.Parse(typeof(FacilityType), scf.ToString());
        }
    }

    public enum ConstructionState
    {
        Started, Cancelled, Completed
    }

    public enum FacilityType
    {
        Administration = 1,
        AstronautComplex = 1 << 1,
        LaunchPad = 1 << 2,
        MissionControl = 1 << 3,
        ResearchAndDevelopment = 1 << 4,
        Runway = 1 << 5,
        TrackingStation = 1 << 6,
        SpaceplaneHangar = 1 << 7,
        VehicleAssemblyBuilding = 1 << 8,
        LaunchComplex = 1 << 9
    }
}
