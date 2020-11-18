// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace k8s.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// HPAScalingPolicy is a single policy which must hold true for a
    /// specified past interval.
    /// </summary>
    public partial class V2beta2HPAScalingPolicy
    {
        /// <summary>
        /// Initializes a new instance of the V2beta2HPAScalingPolicy class.
        /// </summary>
        public V2beta2HPAScalingPolicy()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the V2beta2HPAScalingPolicy class.
        /// </summary>
        /// <param name="periodSeconds">PeriodSeconds specifies the window of
        /// time for which the policy should hold true. PeriodSeconds must be
        /// greater than zero and less than or equal to 1800 (30 min).</param>
        /// <param name="type">Type is used to specify the scaling
        /// policy.</param>
        /// <param name="value">Value contains the amount of change which is
        /// permitted by the policy. It must be greater than zero</param>
        public V2beta2HPAScalingPolicy(int periodSeconds, string type, int value)
        {
            PeriodSeconds = periodSeconds;
            Type = type;
            Value = value;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets periodSeconds specifies the window of time for which
        /// the policy should hold true. PeriodSeconds must be greater than
        /// zero and less than or equal to 1800 (30 min).
        /// </summary>
        [JsonProperty(PropertyName = "periodSeconds")]
        public int PeriodSeconds { get; set; }

        /// <summary>
        /// Gets or sets type is used to specify the scaling policy.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets value contains the amount of change which is permitted
        /// by the policy. It must be greater than zero
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Type == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Type");
            }
        }
    }
}