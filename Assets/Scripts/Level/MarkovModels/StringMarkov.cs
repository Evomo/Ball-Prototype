// This model will use a phrase type of string, and also token type of string.

using System.Collections.Generic;
using MarkovSharp;

namespace Level.MarkovModels {
	public class StringMarkov : GenericMarkov<string, string> {
		public StringMarkov(int level = 2)
			: base(level) { }

		// Define how to split a phrase to collection of tokens
		public override IEnumerable<string> SplitTokens(string input) {
			if (input == null) {
				return new List<string>() {GetPrepadUnigram()};
			}

			return input?.Split(' ');
		}

		// Define how to join the generated tokens back to a phrase
		public override string RebuildPhrase(IEnumerable<string> tokens) {
			return string.Join(" ", tokens);
		}

		public override string GetTerminatorUnigram() {
			return null;
		}

		public override string GetPrepadUnigram() {
			return "";
		}
	}
}