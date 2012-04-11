// Copyright 2011-2012 Chris Patterson
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
namespace OdoyuleRules.Configuration
{
    using System;
    using Models.SemanticModel;


    public static class Consequences
    {
        public static AddFactConsequence<T, TFact> Add<T, TFact>(Func<T, TFact> factFactory)
            where T : class
            where TFact : class
        {
            var consequence = new AddFactConsequence<T, TFact>(factFactory);

            return consequence;
        }

        public static DelegateConsequence<T> Delegate<T>(Action<T> callback)
            where T : class
        {
            return new DelegateConsequence<T>((session, x) => callback(x));
        }

        public static DelegateConsequence<T> Delegate<T>(Action<Session, T> callback)
            where T : class
        {
            return new DelegateConsequence<T>(callback);
        }
    }
}