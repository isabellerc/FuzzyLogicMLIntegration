using System;

public class FuzzyLogic
{
    // Funções de pertinência para os termos fuzzy 'low', 'medium', 'high'
    public static double PertinenciaLow(double x)
    {
        if (x <= 0) return 1;
        if (x > 0 && x <= 25) return 1 - (x / 25);
        return 0;
    }

    public static double PertinenciaMedium(double x)
    {
        if (x > 0 && x <= 25) return 0;  // Fora da faixa do 'medium'
        if (x > 25 && x <= 50) return (x - 25) / 25;  // Transição suave para 'medium'
        if (x >= 50 && x <= 75) return 1 - Math.Abs(x - 50) / 25;  // 'medium' com valor máximo em 50
        if (x > 75 && x <= 100) return (x - 75) / 25;  // Transição suave para 'high'
        return 0;
    }

    public static double PertinenciaHigh(double x)
    {
        if (x > 50 && x <= 100) return (x - 50) / 50;
        return 0;
    }

    // Fuzzificação: Mapeia o valor crisp para os termos fuzzy
    public static (double low, double medium, double high) Fuzzificar(double x)
    {
        double low = PertinenciaLow(x);
        double medium = PertinenciaMedium(x);
        double high = PertinenciaHigh(x);

        return (low, medium, high);
    }

    // Função de inferência fuzzy (usando uma regra simples)
    public static double InferenciaFuzzy(double low, double medium, double high)
    {
        // Aplicando uma inferência simples, onde a regra assume que 'medium' tem maior peso
        double resultadoFuzzy = (medium * 1) + (low * 0.5) + (high * 0.75);
        return resultadoFuzzy / (low + medium + high); // Média ponderada dos graus de pertinência
    }

    // Defuzzificação (Método do centro de gravidade)
    public static double Defuzzificar(double resultadoFuzzy)
    {
        // Convertendo o valor fuzzy de volta para crisp
        double valorCrisp = resultadoFuzzy * 100; // Ajuste de escala
        return valorCrisp;
    }

    public static void Main(string[] args)
    {
        // Exemplo de valor crisp
        double valorCrisp = 0.75;

        // Fuzzificação
        var (low, medium, high) = Fuzzificar(valorCrisp);

        // Exibindo os valores fuzzificados
        Console.WriteLine($"Valor crisp {valorCrisp} mapeado para o termo fuzzy 'low' com grau de pertinência {low}");
        Console.WriteLine($"Valor crisp {valorCrisp} mapeado para o termo fuzzy 'medium' com grau de pertinência {medium}");
        Console.WriteLine($"Valor crisp {valorCrisp} mapeado para o termo fuzzy 'high' com grau de pertinência {high}");

        // Inferência fuzzy
        double resultadoFuzzy = InferenciaFuzzy(low, medium, high);
        Console.WriteLine($"Resultado da inferência fuzzy: {resultadoFuzzy}");

        // Defuzzificação
        double valorCrispFinal = Defuzzificar(resultadoFuzzy);
        Console.WriteLine($"Valor crisp após defuzzificação: {valorCrispFinal}");

        // Resultado final
        Console.WriteLine($"Resultado crisp final: {valorCrispFinal}");
    }
}
