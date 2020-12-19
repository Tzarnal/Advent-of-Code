using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;
using Serilog;

namespace Day_19
{
    public interface IMessageRule
    {
        int Number { get; set; }
        bool Ready { get; set; }
        bool Recurses { get; set; }
        HashSet<string> MatchValues { get; set; }

        bool Matches(string input);

        void Prepare(Dictionary<int, IMessageRule> OtherRules);
    }

    public class MessageRuleString : IMessageRule
    {
        public int Number { get; set; }
        public bool Ready { get; set; }
        public bool Recurses { get; set; }
        public HashSet<string> MatchValues { get; set; }

        public MessageRuleString(int number, string ruleInput)
        {
            Number = number;
            MatchValues = new HashSet<string>();

            var matchText = ruleInput.Extract<string>(@"(\w)+");
            MatchValues.Add(matchText);

            Ready = true;
            Recurses = false;
        }

        public bool Matches(string input)
        {
            return MatchValues.Contains(input);
        }

        public void Prepare(Dictionary<int, IMessageRule> OtherRules)
        {
        }
    }

    public class MessageRuleOr : IMessageRule
    {
        public int Number { get; set; }
        public bool Ready { get; set; }
        public bool Recurses { get; set; }
        public HashSet<string> MatchValues { get; set; }

        private List<int> _leftDependantRules;
        private List<int> _rightDependantRules;

        public MessageRuleOr(int number, string ruleInput)
        {
            Number = number;
            MatchValues = new HashSet<string>();

            var chunks = ruleInput.Split("|");

            _leftDependantRules = chunks[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)
               .Select(s => int.Parse(s)).ToList();

            _rightDependantRules = chunks[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s)).ToList();

            if (_leftDependantRules.Contains(number) || _rightDependantRules.Contains(number))
            {
                Recurses = true;
            }
        }

        public bool Matches(string input)
        {
            return MatchValues.Contains(input);
        }

        public void Prepare(Dictionary<int, IMessageRule> OtherRules)
        {
            var matchValues = new HashSet<string> { "" };

            if (Ready)
            {
                return;
            }

            if (Recurses)
            {
                MatchValues = PrepareRecurse(OtherRules, 5);
                Ready = true;
                return;
            }

            if (_leftDependantRules.All(l => OtherRules[l].Ready)
                && _rightDependantRules.All(r => OtherRules[r].Ready))
            {
                foreach (var lRule in _leftDependantRules)
                {
                    matchValues = Combine(matchValues, OtherRules[lRule].MatchValues);
                }

                MatchValues.UnionWith(matchValues);

                matchValues = new HashSet<string> { "" };

                foreach (var rRule in _rightDependantRules)
                {
                    matchValues = Combine(matchValues, OtherRules[rRule].MatchValues);
                }

                MatchValues.UnionWith(matchValues);

                Ready = true;
            }
        }

        public HashSet<string> PrepareRecurse(Dictionary<int, IMessageRule> OtherRules, int depth)
        {
            var matchValues = new HashSet<string> { "" };

            if (depth <= 0)
            {
                return matchValues;
            }

            depth--;

            foreach (var lRule in _leftDependantRules)
            {
                if (lRule == Number)
                {
                    matchValues = Combine(matchValues, PrepareRecurse(OtherRules, depth));
                }
                else
                {
                    var rValues = PrepareRecurse(OtherRules, depth);

                    var m = rValues.Max(m => m.Length);

                    if (m >= 88)
                    {
                        break;
                    }

                    matchValues = Combine(matchValues, OtherRules[lRule].MatchValues);
                }
            }

            MatchValues.UnionWith(matchValues);

            matchValues = new HashSet<string> { "" };

            foreach (var rRule in _rightDependantRules)
            {
                if (rRule == Number)
                {
                    var rValues = PrepareRecurse(OtherRules, depth);

                    var m = rValues.Max(m => m.Length);

                    if (m >= 88)
                    {
                        break;
                    }

                    matchValues = Combine(matchValues, rValues);
                }
                else
                {
                    matchValues = Combine(matchValues, OtherRules[rRule].MatchValues);
                }
            }

            MatchValues.UnionWith(matchValues);
            return matchValues;
        }

        private HashSet<string> Combine(HashSet<string> established, HashSet<string> extra)
        {
            var output = new HashSet<string>();

            var eM = established.Max(m => m.Length);
            var extraM = extra.Max(m => m.Length);

            if (eM >= 88 || extraM >= 88)
            {
                return output;
            }

            if (eM + extraM > 88)
            {
                return output;
            }

            foreach (var entry in established)
            {
                foreach (var add in extra)
                {
                    output.Add(entry + add);
                }
            }

            return output;
        }
    }

    public class MessageRuleAnd : IMessageRule
    {
        public int Number { get; set; }
        public bool Ready { get; set; }
        public bool Recurses { get; set; }

        public HashSet<string> MatchValues { get; set; }

        private List<int> _dependantRules;

        public MessageRuleAnd(int number, string ruleInput)
        {
            Number = number;
            MatchValues = new HashSet<string>();

            _dependantRules = ruleInput.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s)).ToList();
        }

        public bool Matches(string input)
        {
            return MatchValues.Contains(input);
        }

        public void Prepare(Dictionary<int, IMessageRule> OtherRules)
        {
            var matchValues = new HashSet<string> { "" };

            if (!Ready && _dependantRules.All(d => OtherRules[d].Ready))
            {
                foreach (var dRule in _dependantRules)
                {
                    matchValues = Combine(matchValues, OtherRules[dRule].MatchValues);
                }

                MatchValues = matchValues;
                Ready = true;
            }
        }

        private HashSet<string> Combine(HashSet<string> established, HashSet<string> extra)
        {
            var output = new HashSet<string>();

            foreach (var entry in established)
            {
                foreach (var add in extra)
                {
                    output.Add(entry + add);
                }
            }

            return output;
        }
    }
}