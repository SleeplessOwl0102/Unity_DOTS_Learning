using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// Same approach for the cannon ball, we are creating a component to identify the entities.
// But this time it's not a tag component (empty) because it contains data: the Speed field.
// It won't be used immediately, but will become relevant when we implement motion.
struct CannonBall : IComponentData
{
    public float3 Speed;
}


readonly partial struct CannonBallAspect : IAspect
{
    // An Entity field in an aspect provides access to the entity itself.
    // This is required for registering commands in an EntityCommandBuffer for example.
    public readonly Entity Self;

    // Aspects can contain other aspects.
    readonly TransformAspect Transform;

    // A RefRW field provides read write access to a component. If the aspect is taken as an "in"
    // parameter, the field will behave as if it was a RefRO and will throw exceptions on write attempts.
    readonly RefRW<CannonBall> CannonBall;

    // Properties like this are not mandatory, the Transform field could just have been made public instead.
    // But they improve readability by avoiding chains of "aspect.aspect.aspect.component.value.value".
    public float3 Position
    {
        get => Transform.Position;
        set => Transform.Position = value;
    }

    public float3 Speed
    {
        get => CannonBall.ValueRO.Speed;
        set => CannonBall.ValueRW.Speed = value;
    }
}