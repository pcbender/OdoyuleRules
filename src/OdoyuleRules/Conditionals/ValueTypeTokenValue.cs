// Copyright 2011 Chris Patterson
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace OdoyuleRules.Conditionals
{
    using System;
    using Models.RuntimeModel;

    public class ValueTypeTokenValue<T, TProperty>
        : Value<TProperty>
        where T : class
        where TProperty : struct
    {
        readonly Token<T, TProperty> _token;

        public ValueTypeTokenValue(Token<T, TProperty> token)
        {
            _token = token;
        }

        public void Match(Action<TProperty> valueCallback)
        {
            TProperty value = _token.Item2;

            valueCallback(value);
        }
    }
}