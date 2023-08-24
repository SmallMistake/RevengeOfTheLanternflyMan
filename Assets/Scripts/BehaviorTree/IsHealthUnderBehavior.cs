using BehaviorDesigner.Runtime.Tasks;
using MoreMountains.TopDownEngine;

namespace IntronDigital.BehaviorTree
{
    [HelpURL("http://www.example.com")]
    [TaskIcon("Assets/Art/BehaviorTree/attack-icon.png")]
    [TaskName("Is Health Under")]
    [TaskCategory("Conditionals")]
    [TaskDescription("Succeed if health is below a certain threshold")]
    public class IsHealthUnderBehavior : Conditional
    {
        public float healthThreshold;
        public Health health;

        public override TaskStatus OnUpdate()
        {
            return health.CurrentHealth < healthThreshold ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}