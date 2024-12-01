using AOC2023;

namespace AOC2024.Solutions {
    internal class Day1(string inputFilePath) : BaseSolution(inputFilePath) {
        protected override string PartOne() {
            string[] rows = InputReaderUtil.RawLines(inputFilePath);
            IEnumerable<string[]> colValues = rows.Select(row => row.Split("   "));
            IEnumerable<(int leftValue, int rightValue)> parsedColValues = colValues.Select(t => {
                if(int.TryParse(t[0], out int leftValue) && int.TryParse(t[1], out int rightValue)) {
                    return (leftValue, rightValue);
                }

                throw new ArgumentException($"Either {t[0]} or {t[1]} could not be parsed");
            });

            List<int> leftValues = [.. parsedColValues.Select(t => t.leftValue).Order()];
            List<int> rightValues = [.. parsedColValues.Select(t => t.rightValue).Order()];

            int i = 0;
            int left = leftValues[0];
            int right = rightValues[0];
            int sum = 0;

            while(i < leftValues.Count || i < rightValues.Count) {
                sum += Math.Abs(left - right);
                ++i;

                if(i < leftValues.Count) {
                    left = leftValues[i];
                }

                if(i < rightValues.Count) {
                    right = rightValues[i];
                }
            }

            return sum.ToString();
        }

        protected override string PartTwo() {
            string[] rows = InputReaderUtil.RawLines(inputFilePath);
            IEnumerable<string[]> colValues = rows.Select(row => row.Split("   "));
            IEnumerable<(int leftValue, int rightValue)> parsedColValues = colValues.Select(t => {
                if(int.TryParse(t[0], out int leftValue) && int.TryParse(t[1], out int rightValue)) {
                    return (leftValue, rightValue);
                }

                throw new ArgumentException($"Either {t[0]} or {t[1]} could not be parsed");
            });

            Dictionary<int, int> rightCounts = [];
            foreach((int _, int rightValue) in parsedColValues) {
                if(rightCounts.TryGetValue(rightValue, out int value)) {
                    rightCounts[rightValue] = ++value;
                }
                else {
                    rightCounts[rightValue] = 1;
                }
            }

            return parsedColValues.Select(t => {
                if(rightCounts.TryGetValue(t.leftValue, out int count)) {
                    return t.leftValue * count;
                }
                else {
                    return 0;
                }
            }).Sum().ToString();
        }
    }
}