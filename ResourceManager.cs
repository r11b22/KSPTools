using System;
using UnityEngine;

namespace KSPTools
{
    public static class ResourceManager
    {
        /// <summary>
        /// Sets the amount of the resource. If the amount is greater than its max amount it will automatically restrict it.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        public static void SetResource(PartResource resource, double amount)
        {
            resource.amount = (double)Mathf.Clamp((float)amount, 0, (float)resource.maxAmount);
        }
        /// <summary>
        /// Adds the amount to the resource. If the amount is greater than its max amount it will automatically restrict it.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        public static void AddResource(PartResource resource, double amount)
        {
            SetResource(resource, resource.amount + amount);
        }
        /// <summary>
        /// Removes the amount from the resource. If the amount is smaller than zero it will automatically restrict it.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        public static void RemoveResource(PartResource resource, double amount)
        {
            SetResource(resource, resource.amount - amount);
        }
    }
}
