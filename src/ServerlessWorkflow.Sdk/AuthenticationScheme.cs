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

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Enumerates all supported authentication schemes
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.StringEnumConverterFactory))]
    public enum AuthenticationScheme
    {
        /// <summary>
        /// Indicates the basic (username/password) authentication scheme
        /// </summary>
        [EnumMember(Value = "basic")]
        Basic = 1,
        /// <summary>
        /// Indicates the bearer (JwT) authentication scheme
        /// </summary>
        [EnumMember(Value = "bearer")]
        Bearer = 2,
        /// <summary>
        /// Indicates the OAuth 2 authentication scheme
        /// </summary>
        [EnumMember(Value = "oauth2")]
        OAuth2 = 4
    }

}
