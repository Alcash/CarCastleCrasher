
namespace ProjectCore.Vehicle
{
    public interface IVehicleControl
    {
        float ThrottleValue { get;}
        bool BoostingValue { get;}
        float SteeringValue { get;}
        bool DriftValue { get;}
        bool HandbrakeValue { get; }
        bool JumpValue { get; }
    }
}
