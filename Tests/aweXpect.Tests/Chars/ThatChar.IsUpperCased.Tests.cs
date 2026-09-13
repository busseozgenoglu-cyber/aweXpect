namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsUpperCased
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData('A')]
			[InlineData('Z')]
			[InlineData('Ä')]
			[InlineData('Ω')]
			public async Task WhenSubjectIsUpperCased_ShouldSucceed(char subject)
			{
				async Task Act() => await That(subject).IsUpperCased();
				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('a')]
			[InlineData('1')]
			[InlineData('@')]
			public async Task WhenSubjectIsNotUpperCased_ShouldFail(char subject)
			{
				async Task Act() => await That(subject).IsUpperCased();
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is upper-cased,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenSubjectIsUpperCased_ShouldFail()
			{
				char subject = 'A';
				async Task Act() => await That(subject).DoesNotComplyWith(it => it.IsUpperCased());
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not upper-cased,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotUpperCased_ShouldSucceed()
			{
				async Task Act() => await That('a').DoesNotComplyWith(it => it.IsUpperCased());
				await That(Act).DoesNotThrow();
			}
		}
	}
}
