using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIlaTrainingTopic
    {
        public int Corid { get; set; }
        public bool Capacitance { get; set; }
        public bool Inductance { get; set; }
        public bool Impedance { get; set; }
        public bool RealReactivePower { get; set; }
        public bool ElectricalCircuits { get; set; }
        public bool Magnetism { get; set; }
        public bool Trigonometry { get; set; }
        public bool Ratios { get; set; }
        public bool PerUnit { get; set; }
        public bool Pythagorean { get; set; }
        public bool OhmsLaw { get; set; }
        public bool KirchhoffsLaw { get; set; }
        public bool Transmission { get; set; }
        public bool Transformers { get; set; }
        public bool Substations { get; set; }
        public bool PowerPlants { get; set; }
        public bool Protection { get; set; }
        public bool Introduction { get; set; }
        public bool TransmissionPrinciple { get; set; }
        public bool TransformersPrinciple { get; set; }
        public bool Busses { get; set; }
        public bool Generators { get; set; }
        public bool Relays { get; set; }
        public bool PowerSystemFaults { get; set; }
        public bool SyncronizingEquipment { get; set; }
        public bool UnderVoltage { get; set; }
        public bool CommSystems { get; set; }
        public bool VoltageControl { get; set; }
        public bool FrequencyControl { get; set; }
        public bool Stability { get; set; }
        public bool Outage { get; set; }
        public bool EnergyAccounting { get; set; }
        public bool InadvertentEnergy { get; set; }
        public bool TimeError { get; set; }
        public bool BalancingResources { get; set; }
        public bool GenerationLoss { get; set; }
        public bool TransmissionLoss { get; set; }
        public bool OperatingReserves { get; set; }
        public bool ContingencyReserves { get; set; }
        public bool LineLoadingRelief { get; set; }
        public bool LoadShedding { get; set; }
        public bool Emergencies { get; set; }
        public bool EmsLoss { get; set; }
        public bool PrimaryControlCenterLoss { get; set; }
        public bool RestorationPhilosophies { get; set; }
        public bool FacilityRestorationPriorities { get; set; }
        public bool Blackstart { get; set; }
        public bool StabilityAngleVoltage { get; set; }
        public bool IslandingAndSynchronizing { get; set; }
        public bool Naesb { get; set; }
        public bool StandardsOfConduct { get; set; }
        public bool Tariffs { get; set; }
        public bool Oasis { get; set; }
        public bool ETag { get; set; }
        public bool TransactionScheduleing { get; set; }
        public bool MarketApplications { get; set; }
        public bool Interchange { get; set; }
        public bool Scada { get; set; }
        public bool Agc { get; set; }
        public bool PowerFlow { get; set; }
        public bool StateEstimator { get; set; }
        public bool ContingencyAnalysis { get; set; }
        public bool PvCurves { get; set; }
        public bool Forecasting { get; set; }
        public bool EnergyAccountingApp { get; set; }
        public bool VoiceAndDataComms { get; set; }
        public bool DemandSide { get; set; }
        public bool FacilitiesLoss { get; set; }
        public bool CommunicationsLoss { get; set; }
        public bool TelemetryProblems { get; set; }
        public bool ContingencyProblems { get; set; }
        public bool ProperCommunications { get; set; }
        public bool AppropriateCommunications { get; set; }
        public bool CyberSecurity { get; set; }
        public bool IsoRto { get; set; }
        public bool Regional { get; set; }
        public bool CompanyPolicies { get; set; }
        public bool ReliabilityStandards { get; set; }

        public virtual TblCourse Cor { get; set; }
    }
}
