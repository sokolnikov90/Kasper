using System;
using System.Collections.Generic;
using CollectionPairs;
using NUnit.Framework;

namespace CollectionPairsUnitTests
{

    [TestFixture]
    public class CollectionHelperTests
    {
        [Test]
        [TestCaseSource("GetAllParis_TestCases")]
        public IEnumerable<NumbersPair> GetAllParis_TestCollections_Success(IEnumerable<int> collection, int x)
        {
            var result = CollectionHelper.GetAllParis(collection, x);

            return result;
        }

        private static IEnumerable<TestCaseData> GetAllParis_TestCases()
        {
            yield return new TestCaseData(new[] {0, 2, 3, 4, 5, 7, 10}, 7)
                .Returns(new[]
                {
                    new NumbersPair(0, 7),
                    new NumbersPair(2, 5),
                    new NumbersPair(3, 4)
                });
            yield return new TestCaseData(new[] {-3, -5, -3, -1, -12}, -4)
                .Returns(new[]
                {
                    new NumbersPair(-3, -1),
                });
            yield return new TestCaseData(new[] {-3, -5, -2, 0, 3, 7, 9, 12}, 7)
                .Returns(new[]
                {
                    new NumbersPair(-5, 12),
                    new NumbersPair(-2, 9),
                    new NumbersPair(0, 7)
                });
            yield return new TestCaseData(new[] {-2, -2, -2}, -4)
                .Returns(new[]
                {
                    new NumbersPair(-2, -2),
                });
            yield return new TestCaseData(new[] {-5, -4, -3, -2}, -4)
                .Returns(new List<NumbersPair>());
            yield return new TestCaseData(null, 0)
                .Throws(typeof(ArgumentNullException));
        }
    }
}
