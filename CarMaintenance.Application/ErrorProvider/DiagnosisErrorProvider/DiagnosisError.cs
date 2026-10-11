using CarMaintenance.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.ErrorProvider.DiagnosisErrorProvider
{
    public static class DiagnosisError
    {
        public static readonly Error EmptyProblem =
            new("Diagnosis.EmptyProblem", "Please describe the car problem.");

        public static readonly Error DatasetNotFound =
            new("Diagnosis.DatasetNotFound", "The fault dataset was not found.");

        public static readonly Error FaultNotFound =
            new("Diagnosis.FaultNotFound", "No matching fault was found.");

        public static readonly Error OllamaFailed =
            new("Diagnosis.OllamaFailed", "Ollama could not process the request.");

        public static readonly Error OllamaUnavailable =
            new("Diagnosis.OllamaUnavailable", "Ollama is unavailable.");

        public static readonly Error EmptyAnswer =
            new("Diagnosis.EmptyAnswer", "Ollama returned an empty answer.");

        public static readonly Error InvalidDatasetOrResponse =
            new("Diagnosis.InvalidJson", "The dataset or Ollama response is invalid.");

        public static readonly Error DatasetReadFailed =
            new("Diagnosis.DatasetReadFailed", "The dataset could not be read.");
    }
}
