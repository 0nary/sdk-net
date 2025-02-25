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
using ServerlessWorkflow.Sdk.Models;
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="SleepStateDefinition"/>s
    /// </summary>
    public interface IDelayStateBuilder
        : IStateBuilder<SleepStateDefinition>
    {

        /// <summary>
        /// Configures the duration of the workflow execution's delay
        /// </summary>
        /// <param name="duration">The duration of the workflow execution's delay</param>
        /// <returns>The configured <see cref="IDelayStateBuilder"/></returns>
        IDelayStateBuilder For(TimeSpan duration);

    }

}
