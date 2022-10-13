using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFactory
{
    public TankComponent CreateTankWithPathMovement(Vector3 tankPosition, Vector3 tankRotation, float tankScaleFactor, string transformPathSource)
    {
        var tankComponent = this.CreateTankComponentWithTransform(tankPosition, tankRotation, tankScaleFactor);
        var tankMovementComponent = tankComponent.gameObject.AddComponent<TankPathMovementComponent>();
        var tankSeatingComponent = tankComponent.gameObject.GetComponentInChildren<TankSeatingComponent>();
        var tankTurretComponent = tankComponent.gameObject.GetComponentInChildren<TankTurretComponent>();

        tankComponent.SetSeatingTransform(TankSeatingName.RearRight, tankSeatingComponent.GetRearRightSeatingTransform());
        tankComponent.SetSeatingTransform(TankSeatingName.RearLeft, tankSeatingComponent.GetRearLeftSeatingTransform());
        tankComponent.SetSeatingTransform(TankSeatingName.FrontRight, tankSeatingComponent.GetFrontRightSeatingTransform());
        tankComponent.SetMovementComponent(tankMovementComponent);
        tankComponent.SetTankMovementSourcePathFileName(transformPathSource);
        tankComponent.SetTankTurretComponent(tankTurretComponent);

        return tankComponent;
    }

    public TankComponent CreateTankWithEditorMovement(Vector3 tankPosition, Vector3 tankRotation, float tankScaleFactor, string transformPathSource)
    {
        var tankComponent = this.CreateTankComponentWithTransform(tankPosition, tankRotation, tankScaleFactor);
        var tankMovementComponent = tankComponent.gameObject.AddComponent<TankEditorMovementComponent>();
        var tankSeatingComponent = tankComponent.gameObject.GetComponentInChildren<TankSeatingComponent>();
        var tankTurretComponent = tankComponent.gameObject.GetComponentInChildren<TankTurretComponent>();

        tankComponent.SetSeatingTransform(TankSeatingName.EditorMode, tankSeatingComponent.GetEditorSeatingTransform());
        tankComponent.SetMovementComponent(tankMovementComponent);
        tankComponent.SetTankMovementSourcePathFileName(transformPathSource);
        tankComponent.SetTankTurretComponent(tankTurretComponent);

        return tankComponent;
    }

    private TankComponent CreateTankComponentWithTransform(Vector3 tankPosition, Vector3 tankRotation, float tankScaleFactor)
    {
        var tankPrefab = PrefabProviderUtility.getTankPrefab();
        var instantiatedTankGameObject = GameObject.Instantiate(tankPrefab);

        instantiatedTankGameObject.transform.SetPositionAndRotation(tankPosition, Quaternion.Euler(tankRotation));
        instantiatedTankGameObject.transform.localScale *= tankScaleFactor;

        return instantiatedTankGameObject.AddComponent<TankComponent>();
    }
}
