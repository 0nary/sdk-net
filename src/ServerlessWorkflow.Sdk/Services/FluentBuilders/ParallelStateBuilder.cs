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
    /// Represents the default implementation of the <see cref="IParallelStateBuilder"/> interface
    /// </summary>
    public class ParallelStateBuilder
        : StateBuilder<ParallelStateDefinition>, IParallelStateBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ParallelStateBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
        public ParallelStateBuilder(IPipelineBuilder pipeline)
            : base(pipeline)
        {

        }

        /// <inheritdoc/>
        public virtual IParallelStateBuilder Branch(Action<IBranchBuilder> branchSetup)
        {
            IBranchBuilder branch = new BranchBuilder(this.Pipeline);
            branchSetup(branch);
            this.State.Branches.Add(branch.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IParallelStateBuilder WaitFor(uint amount)
        {
            this.State.CompletionType = ParallelCompletionType.AtLeastN;
            this.State.N = amount;
            return this;
        }

        /// <inheritdoc/>
        public virtual IParallelStateBuilder WaitForAll()
        {
            this.State.CompletionType = ParallelCompletionType.AllOf;
            return this;
        }

    }

}
