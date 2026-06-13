using Microsoft.ML;
using Microsoft.ML.Data;

namespace ApiInteligenteTareas.Services;

public class SentimientoService
{
    private readonly PredictionEngine<SentimientoData, SentimientoPrediction> _predictionEngine;
    private readonly object _predictionLock = new();

    public SentimientoService(IWebHostEnvironment environment)
    {
        var mlContext = new MLContext(seed: 0);
        var datasetPath = Path.Combine(environment.ContentRootPath, "Data", "sentimiento_dataset.csv");

        var trainingData = mlContext.Data.LoadFromTextFile<SentimientoData>(datasetPath, hasHeader: true, separatorChar: ',');

        var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimientoData.Comentario))
            .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(SentimientoData.Label), featureColumnName: "Features"));

        var model = pipeline.Fit(trainingData);
        _predictionEngine = mlContext.Model.CreatePredictionEngine<SentimientoData, SentimientoPrediction>(model);
    }

    public string Clasificar(string comentario)
    {
        var input = new SentimientoData { Comentario = comentario };
        lock (_predictionLock)
        {
            var prediction = _predictionEngine.Predict(input);
            return prediction.PredictedLabel ? "Positivo" : "Negativo";
        }
    }
}

public class SentimientoData
{
    [LoadColumn(0)]
    public string Comentario { get; set; } = string.Empty;

    [LoadColumn(1)]
    public bool Label { get; set; }
}

public class SentimientoPrediction
{
    [ColumnName("PredictedLabel")]
    public bool PredictedLabel { get; set; }

    public float Score { get; set; }
    public float Probability { get; set; }
}
