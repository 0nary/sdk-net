﻿/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a workflow state that can be seen as a workflow gateway: they can direct transitions of a workflow based on certain conditions
    /// </summary>
    [DiscriminatorValue(StateType.Switch)]
    [ProtoContract]
    [DataContract]
    public class SwitchStateDefinition
        : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="SwitchStateDefinition"/>
        /// </summary>
        public SwitchStateDefinition()
            : base(StateType.Switch)
        {

        }

        /// <summary>
        /// Gets the <see cref="SwitchStateDefinition"/>'s type
        /// </summary>
        public virtual SwitchStateType SwitchType
        {
            get
            {
                if (this.DataConditions != null
                    && this.DataConditions.Any())
                    return SwitchStateType.Data;
                else if (this.EventConditions != null
                    && this.EventConditions.Any())
                    return SwitchStateType.Event;
                else
                    throw new Exception($"A switch state must define at least one data-based or one event-based condition");
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> of <see cref="DataCaseDefinition"/>s between which to switch. Assigning the property sets the <see cref="SwitchStateDefinition"/>'s <see cref="SwitchType"/> to <see cref="SwitchStateType.Data"/>.
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual List<DataCaseDefinition>? DataConditions { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> of <see cref="EventCaseDefinition"/>s between which to switch. Assigning the property sets the <see cref="SwitchStateDefinition"/>'s <see cref="SwitchType"/> to <see cref="SwitchStateType.Event"/>.
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual List<EventCaseDefinition>? EventConditions { get; set; }

        /// <summary>
        /// Gets/sets the duration to wait for incoming events
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601NullableTimeSpanConverter))]
        public virtual TimeSpan? EventTimeout { get; set; }

        /// <summary>
        /// Gets/sets an object used to configure the <see cref="SwitchStateDefinition"/>'s default condition, in case none of the specified conditions were met
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(3, IsRequired = true)]
        [DataMember(Order = 3, IsRequired = true)]
        public virtual DefaultCaseDefinition DefaultCondition { get; set; } = null!;

        /// <summary>
        /// Gets the <see cref="SwitchCaseDefinition"/> with the specified name
        /// </summary>
        /// <param name="caseName">The name of the <see cref="SwitchCaseDefinition"/> to get</param>
        /// <returns>The <see cref="SwitchCaseDefinition"/> with the specified name</returns>
        public virtual SwitchCaseDefinition? GetCase(string caseName)
        {
            SwitchCaseDefinition @case;
            switch (this.SwitchType)
            {
                case SwitchStateType.Data:
                    if (caseName == "default")
                        @case = new DataCaseDefinition() { Name = "default", Transition = this.DefaultCondition.Transition, End = this.DefaultCondition.End };
                    else
                        @case = this.DataConditions.Single(c => c.Name == caseName);
                    break;
                case SwitchStateType.Event:
                    if (caseName == "default")
                        @case = new EventCaseDefinition() { Name = "default", Transition = this.DefaultCondition.Transition, End = this.DefaultCondition.End };
                    else
                        @case = this.EventConditions.Single(c => c.Name == caseName);
                    break;
                default:
                    throw new NotSupportedException($"The specified switch state type '{this.SwitchType}' is not supported in this context");
            }
            return @case;
        }

        /// <summary>
        /// Attempts to get the <see cref="SwitchCaseDefinition"/> with the specified name
        /// </summary>
        /// <param name="caseName">The name of the <see cref="SwitchCaseDefinition"/> to get</param>
        /// <param name="case">The <see cref="SwitchCaseDefinition"/> with the specified name</param>
        /// <returns>A boolean indicating whether or not the <see cref="SwitchCaseDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetCase(string caseName, out SwitchCaseDefinition @case)
        {
            @case = null!;
            try
            {
                @case = this.GetCase(caseName)!;
            }
            catch
            {
                return false;
            }
            return @case != null;
        }

        /// <summary>
        /// Gets the <see cref="EventCaseDefinition"/> that applies to the specified event
        /// </summary>
        /// <param name="eventReference">The name of the event the <see cref="EventCaseDefinition"/> to get applies to</param>
        /// <returns>The <see cref="EventCaseDefinition"/> that applies to the specified event</returns>
        public virtual EventCaseDefinition? GetEventCase(string eventReference)
        {
            return this.EventConditions?.FirstOrDefault(c => c.Event == eventReference);
        }

        /// <summary>
        /// Attempts to get the <see cref="EventCaseDefinition"/> that applies to the specified event
        /// </summary>
        /// <param name="eventReference">The reference of the event the <see cref="EventCaseDefinition"/> to get applies to</param>
        /// <param name="case">The <see cref="EventCaseDefinition"/> that applies to the specified event</param>
        /// <returns>A boolean indicating whether or not a <see cref="EventCaseDefinition"/> with the specified id could be found</returns>
        public virtual bool TryGetEventCase(string eventReference, out EventCaseDefinition @case)
        {
            @case = null!;
            try
            {
                @case = this.GetEventCase(eventReference)!;
            }
            catch
            {
                return false;
            }
            return @case != null;
        }

    }

}
