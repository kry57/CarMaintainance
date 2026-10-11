using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Implementations
{
    public static class EmbeddingSimilarity
    {
        public static double CosineSimilarity(
            float[] first,
            float[] second)
        {
            if (first.Length != second.Length)
            {
                throw new ArgumentException(
                    "Embedding vectors must have the same length.");
            }

            double dotProduct = 0;
            double firstMagnitude = 0;
            double secondMagnitude = 0;

            for (int i = 0; i < first.Length; i++)
            {
                dotProduct += first[i] * second[i];

                firstMagnitude += first[i] * first[i];
                secondMagnitude += second[i] * second[i];
            }

            if (firstMagnitude == 0 || secondMagnitude == 0)
                return 0;

            return dotProduct /
                   (Math.Sqrt(firstMagnitude) *
                    Math.Sqrt(secondMagnitude));
        }
    }
}
