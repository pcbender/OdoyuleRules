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
namespace OdoyuleRules.Configuration.Builders
{
    using System;
    using System.Collections.Generic;
    using Compilation;
    using Internals.Caching;
    using RuntimeConfigurators;
    using SemanticModel;


    public class OdoyuleEngineBuilder :
        EngineBuilder
    {
        readonly Cache<string, Rule> _rules;
        readonly Func<RuntimeConfigurator> _runtimeConfiguratorFactory;
        readonly IList<Action<RuntimeConfigurator>> _runtimeConfiguratorActions;

        public OdoyuleEngineBuilder()
        {
            _runtimeConfiguratorFactory = DefaultRuntimeConfiguratorFactory;

            _rules = new DictionaryCache<string, Rule>(rule => rule.RuleName);
            _runtimeConfiguratorActions = new List<Action<RuntimeConfigurator>>();
        }

        public void AddRule(Rule rule)
        {
            _rules.AddValue(rule);
        }

        public RulesEngine Build()
        {
            RuntimeConfigurator runtimeConfigurator = _runtimeConfiguratorFactory();

            foreach (var configuratorAction in _runtimeConfiguratorActions)
            {
                configuratorAction(runtimeConfigurator);
            }

            CompileRules(runtimeConfigurator);

            return runtimeConfigurator.RulesEngine;
        }

        public void AddRuntimeConfiguratorAction(Action<RuntimeConfigurator> callback)
        {
            _runtimeConfiguratorActions.Add(callback);
        }

        void CompileRules(RuntimeConfigurator runtimeConfigurator)
        {
            RuleCompiler compiler = new SemanticRuleCompiler(runtimeConfigurator);

            _rules.Each(compiler.Compile);
        }

        static RuntimeConfigurator DefaultRuntimeConfiguratorFactory()
        {
            return new OdoyuleRuntimeConfigurator();
        }
    }
}